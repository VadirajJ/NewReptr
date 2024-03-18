Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.ScriptManager
Imports System.Drawing.FontStyle
Public Class ScheduleNote
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "ScheduleNote"
    Dim sSession As New AllSession
    Dim objclsReport As New clsReport
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Dim objclsOpeningBalance As New clsOpeningBalance
    Dim objgenfunc As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleNote As New clsScheduleNote
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                lblId.Text = 0
                'BindCompanytype()
                LoadExistingCustomer()
                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        If ddlCustomerName.SelectedIndex > 0 Then
                            ddlCustomerName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
    Public Sub LoadExistingCustomer()
        Try
            ddlCustomerName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "Cust_Name"
            ddlCustomerName.DataValueField = "Cust_Id"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub ddlsubheading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsubheading.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlsubheading.SelectedIndex > 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    dt = objclsSchduleNote.getSubHeadingDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlsubheading.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        txtEnterDescription.Text = dt.Rows(0)("Description")
                        lblId.Text = dt.Rows(0)("ASHN_ID")
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlsubheading_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            lblModalValidationMsg.Text = lblError.Text
            If ddlCustomerName.SelectedIndex > 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustomerName.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                dt = objclsSchduleNote.getSubHeadingDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                If dt.Rows.Count > 0 Then
                    ddlsubheading.DataSource = dt
                    ddlsubheading.DataTextField = "AsSh_Notes"
                    ddlsubheading.DataValueField = "assh_id"
                    ddlsubheading.DataBind()
                    ddlsubheading.Items.Insert(0, "Select Sub Heading")
                    txtEnterDescription.Text = ""
                Else
                    ddlsubheading.DataSource = dt
                    ddlsubheading.DataBind()
                End If
                dt = objclsSchduleNote.getNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                If dt.Rows.Count > 0 Then
                    gvReportContentMaster.DataSource = dt
                    gvReportContentMaster.DataBind()
                Else
                    gvReportContentMaster.DataSource = Nothing
                    gvReportContentMaster.DataBind()
                    txtEnterDescription.Text = ""
                End If
            Else
            End If
            lblIssCurrent.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblIssPrev.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCAu.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPAu.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCIs.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPIs.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCSub.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPSub.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCCalls.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPCalls.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCFor.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPFor.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCdetails.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            lblPdetails.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue - 1
            lblCShares.Text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue

            'lblFAIssuedCYear_Amount.text = " 31st March" & " 20" & ddlFinancialYear.SelectedValue
            BindFirstScheduleAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            BindSecondScheduleAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            BindDescriptionAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            txtEnterDescription.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged")
        End Try
    End Sub

    ''' Predifined Schedules
    Private Sub ImgBtnAddDetails_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnAddDetails.Click
        Dim Arr() As String
        Dim dt As New DataTable
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                If ddlsubheading.SelectedIndex > 0 Then
                    If lblId.Text <> 0 Then
                        objclsSchduleNote.ASHN_ID = lblId.Text
                    Else
                        objclsSchduleNote.ASHN_ID = 0
                    End If
                    lblError.Text = ""
                    objclsSchduleNote.ASHN_SubHeadingId = ddlsubheading.SelectedValue
                    objclsSchduleNote.ASHN_CustomerId = ddlCustomerName.SelectedValue
                    objclsSchduleNote.ASHN_Description = Trim(txtEnterDescription.Text)
                    objclsSchduleNote.ASHN_DelFlag = "A"
                    objclsSchduleNote.ASHN_Status = "A"
                    objclsSchduleNote.ASHN_CreatedBy = sSession.UserID
                    objclsSchduleNote.ASHN_CreatedOn = DateTime.Today
                    objclsSchduleNote.ASHN_UpdatedBy = sSession.UserID
                    objclsSchduleNote.ASHN_UpdatedOn = DateTime.Today
                    objclsSchduleNote.ASHN_ApprovedBy = sSession.UserID
                    objclsSchduleNote.ASHN_ApprovedOn = DateTime.Today
                    objclsSchduleNote.ASHN_CompID = sSession.AccessCodeID
                    objclsSchduleNote.ASHN_YearID = ddlFinancialYear.SelectedValue
                    objclsSchduleNote.ASHN_IPAddress = sSession.IPAddress
                    objclsSchduleNote.ASHN_Operation = ""
                    Arr = objclsSchduleNote.saveNoticeBoard(sSession.AccessCode, objclsSchduleNote)
                    dt = objclsSchduleNote.getNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        gvReportContentMaster.DataSource = dt
                        gvReportContentMaster.DataBind()
                    End If
                    txtEnterDescription.Text = "" : ddlsubheading.SelectedIndex = 0
                Else
                    lblError.Text = "Select Sub Heading"
                End If
            Else
                lblError.Text = "Select Customer"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnAddDetails_Click")
        End Try
    End Sub


    Private Sub lnkbtnSchedulenoteDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnSchedulenoteDetails.Click
        Try
            lblError.Text = ""
            liShchedulenotes.Attributes.Remove("class")
            liShchedulenotes.Attributes.Add("class", "active")
            liShchedulepredefinedNotes.Attributes.Remove("class")
            divEmpBasic.Attributes.Add("class", "tab-pane active")
            divSchedulePredefinednotes.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSchedulenoteDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub lnkbtnShchedulepredefinedNotes_Click(sender As Object, e As EventArgs) Handles lnkbtnShchedulepredefinedNotes.Click
        Try
            lblError.Text = ""
            liShchedulepredefinedNotes.Attributes.Remove("class")
            liShchedulepredefinedNotes.Attributes.Add("class", "active")
            liShchedulenotes.Attributes.Remove("class")
            divSchedulePredefinednotes.Attributes.Add("class", "tab-pane active")
            divEmpBasic.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnShchedulepredefinedNotes_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub


    '''  Defined Schedules

    Public Sub BindFirstScheduleAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim dt As New System.Data.DataSet
        Dim dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8 As DataTable
        Try
            lblError.Text = ""
            dt = objclsSchduleNote.getSchedFirstNoteAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "")
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            dt3 = dt.Tables(2)
            dt4 = dt.Tables(3)
            dt5 = dt.Tables(4)
            dt6 = dt.Tables(5)
            If dt1.Rows.Count > 0 Then
                gvFAuthorised.DataSource = dt1
                gvFAuthorised.DataBind()
            Else
                gvFAuthorised.DataSource = Nothing
                gvFAuthorised.DataBind()
            End If
            If dt2.Rows.Count > 0 Then
                gvFISubscribed.DataSource = dt2
                gvFISubscribed.DataBind()
            Else
                gvFISubscribed.DataSource = Nothing
                gvFISubscribed.DataBind()
            End If
            If dt3.Rows.Count > 0 Then
                gvFAIssued.DataSource = dt3
                gvFAIssued.DataBind()
            Else
                gvFAIssued.DataSource = Nothing
                gvFAIssued.DataBind()
            End If
            If dt4.Rows.Count > 0 Then
                gvFBSubscribed.DataSource = dt4
                gvFBSubscribed.DataBind()
            Else
                gvFBSubscribed.DataSource = Nothing
                gvFBSubscribed.DataBind()
            End If
            If dt5.Rows.Count > 0 Then
                gvFCCUnpaid.DataSource = dt5
                gvFCCUnpaid.DataBind()
            Else
                gvFCCUnpaid.DataSource = Nothing
                gvFCCUnpaid.DataBind()
            End If
            If dt6.Rows.Count > 0 Then
                gvFDFS.DataSource = dt6
                gvFDFS.DataBind()
            Else
                gvFDFS.DataSource = Nothing
                gvFDFS.DataBind()
            End If


            dt = objclsSchduleNote.getSchedThirdNoteAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "")
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            dt3 = dt.Tables(2)
            dt4 = dt.Tables(3)
            dt5 = dt.Tables(4)
            dt6 = dt.Tables(5)
            If dt1.Rows.Count > 0 Then
                gvTBPref.DataSource = dt1
                gvTBPref.DataBind()
            Else
                gvTBPref.DataSource = Nothing
                gvTBPref.DataBind()
            End If
            If dt2.Rows.Count > 0 Then
                gvTBEquity.DataSource = dt2
                gvTBEquity.DataBind()
            Else
                gvTBEquity.DataSource = Nothing
                gvTBEquity.DataBind()
            End If
            If dt3.Rows.Count > 0 Then
                gvTEEquity.DataSource = dt3
                gvTEEquity.DataBind()
            Else
                gvTEEquity.DataSource = Nothing
                gvTEEquity.DataBind()
            End If
            If dt4.Rows.Count > 0 Then
                gvTEPref.DataSource = dt4
                gvTEPref.DataBind()
            Else
                gvTEPref.DataSource = Nothing
                gvTEPref.DataBind()
            End If

            dt = objclsSchduleNote.getSchedFourthNoteAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            If dt1.Rows.Count > 0 Then
                gvFSCYShares.DataSource = dt1
                gvFSCYShares.DataBind()
            Else
                gvFSCYShares.DataSource = Nothing
                gvFSCYShares.DataBind()
            End If
            'If dt2.Rows.Count > 0 Then
            '    gvFSPYREF.DataSource = dt2
            '    gvFSPYREF.DataBind()
            'Else
            '    gvFSPYREF.DataSource = Nothing
            '    gvFSPYREF.DataBind()
            'End If
            gvFAIssued.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
            gvFAIssued.DataBind()
            gvFBSubscribed.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
            gvFBSubscribed.DataBind()
            gvFCCUnpaid.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
            gvFCCUnpaid.DataBind()
            gvFDFS.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
            gvFDFS.DataBind()
            gvTBEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
            gvTBEquity.DataBind()
            gvTBPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
            gvTBPref.DataBind()
            gvTEEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, objclsSchduleNote.SNT_Category)
            gvTEEquity.DataBind()
            gvTEPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEP")
            gvTEPref.DataBind()
            gvFSPYREF.DataSource = objclsSchduleNote.getSchedFourthNoteDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
            gvFSPYREF.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "divSchedulePredefinednotes_Load" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDescriptionAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim dt As New DataSet
        Dim dt1, dt2, dt3, dt4 As DataTable
        Try
            dt = objclsSchduleNote.getDescAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iSupplier, "")
            dt1 = dt.Tables(0)
            dt2 = dt.Tables(1)
            dt3 = dt.Tables(2)
            dt4 = dt.Tables(3)

            If dt1.Rows.Count > 0 Then
                If (dt1.Rows(0)("SND_Description")) <> "" Then
                    txtDescCTermsEquity.Text = dt1.Rows(0)("SND_Description")
                Else
                    txtDescCTermsEquity.Text = ""
                End If
            Else
                txtDescCTermsEquity.Text = ""
            End If
            If dt2.Rows.Count > 0 Then
                If (dt2.Rows(0)("SND_Description")) <> "" Then
                    txtDescDTermsPref.Text = dt2.Rows(0)("SND_Description")
                Else
                    txtDescDTermsPref.Text = ""
                End If
            End If
            If dt3.Rows.Count > 0 Then
                If (dt3.Rows(0)("SND_Description")) <> "" Then
                    txtDescFShares.Text = dt3.Rows(0)("SND_Description")
                Else
                    txtDescFShares.Text = ""
                End If
            Else
                txtDescFShares.Text = ""
            End If
            If dt4.Rows.Count > 0 Then
                If (dt4.Rows(0)("SND_Description")) <> "" Then
                    txtFootNote.Text = dt4.Rows(0)("SND_Description")
                Else
                    txtFootNote.Text = ""
                End If
            Else
                txtFootNote.Text = ""
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSecondScheduleAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
        Dim dt As New DataSet
        Dim dt1, dt2, dt3, dt4 As DataTable
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                dt = objclsSchduleNote.getSchedSecondNoteAllDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iSupplier, "")
                dt1 = dt.Tables(0)
                dt2 = dt.Tables(1)
                dt3 = dt.Tables(2)
                dt4 = dt.Tables(3)
                If dt1.Rows.Count > 0 Then
                    If dt1.Rows.Count > 0 Then
                        If (dt1.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                            txtSFBegCYShares.Text = dt1.Rows(0)("SNS_CYear_BegShares")
                        Else
                            txtSFBegCYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                            txtSFBegCYValues.Text = dt1.Rows(0)("SNS_CYear_BegAmount")
                        Else
                            txtSFBegCYValues.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                            txtSFBegPYShares.Text = dt1.Rows(0)("SNS_PYear_BegShares")
                        Else
                            txtSFBegPYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                            txtSFBegPYValues.Text = dt1.Rows(0)("SNS_PYear_BegAmount")
                        Else
                            txtSFBegPYValues.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                            txtSFAddCYShares.Text = dt1.Rows(0)("SNS_CYear_AddShares")
                        Else
                            txtSFAddCYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                            txtSFAddCYValues.Text = dt1.Rows(0)("SNS_CYear_AddAmount")
                        Else
                            txtSFAddCYValues.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                            txtSFAddPYShares.Text = dt1.Rows(0)("SNS_PYear_AddShares")
                        Else
                            txtSFAddPYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                            txtSFAddPYValues.Text = dt1.Rows(0)("SNS_PYear_AddAmount")
                        Else
                            txtSFAddPYValues.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                            txtSFEndCYShares.Text = dt1.Rows(0)("SNS_CYear_EndShares")
                        Else
                            txtSFEndCYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                            txtSFEndCYValues.Text = dt1.Rows(0)("SNS_CYear_EndAmount")
                        Else
                            txtSFEndCYValues.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                            txtSFEndPYShares.Text = dt1.Rows(0)("SNS_PYear_EndShares")
                        Else
                            txtSFEndPYShares.Text = ""
                        End If
                        If (dt1.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                            txtSFEndPYValues.Text = dt1.Rows(0)("SNS_PYear_EndAmount")
                        Else
                            txtSFEndPYValues.Text = ""
                        End If
                    End If
                Else
                    txtSFBegCYShares.Text = "" : txtSFBegCYValues.Text = "" : txtSFBegPYShares.Text = "" : txtSFBegPYValues.Text = ""
                    txtSFAddCYShares.Text = "" : txtSFAddCYValues.Text = "" : txtSFAddPYShares.Text = "" : txtSFAddPYValues.Text = ""
                    txtSFEndCYShares.Text = "" : txtSFEndCYValues.Text = "" : txtSFEndPYShares.Text = "" : txtSFEndPYValues.Text = ""
                End If
                If dt2.Rows.Count > 0 Then
                    If (dt2.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSSBegCYShares.Text = dt2.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSSBegCYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSSBegCYValues.Text = dt2.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSSBegCYValues.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSSBegPYShares.Text = dt2.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSSBegPYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSSBegPYValues.Text = dt2.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSSBegPYValues.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSSAddCYShares.Text = dt2.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSSAddCYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSSAddCYValues.Text = dt2.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSSAddCYValues.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSSAddPYShares.Text = dt2.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSSAddPYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSSAddPYValues.Text = dt2.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSSAddPYValues.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSSEndCYShares.Text = dt2.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSSEndCYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSSEndCYValues.Text = dt2.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSSEndCYValues.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSSEndPYShares.Text = dt2.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSSEndPYShares.Text = ""
                    End If
                    If (dt2.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSSEndPYValues.Text = dt2.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSSEndPYValues.Text = ""
                    End If
                Else
                    txtSSBegCYShares.Text = "" : txtSSBegCYValues.Text = "" : txtSSBegPYShares.Text = ""
                    txtSSBegPYValues.Text = "" : txtSSAddCYShares.Text = "" : txtSSAddCYValues.Text = ""
                    txtSSAddPYShares.Text = "" : txtSSAddPYValues.Text = "" : txtSSEndCYShares.Text = ""
                    txtSSEndCYValues.Text = "" : txtSSEndPYShares.Text = "" : txtSSEndPYValues.Text = ""

                End If
                If dt3.Rows.Count > 0 Then
                    If (dt3.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSTBegCYShares.Text = dt3.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSTBegCYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSTBegCYValues.Text = dt3.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSTBegCYValues.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSTBegPYShares.Text = dt3.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSTBegPYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSTBegPYValues.Text = dt3.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSTBegPYValues.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSTAddCYShares.Text = dt3.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSTAddCYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSTAddCYValues.Text = dt3.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSTAddCYValues.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSTAddPYShares.Text = dt3.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSTAddPYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSTAddPYValues.Text = dt3.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSTAddPYValues.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSTEndCYShares.Text = dt3.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSTEndCYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSTEndCYValues.Text = dt3.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSTEndCYValues.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSTEndPYShares.Text = dt3.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSTEndPYShares.Text = ""
                    End If
                    If (dt3.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSTEndPYValues.Text = dt3.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSTEndPYValues.Text = ""
                    End If
                Else
                    txtSTBegCYShares.Text = "" : txtSTBegCYValues.Text = "" : txtSTBegPYShares.Text = ""
                    txtSTBegPYValues.Text = "" : txtSTAddCYShares.Text = "" : txtSTAddCYValues.Text = ""
                    txtSTAddPYShares.Text = "" : txtSTAddPYValues.Text = "" : txtSTEndCYShares.Text = ""
                    txtSTEndCYValues.Text = "" : txtSTEndPYShares.Text = "" : txtSTEndPYValues.Text = ""
                End If
                If dt4.Rows.Count > 0 Then
                    If (dt4.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSVBegCYShares.Text = dt4.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSVBegCYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSVBegCYValues.Text = dt4.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSVBegCYValues.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSVBegPYShares.Text = dt4.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSVBegPYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSVBegPYValues.Text = dt4.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSVBegPYValues.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSVAddCYShares.Text = dt4.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSVAddCYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSVAddCYValues.Text = dt4.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSVAddCYValues.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSVAddPYShares.Text = dt4.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSVAddPYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSVAddPYValues.Text = dt4.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSVAddPYValues.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSVEndCYShares.Text = dt4.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSVEndCYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSVEndCYValues.Text = dt4.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSVEndCYValues.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSVEndPYShares.Text = dt4.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSVEndPYShares.Text = ""
                    End If
                    If (dt4.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSVEndPYValues.Text = dt4.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSVEndPYValues.Text = ""
                    End If
                Else
                    txtSVBegCYShares.Text = "" : txtSVBegCYValues.Text = "" : txtSVBegPYShares.Text = ""
                    txtSVBegPYValues.Text = "" : txtSVAddCYShares.Text = "" : txtSVAddCYValues.Text = ""
                    txtSVAddPYShares.Text = "" : txtSVAddPYValues.Text = "" : txtSVEndCYShares.Text = ""
                    txtSVEndCYValues.Text = "" : txtSVEndPYShares.Text = "" : txtSVEndPYValues.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindSchedSecondDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSchedType As String)
        Dim dt As New DataTable
        Try
            If sSchedType = "SF" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSFBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSFBegCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSFBegCYValues.Text = dt.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSFBegCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSFBegPYShares.Text = dt.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSFBegPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSFBegPYValues.Text = dt.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSFBegPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSFAddCYShares.Text = dt.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSFAddCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSFAddCYValues.Text = dt.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSFAddCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSFAddPYShares.Text = dt.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSFAddPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSFAddPYValues.Text = dt.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSFAddPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSFEndCYShares.Text = dt.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSFEndCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSFEndCYValues.Text = dt.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSFEndCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSFEndPYShares.Text = dt.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSFEndPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSFEndPYValues.Text = dt.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSFEndPYValues.Text = ""
                    End If
                Else
                    txtSFBegCYShares.Text = "" : txtSFBegCYValues.Text = "" : txtSFBegPYShares.Text = "" : txtSFBegPYValues.Text = ""
                    txtSFAddCYShares.Text = "" : txtSFAddCYValues.Text = "" : txtSFAddPYShares.Text = "" : txtSFAddPYValues.Text = ""
                    txtSFEndCYShares.Text = "" : txtSFEndCYValues.Text = "" : txtSFEndPYShares.Text = "" : txtSFEndPYValues.Text = ""
                End If
            ElseIf sSchedType = "SS" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSSBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSSBegCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSSBegCYValues.Text = dt.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSSBegCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSSBegPYShares.Text = dt.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSSBegPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSSBegPYValues.Text = dt.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSSBegPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSSAddCYShares.Text = dt.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSSAddCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSSAddCYValues.Text = dt.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSSAddCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSSAddPYShares.Text = dt.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSSAddPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSSAddPYValues.Text = dt.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSSAddPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSSEndCYShares.Text = dt.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSSEndCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSSEndCYValues.Text = dt.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSSEndCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSSEndPYShares.Text = dt.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSSEndPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSSEndPYValues.Text = dt.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSSEndPYValues.Text = ""
                    End If
                Else
                    txtSSBegCYShares.Text = "" : txtSSBegCYValues.Text = "" : txtSSBegPYShares.Text = ""
                    txtSSBegPYValues.Text = "" : txtSSAddCYShares.Text = "" : txtSSAddCYValues.Text = ""
                    txtSSAddPYShares.Text = "" : txtSSAddPYValues.Text = "" : txtSSEndCYShares.Text = ""
                    txtSSEndCYValues.Text = "" : txtSSEndPYShares.Text = "" : txtSSEndPYValues.Text = ""
                End If
            ElseIf sSchedType = "ST" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSTBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSTBegCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSTBegCYValues.Text = dt.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSTBegCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSTBegPYShares.Text = dt.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSTBegPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSTBegPYValues.Text = dt.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSTBegPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSTAddCYShares.Text = dt.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSTAddCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSTAddCYValues.Text = dt.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSTAddCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSTAddPYShares.Text = dt.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSTAddPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSTAddPYValues.Text = dt.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSTAddPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSTEndCYShares.Text = dt.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSTEndCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSTEndCYValues.Text = dt.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSTEndCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSTEndPYShares.Text = dt.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSTEndPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSTEndPYValues.Text = dt.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSTEndPYValues.Text = ""
                    End If
                Else
                    txtSTBegCYShares.Text = "" : txtSTBegCYValues.Text = "" : txtSTBegPYShares.Text = ""
                    txtSTBegPYValues.Text = "" : txtSTAddCYShares.Text = "" : txtSTAddCYValues.Text = ""
                    txtSTAddPYShares.Text = "" : txtSTAddPYValues.Text = "" : txtSTEndCYShares.Text = ""
                    txtSTEndCYValues.Text = "" : txtSTEndPYShares.Text = "" : txtSTEndPYValues.Text = ""
                End If
            ElseIf sSchedType = "SV" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSVBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSVBegCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_BegAmount")) <> 0 Then
                        txtSVBegCYValues.Text = dt.Rows(0)("SNS_CYear_BegAmount")
                    Else
                        txtSVBegCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegShares")) <> 0 Then
                        txtSVBegPYShares.Text = dt.Rows(0)("SNS_PYear_BegShares")
                    Else
                        txtSVBegPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_BegAmount")) <> 0 Then
                        txtSVBegPYValues.Text = dt.Rows(0)("SNS_PYear_BegAmount")
                    Else
                        txtSVBegPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddShares")) <> 0 Then
                        txtSVAddCYShares.Text = dt.Rows(0)("SNS_CYear_AddShares")
                    Else
                        txtSVAddCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_AddAmount")) <> 0 Then
                        txtSVAddCYValues.Text = dt.Rows(0)("SNS_CYear_AddAmount")
                    Else
                        txtSVAddCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddShares")) <> 0 Then
                        txtSVAddPYShares.Text = dt.Rows(0)("SNS_PYear_AddShares")
                    Else
                        txtSVAddPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_AddAmount")) <> 0 Then
                        txtSVAddPYValues.Text = dt.Rows(0)("SNS_PYear_AddAmount")
                    Else
                        txtSVAddPYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndShares")) <> 0 Then
                        txtSVEndCYShares.Text = dt.Rows(0)("SNS_CYear_EndShares")
                    Else
                        txtSVEndCYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_CYear_EndAmount")) <> 0 Then
                        txtSVEndCYValues.Text = dt.Rows(0)("SNS_CYear_EndAmount")
                    Else
                        txtSVEndCYValues.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndShares")) <> 0 Then
                        txtSVEndPYShares.Text = dt.Rows(0)("SNS_PYear_EndShares")
                    Else
                        txtSVEndPYShares.Text = ""
                    End If
                    If (dt.Rows(0)("SNS_PYear_EndAmount")) <> 0 Then
                        txtSVEndPYValues.Text = dt.Rows(0)("SNS_PYear_EndAmount")
                    Else
                        txtSVEndPYValues.Text = ""
                    End If
                Else
                    txtSVBegCYShares.Text = "" : txtSVBegCYValues.Text = "" : txtSVBegPYShares.Text = ""
                    txtSVBegPYValues.Text = "" : txtSVAddCYShares.Text = "" : txtSVAddCYValues.Text = ""
                    txtSVAddPYShares.Text = "" : txtSVAddPYValues.Text = "" : txtSVEndCYShares.Text = ""
                    txtSVEndCYValues.Text = "" : txtSVEndPYShares.Text = "" : txtSVEndPYValues.Text = ""
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnFAuthorised_Click(sender As Object, e As EventArgs) Handles btnFAuthorised.Click
        Dim iDescId As Integer = 0
        Try
            If txtFAuthorised.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFAuthorised.Text
                objclsSchduleNote.SNF_Category = "AU"
                If Val(txtFAuthorisedCYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFAuthorisedCYamt.Text
                End If
                If Val(txtFAuthorisedPYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFAuthorisedPYamt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFAuthorised()
                gvFAuthorised.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
                gvFAuthorised.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFAuthorised_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFAuthorised()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFAuthorised.Text = "" : txtFAuthorisedCYamt.Text = "" : txtFAuthorisedPYamt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnFISubscribed_Click(sender As Object, e As EventArgs) Handles btnFISubscribed.Click
        Dim iDescId As Integer = 0
        Try
            If txtFIssuedSubscribed.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFIssuedSubscribed.Text
                objclsSchduleNote.SNF_Category = "IS"
                If Val(txtFISubscribedCYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFISubscribedCYamt.Text
                End If
                If Val(txtFISubscribedPYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFISubscribedPYamt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFIssuedSubscribed()
                gvFISubscribed.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
                gvFISubscribed.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFAuthorised_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFIssuedSubscribed()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFIssuedSubscribed.Text = "" : txtFISubscribedCYamt.Text = "" : txtFISubscribedPYamt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnFAIssued_Click(sender As Object, e As EventArgs) Handles btnFAIssued.Click

        Dim iDescId As Integer = 0
        Try
            If txtFAIssued.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFAIssued.Text
                objclsSchduleNote.SNF_Category = "AI"
                If Val(txtFAIssuedCYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFAIssuedCYamt.Text
                End If
                If Val(txtFAIssuedPYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFAIssuedPYamt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFAIssued()
                gvFAIssued.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
                gvFAIssued.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

            End If
            btnFAIssued.Focus()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFAuthorised_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFAIssued()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFAIssued.Text = "" : txtFAIssuedCYamt.Text = "" : txtFAIssuedPYamt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnFBSubscribed_Click(sender As Object, e As EventArgs) Handles btnFBSubscribed.Click
        Dim iDescId As Integer = 0
        Try
            If txtFBSubscribed.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFBSubscribed.Text
                objclsSchduleNote.SNF_Category = "BS"
                If Val(txtFBSubCYAmt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFBSubCYAmt.Text
                End If
                If Val(txtFBSubPYAmt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFBSubPYAmt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFBSubscribed()
                gvFBSubscribed.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
                gvFBSubscribed.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFAuthorised_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
    Public Sub ClearFBSubscribed()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFBSubscribed.Text = "" : txtFBSubCYAmt.Text = "" : txtFBSubPYAmt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnFCCUnpaid_Click(sender As Object, e As EventArgs) Handles btnFCCUnpaid.Click
        Dim iDescId As Integer = 0
        Try
            If txtFCCUnpaid.Text <> "" Then

                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFCCUnpaid.Text
                objclsSchduleNote.SNF_Category = "CC"
                If Val(txtFCCUnpaidCYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFCCUnpaidCYamt.Text
                End If
                If Val(txtFCCUnpaidPYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFCCUnpaidPYamt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFCCUnpaid()
                gvFCCUnpaid.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
                gvFCCUnpaid.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            btnFCCUnpaid.Focus()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFAuthorised_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFCCUnpaid()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFCCUnpaid.Text = "" : txtFCCUnpaidCYamt.Text = "" : txtFCCUnpaidPYamt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnFDFS_Click(sender As Object, e As EventArgs) Handles btnFDFS.Click
        Dim iDescId As Integer = 0
        Try
            If txtFDFS.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNF_ID = lblId.Text
                Else
                    objclsSchduleNote.SNF_ID = 0
                End If

                objclsSchduleNote.SNF_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNF_Description = txtFDFS.Text
                objclsSchduleNote.SNF_Category = "FD"
                If Val(txtFDFSCYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_CYear_Amount = txtFDFSCYamt.Text
                End If
                If Val(txtFDFSPYamt.Text) = 0 Then
                    objclsSchduleNote.SNF_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNF_pYear_Amount = txtFDFSPYamt.Text
                End If
                objclsSchduleNote.SNF_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNF_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNF_Status = "W"
                objclsSchduleNote.SNF_DelFlag = "X"
                objclsSchduleNote.SNF_CRON = DateTime.Today
                objclsSchduleNote.SNF_CrBy = sSession.UserID
                objclsSchduleNote.SNF_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveFirstScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearFDFS()
                gvFDFS.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
                gvFDFS.DataBind()
                ' BindSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFDFS()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtFDFS.Text = "" : txtFDFSCYamt.Text = "" : txtFDFSPYamt.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '' Second  Schedules
    Private Sub btnSFSchedules_Click(sender As Object, e As EventArgs) Handles btnSFSchedules.Click
        Dim iDescId As Integer = 0

        Try



            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNS_ID = lblId.Text
            Else
                objclsSchduleNote.SNS_ID = 0
            End If

            objclsSchduleNote.SNS_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNS_Description = ""
            objclsSchduleNote.SNS_Category = "SF"
            If Val(txtSFBegCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_CYear_BegShares = txtSFBegCYShares.Text
            End If
            If Val(txtSFBegCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_BegAmount = txtSFBegCYValues.Text
            End If
            If Val(txtSFBegPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_PYear_BegShares = txtSFBegPYShares.Text
            End If
            If Val(txtSFBegPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_BegAmount = txtSFBegPYValues.Text
            End If
            If Val(txtSFAddCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_CYear_AddShares = txtSFAddCYShares.Text
            End If
            If Val(txtSFAddCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_AddAmount = txtSFAddCYValues.Text
            End If
            If Val(txtSFAddPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_PYear_AddShares = txtSFAddPYShares.Text
            End If
            If Val(txtSFAddPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_AddAmount = txtSFAddPYValues.Text
            End If
            If Val(txtSFEndCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_CYear_EndShares = txtSFEndCYShares.Text
            End If
            If Val(txtSFEndCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_EndAmount = txtSFEndCYValues.Text
            End If
            If Val(txtSFEndPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_PYear_EndShares = txtSFEndPYShares.Text
            End If
            If Val(txtSFEndPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_EndAmount = txtSFEndPYValues.Text
            End If
            objclsSchduleNote.SNS_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNS_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNS_Status = "W"
            objclsSchduleNote.SNS_DelFlag = "X"
            objclsSchduleNote.SNS_CRON = DateTime.Today
            objclsSchduleNote.SNS_CrBy = sSession.UserID
            objclsSchduleNote.SNS_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveSecondScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            BindSchedSecondDetails(sSession.AccessCode, sSession.AccessCodeID, "SF")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSSSchedules_Click(sender As Object, e As EventArgs) Handles btnSSSchedules.Click
        Dim iDescId As Integer = 0

        Try

            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNS_ID = lblId.Text
            Else
                objclsSchduleNote.SNS_ID = 0
            End If

            objclsSchduleNote.SNS_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNS_Description = ""
            objclsSchduleNote.SNS_Category = "SS"
            If Val(txtSFBegCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_CYear_BegShares = txtSSBegCYShares.Text
            End If
            If Val(txtSFBegCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_BegAmount = txtSSBegCYValues.Text
            End If
            If Val(txtSFBegPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_PYear_BegShares = txtSSBegPYShares.Text
            End If
            If Val(txtSFBegPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_BegAmount = txtSSBegPYValues.Text
            End If
            If Val(txtSFAddCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_CYear_AddShares = txtSSAddCYShares.Text
            End If
            If Val(txtSFAddCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_AddAmount = txtSSAddCYValues.Text
            End If
            If Val(txtSFAddPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_PYear_AddShares = txtSSAddPYShares.Text
            End If
            If Val(txtSFAddPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_AddAmount = txtSSAddPYValues.Text
            End If
            If Val(txtSFEndCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_CYear_EndShares = txtSSEndCYShares.Text
            End If
            If Val(txtSFEndCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_EndAmount = txtSSEndCYValues.Text
            End If
            If Val(txtSFEndPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_PYear_EndShares = txtSSEndPYShares.Text
            End If
            If Val(txtSFEndPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_EndAmount = txtSSEndPYValues.Text
            End If
            objclsSchduleNote.SNS_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNS_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNS_Status = "W"
            objclsSchduleNote.SNS_DelFlag = "X"
            objclsSchduleNote.SNS_CRON = DateTime.Today
            objclsSchduleNote.SNS_CrBy = sSession.UserID
            objclsSchduleNote.SNS_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveSecondScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            BindSchedSecondDetails(sSession.AccessCode, sSession.AccessCodeID, "SS")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSTSchedules_Click(sender As Object, e As EventArgs) Handles btnSTSchedules.Click
        Dim iDescId As Integer = 0

        Try


            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNS_ID = lblId.Text
            Else
                objclsSchduleNote.SNS_ID = 0
            End If

            objclsSchduleNote.SNS_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNS_Description = ""
            objclsSchduleNote.SNS_Category = "ST"
            If Val(txtSTBegCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_CYear_BegShares = txtSTBegCYShares.Text
            End If
            If Val(txtSTBegCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_BegAmount = txtSTBegCYValues.Text
            End If
            If Val(txtSTBegPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_PYear_BegShares = txtSTBegPYShares.Text
            End If
            If Val(txtSTBegPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_BegAmount = txtSTBegPYValues.Text
            End If
            If Val(txtSTAddCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_CYear_AddShares = txtSTAddCYShares.Text
            End If
            If Val(txtSTAddCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_AddAmount = txtSTAddCYValues.Text
            End If
            If Val(txtSTAddPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_PYear_AddShares = txtSTAddPYShares.Text
            End If
            If Val(txtSTAddPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_AddAmount = txtSTAddPYValues.Text
            End If
            If Val(txtSTEndCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_CYear_EndShares = txtSTEndCYShares.Text
            End If
            If Val(txtSTEndCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_EndAmount = txtSTEndCYValues.Text
            End If
            If Val(txtSTEndPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_PYear_EndShares = txtSTEndPYShares.Text
            End If
            If Val(txtSTEndPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_EndAmount = txtSTEndPYValues.Text
            End If
            objclsSchduleNote.SNS_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNS_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNS_Status = "W"
            objclsSchduleNote.SNS_DelFlag = "X"
            objclsSchduleNote.SNS_CRON = DateTime.Today
            objclsSchduleNote.SNS_CrBy = sSession.UserID
            objclsSchduleNote.SNS_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveSecondScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            BindSchedSecondDetails(sSession.AccessCode, sSession.AccessCodeID, "ST")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSTSchedules_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSVSchedules_Click(sender As Object, e As EventArgs) Handles btnSVSchedules.Click
        Dim iDescId As Integer = 0

        Try
            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNS_ID = lblId.Text
            Else
                objclsSchduleNote.SNS_ID = 0
            End If

            objclsSchduleNote.SNS_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNS_Description = ""
            objclsSchduleNote.SNS_Category = "SV"
            If Val(txtSVBegCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_CYear_BegShares = txtSVBegCYShares.Text
            End If
            If Val(txtSVBegCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_BegAmount = txtSVBegCYValues.Text
            End If
            If Val(txtSVBegPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_BegShares = 0
            Else
                objclsSchduleNote.SNS_PYear_BegShares = txtSVBegPYShares.Text
            End If
            If Val(txtSVBegPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_BegAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_BegAmount = Val(txtSVBegPYValues.Text)
            End If
            If Val(txtSVAddCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_CYear_AddShares = txtSVAddCYShares.Text
            End If
            If Val(txtSVAddCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_AddAmount = txtSVAddCYValues.Text
            End If
            If Val(txtSVAddPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_AddShares = 0
            Else
                objclsSchduleNote.SNS_PYear_AddShares = txtSVAddPYShares.Text
            End If
            If Val(txtSVAddPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_AddAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_AddAmount = txtSVAddPYValues.Text
            End If
            If Val(txtSVEndCYShares.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_CYear_EndShares = txtSVEndCYShares.Text
            End If
            If Val(txtSVEndCYValues.Text) = 0 Then
                objclsSchduleNote.SNS_CYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_CYear_EndAmount = txtSVEndCYValues.Text
            End If
            If Val(txtSVEndPYShares.Text) = 0 Then
                objclsSchduleNote.SNS_PYear_EndShares = 0
            Else
                objclsSchduleNote.SNS_PYear_EndShares = txtSVEndPYShares.Text
            End If
            If Val(txtSVEndPYValues.Text) = 0 Then
                objclsSchduleNote.SNS_pYear_EndAmount = 0
            Else
                objclsSchduleNote.SNS_pYear_EndAmount = txtSVEndPYValues.Text
            End If
            objclsSchduleNote.SNS_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNS_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNS_Status = "W"
            objclsSchduleNote.SNS_DelFlag = "X"
            objclsSchduleNote.SNS_CRON = DateTime.Today
            objclsSchduleNote.SNS_CrBy = sSession.UserID
            objclsSchduleNote.SNS_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveSecondScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            BindSchedSecondDetails(sSession.AccessCode, sSession.AccessCodeID, "SV")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSTSchedules_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub



    'Third
    Private Sub btnTBEquity_Click(sender As Object, e As EventArgs) Handles btnTBEquity.Click
        Dim iDescId As Integer = 0
        Try
            If txtTBEquityDesc.Text <> "" Then
                'If lblId.Text = 0 Then
                '    'If txtSupplierName.Text <> "" Then
                '    '    bCheck = objFxdAsst.LoadSupplier(sSession.AccessCode, sSession.AccessCodeID, txtSupplierName.Text)
                '    '    If bCheck = True Then
                '    '        lblErrorUp.Text = "Entred Supplier is Already Exist, Please Enter Diffrent Supplier"
                '    '        Exit Sub
                '    '    End If
                '    'End If
                'End If

                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNT_ID = lblId.Text
                Else
                    objclsSchduleNote.SNT_ID = 0
                End If

                objclsSchduleNote.SNT_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNT_Description = txtTBEquityDesc.Text
                objclsSchduleNote.SNT_Category = "TBE"
                If Val(txtTBEquity_CYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_CYear_Shares = txtTBEquity_CYShares.Text
                End If
                If Val(txtTBEquity_CYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_CYear_Amount = txtTBEquity_CYAmount.Text
                End If
                If Val(txtTBEquity_PYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_pYear_Shares = txtTBEquity_PYShares.Text
                End If
                If Val(txtTBEquity_PYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_pYear_Amount = txtTBEquity_PYAmount.Text
                End If
                objclsSchduleNote.SNT_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNT_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNT_Status = "W"
                objclsSchduleNote.SNT_DelFlag = "X"
                objclsSchduleNote.SNT_CRON = DateTime.Today
                objclsSchduleNote.SNT_CrBy = sSession.UserID
                objclsSchduleNote.SNT_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveThirdScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If objclsSchduleNote.SNT_ID = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearTBEquity()
                gvTBEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
                gvTBEquity.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
    Public Sub ClearTBEquity()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtTBEquityDesc.Text = "" : txtTBEquity_CYShares.Text = "" : txtTBEquity_CYAmount.Text = "" : txtTBEquity_PYShares.Text = ""
            txtTBEquity_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnTBPref_Click(sender As Object, e As EventArgs) Handles btnTBPref.Click
        Dim iDescId As Integer = 0
        Try
            If txtTBPrefDesc.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNT_ID = lblId.Text
                Else
                    objclsSchduleNote.SNT_ID = 0
                End If

                objclsSchduleNote.SNT_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNT_Description = txtTBPrefDesc.Text
                objclsSchduleNote.SNT_Category = "TBp"
                If Val(txtTBPref_CYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_CYear_Shares = txtTBPref_CYShares.Text
                End If
                If Val(txtTBPref_CYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_CYear_Amount = txtTBPref_CYAmount.Text
                End If
                If Val(txtTBPref_PYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_pYear_Shares = txtTBPref_PYShares.Text
                End If
                If Val(txtTBPref_PYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_pYear_Amount = txtTBPref_PYAmount.Text
                End If
                objclsSchduleNote.SNT_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNT_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNT_Status = "W"
                objclsSchduleNote.SNT_DelFlag = "X"
                objclsSchduleNote.SNT_CRON = DateTime.Today
                objclsSchduleNote.SNT_CrBy = sSession.UserID
                objclsSchduleNote.SNT_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveThirdScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearTBpREF()
                gvTBPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                gvTBPref.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
    Public Sub ClearTBpREF()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtTBPrefDesc.Text = "" : txtTBPref_CYShares.Text = "" : txtTBPref_CYAmount.Text = "" : txtTBPref_PYShares.Text = ""
            txtTBPref_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnTEEquity_Click(sender As Object, e As EventArgs) Handles btnTEEquity.Click
        Dim iDescId As Integer = 0
        Try
            If txtTEEquityDesc.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNT_ID = lblId.Text
                Else
                    objclsSchduleNote.SNT_ID = 0
                End If

                objclsSchduleNote.SNT_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNT_Description = txtTEEquityDesc.Text
                objclsSchduleNote.SNT_Category = "TEE"
                If Val(txtTEEquity_CYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_CYear_Shares = txtTEEquity_CYShares.Text
                End If
                If Val(txtTEEquity_CYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_CYear_Amount = txtTEEquity_CYAmount.Text
                End If
                If Val(txtTEEquity_PYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_pYear_Shares = txtTEEquity_PYShares.Text
                End If
                If Val(txtTEEquity_PYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_pYear_Amount = txtTEEquity_PYAmount.Text
                End If
                objclsSchduleNote.SNT_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNT_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNT_Status = "W"
                objclsSchduleNote.SNT_DelFlag = "X"
                objclsSchduleNote.SNT_CRON = DateTime.Today
                objclsSchduleNote.SNT_CrBy = sSession.UserID
                objclsSchduleNote.SNT_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveThirdScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearTEEquity()
                gvTEEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, objclsSchduleNote.SNT_Category)
                gvTEEquity.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearTEEquity()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtTEEquityDesc.Text = "" : txtTEEquity_CYShares.Text = "" : txtTEEquity_CYAmount.Text = "" : txtTEEquity_PYShares.Text = ""
            txtTEEquity_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnTEPref_Click(sender As Object, e As EventArgs) Handles btnTEPref.Click
        Dim iDescId As Integer = 0
        Try
            If txtTEPrefDesc.Text <> "" Then


                If Val(lblId.Text) <> 0 Then
                    objclsSchduleNote.SNT_ID = lblId.Text
                Else
                    objclsSchduleNote.SNT_ID = 0
                End If

                objclsSchduleNote.SNT_CustId = ddlCustomerName.SelectedValue
                objclsSchduleNote.SNT_Description = txtTEPrefDesc.Text
                objclsSchduleNote.SNT_Category = "TEP"
                If Val(txtTEPref_CYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_CYear_Shares = txtTEPref_CYShares.Text
                End If
                If Val(txtTEPref_CYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_CYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_CYear_Amount = txtTEPref_CYAmount.Text
                End If
                If Val(txtTEPref_PYShares.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Shares = 0
                Else
                    objclsSchduleNote.SNT_pYear_Shares = txtTEPref_PYShares.Text
                End If
                If Val(txtTEPref_PYAmount.Text) = 0 Then
                    objclsSchduleNote.SNT_pYear_Amount = 0
                Else
                    objclsSchduleNote.SNT_pYear_Amount = txtTEPref_PYAmount.Text
                End If
                objclsSchduleNote.SNT_YearID = ddlFinancialYear.SelectedValue
                objclsSchduleNote.SNT_CompID = sSession.AccessCodeID
                objclsSchduleNote.SNT_Status = "W"
                objclsSchduleNote.SNT_DelFlag = "X"
                objclsSchduleNote.SNT_CRON = DateTime.Today
                objclsSchduleNote.SNT_CrBy = sSession.UserID
                objclsSchduleNote.SNT_IPAddress = sSession.IPAddress

                iDescId = objclsSchduleNote.SaveThirdScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
                If lblId.Text = 0 Then
                    lblModalValidationMsg.Text = "Successfully saved"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                Else
                    lblModalValidationMsg.Text = "Successfully Updated"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                ClearTBpREF()
                gvTEPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEP")
                gvTEPref.DataBind()

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFDFS_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearTEpREF()
        Dim dt As New DataTable
        Try
            lblId.Text = 0
            txtTEPrefDesc.Text = "" : txtTEPref_CYShares.Text = "" : txtTEPref_CYAmount.Text = "" : txtTEPref_PYShares.Text = ""
            txtTEPref_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '''Description
    '''

    Public Sub BindDescriptionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sSchedType As String)
        Dim dt As New DataTable
        Try
            If sSchedType = "cTerms" Then
                dt = objclsSchduleNote.getDesciptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SND_Description")) <> 0 Then
                        txtDescCTermsEquity.Text = dt.Rows(0)("SND_Description")
                    Else
                        txtDescCTermsEquity.Text = ""
                    End If
                End If
            ElseIf sSchedType = "dPref" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSSBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSSBegCYShares.Text = ""
                    End If
                End If
            ElseIf sSchedType = "ST" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSTBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSTBegCYShares.Text = ""
                    End If
                End If
            ElseIf sSchedType = "SV" Then
                dt = objclsSchduleNote.getSchedSecondNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSchedType)
                If dt.Rows.Count > 0 Then
                    If (dt.Rows(0)("SNS_CYear_BegShares")) <> 0 Then
                        txtSVBegCYShares.Text = dt.Rows(0)("SNS_CYear_BegShares")
                    Else
                        txtSVBegCYShares.Text = ""
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnDescCTermsEquity_Click(sender As Object, e As EventArgs) Handles btnDescCTermsEquity.Click
        Dim iDescId As Integer = 0
        Try

            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SND_ID = 0

            Else
                objclsSchduleNote.SND_ID = lblId.Text
            End If
            objclsSchduleNote.SND_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SND_Description = txtDescCTermsEquity.Text
            objclsSchduleNote.SND_Category = "cEquity"
            objclsSchduleNote.SND_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SND_CompID = sSession.AccessCodeID
            objclsSchduleNote.SND_Status = "W"
            objclsSchduleNote.SND_DelFlag = "X"
            objclsSchduleNote.SND_CRON = DateTime.Today
            objclsSchduleNote.SND_CrBy = sSession.UserID
            objclsSchduleNote.SND_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveaDescScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            '    BindDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, "cEquity")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescCTermsEquity_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDescDtermsPref_Click(sender As Object, e As EventArgs) Handles btnDescDtermsPref.Click
        Dim iDescId As Integer = 0
        Try

            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SND_ID = 0
            Else
                objclsSchduleNote.SND_ID = 0

            End If
            objclsSchduleNote.SND_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SND_Description = txtDescDTermsPref.Text
            objclsSchduleNote.SND_Category = "dPref"
            objclsSchduleNote.SND_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SND_CompID = sSession.AccessCodeID
            objclsSchduleNote.SND_Status = "W"
            objclsSchduleNote.SND_DelFlag = "X"
            objclsSchduleNote.SND_CRON = DateTime.Today
            objclsSchduleNote.SND_CrBy = sSession.UserID
            objclsSchduleNote.SND_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveaDescScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If objclsSchduleNote.SND_ID = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            '    BindDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, "dPref")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescDtermsPref_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnDescFShares_Click(sender As Object, e As EventArgs) Handles btnDescFShares.Click
        Dim iDescId As Integer = 0
        Try

            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SND_ID = lblId.Text
            Else
                objclsSchduleNote.SND_ID = 0
            End If
            objclsSchduleNote.SND_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SND_Description = txtDescFShares.Text
            objclsSchduleNote.SND_Category = "fShares"
            objclsSchduleNote.SND_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SND_CompID = sSession.AccessCodeID
            objclsSchduleNote.SND_Status = "W"
            objclsSchduleNote.SND_DelFlag = "X"
            objclsSchduleNote.SND_CRON = DateTime.Today
            objclsSchduleNote.SND_CrBy = sSession.UserID
            objclsSchduleNote.SND_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveaDescScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If objclsSchduleNote.SND_ID = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            '    BindDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, "fShares")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescFShares_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnFSCYShares_Click(sender As Object, e As EventArgs) Handles btnFSCYShares.Click
        Dim iDescId As Integer = 0
        Try
            'If lblId.Text = 0 Then
            'End If
            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNFT_ID = lblId.Text
            Else
                objclsSchduleNote.SNFT_ID = 0
            End If
            objclsSchduleNote.SNFT_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNFT_Description = txtFSCYEquityDesc.Text
            objclsSchduleNote.SNFT_Category = "FSC"
            objclsSchduleNote.SNFT_NumShares = txtFSCYShares.Text
            objclsSchduleNote.SNFT_TotalShares = txtFSCYTotShares.Text
            objclsSchduleNote.SNFT_ChangedShares = txtFSCYChangedShares.Text
            objclsSchduleNote.SNFT_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNFT_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNFT_Status = "W"
            objclsSchduleNote.SNFT_DelFlag = "X"
            objclsSchduleNote.SNFT_CRON = DateTime.Today
            objclsSchduleNote.SNFT_CrBy = sSession.UserID
            objclsSchduleNote.SNFT_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveFourthScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If objclsSchduleNote.SNFT_ID = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            ClearFSC1()
            gvFSCYShares.DataSource = objclsSchduleNote.getSchedFourthNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
            gvFSCYShares.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescFShares_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFSC()
        Dim dt As New DataTable
        Try
            lblId.Text = ""
            txtFSCYEquityDesc.Text = "" : txtFSCYShares.Text = "" : txtFSCYTotShares.Text = "" : txtFSCYChangedShares.Text = ""
            txtTBPref_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearFSC1()
        Dim dt As New DataTable
        Try
            lblId.Text = ""
            txtFSCYEquityDesc.Text = "" : txtFSCYShares.Text = "" : txtFSCYTotShares.Text = "" : txtFSCYChangedShares.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnFSpYpREF_Click(sender As Object, e As EventArgs) Handles btnFSpYpREF.Click
        Dim iDescId As Integer = 0
        Try
            'If lblId.Text = 0 Then
            'End If
            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SNFT_ID = lblId.Text
            Else
                objclsSchduleNote.SNFT_ID = 0
            End If
            objclsSchduleNote.SNFT_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SNFT_Description = txtFSPYREFDesc.Text
            objclsSchduleNote.SNFT_Category = "FSP"
            objclsSchduleNote.SNFT_NumShares = txtFSPYShares.Text
            objclsSchduleNote.SNFT_TotalShares = txtFSPYTotShares.Text
            objclsSchduleNote.SNFT_ChangedShares = txtFSPYChangedShares.Text
            objclsSchduleNote.SNFT_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SNFT_CompID = sSession.AccessCodeID
            objclsSchduleNote.SNFT_Status = "W"
            objclsSchduleNote.SNFT_DelFlag = "X"
            objclsSchduleNote.SNFT_CRON = DateTime.Today
            objclsSchduleNote.SNFT_CrBy = sSession.UserID
            objclsSchduleNote.SNFT_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveFourthScheduleNoteDetails1(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If objclsSchduleNote.SNFT_ID = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            ClearFSC()
            gvFSPYREF.DataSource = objclsSchduleNote.getSchedFourthNoteDetails1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
            gvFSPYREF.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnFSpYpREF_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ClearFSP()
        Dim dt As New DataTable
        Try
            lblId.Text = ""
            txtFSPYREFDesc.Text = "" : txtFSPYShares.Text = "" : txtFSPYTotShares.Text = "" : txtFSPYChangedShares.Text = ""
            txtTBPref_PYAmount.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnFootNote_Click(sender As Object, e As EventArgs) Handles btnFootNote.Click
        Dim iDescId As Integer = 0
        Try

            If Val(lblId.Text) <> 0 Then
                objclsSchduleNote.SND_ID = lblId.Text
            Else
                objclsSchduleNote.SND_ID = 0
            End If
            objclsSchduleNote.SND_CustId = ddlCustomerName.SelectedValue
            objclsSchduleNote.SND_Description = txtFootNote.Text
            objclsSchduleNote.SND_Category = "footNote"
            objclsSchduleNote.SND_YearID = ddlFinancialYear.SelectedValue
            objclsSchduleNote.SND_CompID = sSession.AccessCodeID
            objclsSchduleNote.SND_Status = "W"
            objclsSchduleNote.SND_DelFlag = "X"
            objclsSchduleNote.SND_CRON = DateTime.Today
            objclsSchduleNote.SND_CrBy = sSession.UserID
            objclsSchduleNote.SND_IPAddress = sSession.IPAddress
            iDescId = objclsSchduleNote.SaveaDescScheduleNoteDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSchduleNote)
            If lblId.Text = 0 Then
                lblModalValidationMsg.Text = "Successfully saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblModalValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "ScrollToFootNote", "myFunction();", True)
            'ClientScript.RegisterStartupScript(Me.GetType(), "ScrollToTextbox", "ScrollToTextbox();", True)

            '    BindDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, "fShares")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescFShares_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFAuthorised_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFAuthorised.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblID"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblFAthorisedDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFACYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblFAPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFAuthorised.Text = lblDescName.Text
                txtFAuthorisedCYamt.Text = lblCYear_Amount.Text
                txtFAuthorisedPYamt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFAuthorised()
                gvFAuthorised.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
                gvFAuthorised.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFAuthorised_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvFAuthorised_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFAuthorised.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFAuthorised_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFAuthorised.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFAuthorised_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFAuthorised.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFISubscribed_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFISubscribed.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblFISid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblFIAthorisedDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFICYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblFIPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFIssuedSubscribed.Text = lblDescName.Text
                txtFISubscribedCYamt.Text = lblCYear_Amount.Text
                txtFISubscribedPYamt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFIssuedSubscribed()
                gvFISubscribed.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
                gvFISubscribed.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFAuthorised_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFISubscribed_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFISubscribed.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFISubscribed_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFISubscribed.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFISubscribed_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFISubscribed.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFAIssued_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFAIssued.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblFAISid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblFAIssuedDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFAIssuedCYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblFAIssuedPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFAIssued.Text = lblDescName.Text
                txtFAIssuedCYamt.Text = lblCYear_Amount.Text
                txtFAIssuedPYamt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFIssuedSubscribed()
                gvFAIssued.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
                gvFAIssued.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFAuthorised_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFAIssued_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFAIssued.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFAIssued_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFAIssued.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFAIssued_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFAIssued.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFBSubscribed_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFBSubscribed.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblFBid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblFBDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFBCYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblFBCYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFBSubscribed.Text = lblDescName.Text
                txtFBSubCYAmt.Text = lblCYear_Amount.Text
                txtFBSubPYAmt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFBSubscribed()
                gvFBSubscribed.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FB")
                gvFBSubscribed.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvgvFBSubscribed_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFBSubscribed_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFBSubscribed.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFBSubscribed_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFBSubscribed.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFBSubscribed_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFBSubscribed.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFCCUnpaid_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFCCUnpaid.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblccid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblCCDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblCCCYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblCCPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFCCUnpaid.Text = lblDescName.Text
                txtFCCUnpaidCYamt.Text = lblCYear_Amount.Text
                txtFCCUnpaidPYamt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFBSubscribed()
                gvFCCUnpaid.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
                gvFCCUnpaid.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFCCUnpaid_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFCCUnpaid_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFCCUnpaid.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFCCUnpaid_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFCCUnpaid.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFCCUnpaid_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFCCUnpaid.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFDFS_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFDFS.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Amount As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblFDFSid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblFDFSDesc"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFDFSCYear_Amount"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblFDFSPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtFDFS.Text = lblDescName.Text
                txtFDFSCYamt.Text = lblCYear_Amount.Text
                txtFDFSPYamt.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearFBSubscribed()
                gvFDFS.DataSource = objclsSchduleNote.getSchedFirstNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
                gvFDFS.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFCCUnpaid_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvFDFS_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFDFS.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvFDFS_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFDFS.RowDataBound
        Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvFDFS_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFDFS.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvTBEquity_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTBEquity.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
        Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblTBEquityid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblTBEquityDesc"), Label)
            lblCYear_Shares = DirectCast(clickedRow.FindControl("lblTBEquityCYear_Shares"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblTBEquityCYear_Amount"), Label)
            lblPYear_Shares = DirectCast(clickedRow.FindControl("lblTBEquityPYear_Shares"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblTBEquityPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtTBEquityDesc.Text = lblDescName.Text
                txtTBEquity_CYShares.Text = lblCYear_Shares.Text
                txtTBEquity_CYAmount.Text = lblCYear_Amount.Text
                txtTBEquity_PYShares.Text = lblPYear_Shares.Text
                txtTBEquity_PYAmount.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearTBEquity()
                gvTBEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
                gvTBEquity.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTBEquity_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvTBEquity_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvTBEquity.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvTBEquity_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTBEquity.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvTBEquity_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTBEquity.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub


    Private Sub gvTBPref_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTBPref.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
        Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblTBPrefid"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblTBPrefDesc"), Label)
            lblCYear_Shares = DirectCast(clickedRow.FindControl("lblTBPrefCYear_Shares"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblTBPrefCYear_Amount"), Label)
            lblPYear_Shares = DirectCast(clickedRow.FindControl("lblTBPrefPYear_Shares"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblTBPrefPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtTBPrefDesc.Text = lblDescName.Text
                txtTBPref_CYShares.Text = lblCYear_Shares.Text
                txtTBPref_CYAmount.Text = lblCYear_Amount.Text
                txtTBPref_PYShares.Text = lblPYear_Shares.Text
                txtTBPref_PYAmount.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearTBpREF()
                gvTBPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBp")
                gvTBPref.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTBPref_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvTBPref_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvTBPref.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvTBPref_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTBPref.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvTBPref_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTBPref.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvTEEquity_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTEEquity.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
        Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblTEEquityId"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblTEEquityDesc"), Label)
            lblCYear_Shares = DirectCast(clickedRow.FindControl("lblTEEquityCYear_Shares"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblTEEquityCYear_Amount"), Label)
            lblPYear_Shares = DirectCast(clickedRow.FindControl("lblTEEquityPYear_Shares"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblTEEquityPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtTEEquityDesc.Text = lblDescName.Text
                txtTEEquity_CYShares.Text = lblCYear_Shares.Text
                txtTEEquity_CYAmount.Text = lblCYear_Amount.Text
                txtTEEquity_PYShares.Text = lblPYear_Shares.Text
                txtTEEquity_PYAmount.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearTEEquity()
                gvTEEquity.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
                gvTEEquity.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTEEquity_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvTEEquity_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvTEEquity.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvTEEquity_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTEEquity.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvTEEquity_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTEEquity.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub gvTEPref_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTEPref.RowCommand
        Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
        Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCID = DirectCast(clickedRow.FindControl("lblTEPrefId"), Label)
            lblDescName = DirectCast(clickedRow.FindControl("lblTEPrefDesc"), Label)
            lblCYear_Shares = DirectCast(clickedRow.FindControl("lblTEPrefCYear_Shares"), Label)
            lblCYear_Amount = DirectCast(clickedRow.FindControl("lblTEPrefCYear_Amount"), Label)
            lblPYear_Shares = DirectCast(clickedRow.FindControl("lblTEPrefPYear_Shares"), Label)
            lblPYear_Amount = DirectCast(clickedRow.FindControl("lblTEPrefPYear_Amount"), Label)

            If e.CommandName.Equals("Edit") Then
                lblId.Text = lblCID.Text
                txtTEPrefDesc.Text = lblDescName.Text
                txtTEPref_CYShares.Text = lblCYear_Shares.Text
                txtTEPref_CYAmount.Text = lblCYear_Amount.Text
                txtTEPref_PYShares.Text = lblPYear_Shares.Text
                txtTEPref_PYAmount.Text = lblPYear_Amount.Text
            ElseIf e.CommandName.Equals("Delete") Then
                objclsSchduleNote.DeleteSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lblModalValidationMsg.Text = "Successfully Deleted"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterValidation').modal('show');", True)
                ClearTBpREF()
                gvTEPref.DataSource = objclsSchduleNote.getSchedThirdNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                gvTEPref.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTEEquity_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvTEPref_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvTEPref.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvTEPref_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTEPref.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvTEPref_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvTEPref.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvFSCYShares_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFSCYShares.RowCommand
    '    Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
    '    Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
    '    Try
    '        lblError.Text = ""
    '        Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '        lblCID = DirectCast(clickedRow.FindControl("lblFSCYId"), Label)
    '        lblDescName = DirectCast(clickedRow.FindControl("lblFSCYPromoterName"), Label)
    '        lblCYear_Shares = DirectCast(clickedRow.FindControl("lblFSCYCYShares"), Label)
    '        lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFSCYTotShares"), Label)
    '        lblPYear_Shares = DirectCast(clickedRow.FindControl("lblFSCYChangedShares"), Label)


    '        If e.CommandName.Equals("Edit") Then
    '            lblId.Text = lblCID.Text
    '            txtFSCYEquityDesc.Text = lblDescName.Text
    '            txtFSCYShares.Text = lblCYear_Shares.Text
    '            txtFSCYTotShares.Text = lblCYear_Amount.Text
    '            txtFSCYChangedShares.Text = lblPYear_Shares.Text

    '        ElseIf e.CommandName.Equals("Delete") Then
    '            objclsSchduleNote.DeleteSchedFourthNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
    '            ClearFSP()
    '            gvFSCYShares.DataSource = objclsSchduleNote.getSchedFourthNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
    '            gvFSCYShares.DataBind()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFSCYShares_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub gvFSCYShares_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFSCYShares.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvFSCYShares_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFSCYShares.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvFSCYShares_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFSCYShares.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvFSPYREF_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvFSPYREF.RowCommand
    '    Dim lblCID As New Label, lblDescName As New Label, lblCYear_Shares As New Label, lblCYear_Amount As New Label
    '    Dim lblPYear_Shares As New Label, lblPYear_Amount As New Label
    '    Try
    '        lblError.Text = ""
    '        Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
    '        lblCID = DirectCast(clickedRow.FindControl("lblFSpYId"), Label)
    '        lblDescName = DirectCast(clickedRow.FindControl("lblFSpYPromoterName"), Label)
    '        lblCYear_Shares = DirectCast(clickedRow.FindControl("lblFSpYCYShares"), Label)
    '        lblCYear_Amount = DirectCast(clickedRow.FindControl("lblFSpYTotShares"), Label)
    '        lblPYear_Shares = DirectCast(clickedRow.FindControl("lblFSpYChangedShares"), Label)


    '        If e.CommandName.Equals("Edit") Then
    '            lblId.Text = lblCID.Text
    '            txtFSPYREFDesc.Text = lblDescName.Text
    '            txtFSPYShares.Text = lblCYear_Shares.Text
    '            txtFSPYTotShares.Text = lblCYear_Amount.Text
    '            txtFSPYChangedShares.Text = lblPYear_Shares.Text

    '        ElseIf e.CommandName.Equals("Delete") Then
    '            objclsSchduleNote.DeleteSchedFourthNoteDetails(sSession.AccessCode, sSession.AccessCodeID, lblCID.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
    '            ClearFSC()
    '            gvFSPYREF.DataSource = objclsSchduleNote.getSchedFourthNoteDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
    '            gvFSPYREF.DataBind()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvFSPYREF_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub gvFSPYREF_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvFSPYREF.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub
    'Private Sub gvFSPYREF_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvFSPYREF.RowDataBound
    '    Dim imgbtndelete As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
    '            imgbtndelete.ImageUrl = "~/Images/DeActivate24.png"
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub gvFSPYREF_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvFSPYREF.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    ddlCustomerName_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("UploadTrailbalanceSchedule.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim dt1 As DataTable, dt2 As DataTable, dt3 As DataTable, dt4 As DataTable, dt5 As DataTable, dt6 As DataTable
        Dim dtS1 As DataTable, dtS2 As DataTable, dtS3 As DataTable, dtS4 As DataTable
        Dim dtT1 As DataTable, dtT2 As DataTable, dtT3 As DataTable, dtT4 As DataTable, dtT5 As DataTable, dtT6 As DataTable
        Dim dtT7 As DataTable, dtT8 As DataTable, dtT9 As DataTable
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dt1 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
            Dim rds1 As New ReportDataSource("DataSet1", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)

            dt2 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
            Dim rds2 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds2)

            dt3 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
            Dim rds3 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds3)

            dt4 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
            Dim rds4 As New ReportDataSource("DataSet4", dt4)
            ReportViewer1.LocalReport.DataSources.Add(rds4)

            dt5 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
            Dim rds5 As New ReportDataSource("DataSet5", dt5)
            ReportViewer1.LocalReport.DataSources.Add(rds5)

            dt6 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
            Dim rds6 As New ReportDataSource("DataSet6", dt6)
            ReportViewer1.LocalReport.DataSources.Add(rds6)


            dtS1 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SF")
            Dim rds7 As New ReportDataSource("ScheduleNote_Second1", dtS1)
            ReportViewer1.LocalReport.DataSources.Add(rds7)

            dtS2 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SS")
            Dim rds8 As New ReportDataSource("ScheduleNote_Second2", dtS2)
            ReportViewer1.LocalReport.DataSources.Add(rds8)

            dtS3 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "ST")
            Dim rds9 As New ReportDataSource("ScheduleNote_Second3", dtS3)
            ReportViewer1.LocalReport.DataSources.Add(rds9)

            dtS4 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SV")
            Dim rds10 As New ReportDataSource("ScheduleNote_Second4", dtS4)
            ReportViewer1.LocalReport.DataSources.Add(rds10)

            dtT1 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
            Dim rds11 As New ReportDataSource("ScheduleNote_Third1", dtT1)
            ReportViewer1.LocalReport.DataSources.Add(rds11)

            dtT2 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
            Dim rds12 As New ReportDataSource("ScheduleNote_Third2", dtT2)
            ReportViewer1.LocalReport.DataSources.Add(rds12)

            dtT2 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "cEquity")
            Dim rds13 As New ReportDataSource("ScheduleNote_Desc", dtT2)
            ReportViewer1.LocalReport.DataSources.Add(rds13)

            dtT3 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "dPref")
            Dim rds14 As New ReportDataSource("ScheduleNote_Desc1", dtT3)
            ReportViewer1.LocalReport.DataSources.Add(rds14)

            dtT4 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
            Dim rds15 As New ReportDataSource("ScheduleNote_Third3", dtT4)
            ReportViewer1.LocalReport.DataSources.Add(rds15)

            dtT5 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
            Dim rds16 As New ReportDataSource("ScheduleNote_Third4", dtT5)
            ReportViewer1.LocalReport.DataSources.Add(rds16)

            dtT6 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "fShares")
            Dim rds17 As New ReportDataSource("ScheduleNote_Desc2", dtT6)
            ReportViewer1.LocalReport.DataSources.Add(rds17)

            dtT7 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
            Dim rds18 As New ReportDataSource("ScheduleNote_Fourth", dtT7)
            ReportViewer1.LocalReport.DataSources.Add(rds18)

            dtT8 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
            Dim rds19 As New ReportDataSource("ScheduleNote_Fourth1", dtT8)
            ReportViewer1.LocalReport.DataSources.Add(rds19)

            dtT9 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "footNote")
            Dim rds20 As New ReportDataSource("ScheduleNote_Desc3", dtT9)
            ReportViewer1.LocalReport.DataSources.Add(rds20)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptSchduleNote.rdlc")
            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)
            Dim FYear As ReportParameter() = New ReportParameter() {New ReportParameter("FYear", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(FYear)
            Dim CurrentYear = New ReportParameter() {New ReportParameter("CurrentYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
            ReportViewer1.LocalReport.SetParameters(CurrentYear)
            Dim PreviesYear = New ReportParameter() {New ReportParameter("PreviesYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
            ReportViewer1.LocalReport.SetParameters(PreviesYear)


            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=""ScheduleNote " + ddlCustomerName.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dt1 As DataTable, dt2 As DataTable, dt3 As DataTable, dt4 As DataTable, dt5 As DataTable, dt6 As DataTable
        Dim dtS1 As DataTable, dtS2 As DataTable, dtS3 As DataTable, dtS4 As DataTable
        Dim dtT1 As DataTable, dtT2 As DataTable, dtT3 As DataTable, dtT4 As DataTable, dtT5 As DataTable, dtT6 As DataTable
        Dim dtT7 As DataTable, dtT8 As DataTable, dtT9 As DataTable
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dt1 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
            Dim rds1 As New ReportDataSource("DataSet1", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)

            dt2 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
            Dim rds2 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds2)

            dt3 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
            Dim rds3 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds3)

            dt4 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
            Dim rds4 As New ReportDataSource("DataSet4", dt4)
            ReportViewer1.LocalReport.DataSources.Add(rds4)

            dt5 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
            Dim rds5 As New ReportDataSource("DataSet5", dt5)
            ReportViewer1.LocalReport.DataSources.Add(rds5)

            dt6 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
            Dim rds6 As New ReportDataSource("DataSet6", dt6)
            ReportViewer1.LocalReport.DataSources.Add(rds6)


            dtS1 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SF")
            Dim rds7 As New ReportDataSource("ScheduleNote_Second1", dtS1)
            ReportViewer1.LocalReport.DataSources.Add(rds7)

            dtS2 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SS")
            Dim rds8 As New ReportDataSource("ScheduleNote_Second2", dtS2)
            ReportViewer1.LocalReport.DataSources.Add(rds8)

            dtS3 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "ST")
            Dim rds9 As New ReportDataSource("ScheduleNote_Second3", dtS3)
            ReportViewer1.LocalReport.DataSources.Add(rds9)

            dtS4 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SV")
            Dim rds10 As New ReportDataSource("ScheduleNote_Second4", dtS4)
            ReportViewer1.LocalReport.DataSources.Add(rds10)

            dtT1 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
            Dim rds11 As New ReportDataSource("ScheduleNote_Third1", dtT1)
            ReportViewer1.LocalReport.DataSources.Add(rds11)

            dtT2 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
            Dim rds12 As New ReportDataSource("ScheduleNote_Third2", dtT2)
            ReportViewer1.LocalReport.DataSources.Add(rds12)

            dtT2 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "cEquity")
            Dim rds13 As New ReportDataSource("ScheduleNote_Desc", dtT2)
            ReportViewer1.LocalReport.DataSources.Add(rds13)

            dtT3 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "dPref")
            Dim rds14 As New ReportDataSource("ScheduleNote_Desc1", dtT3)
            ReportViewer1.LocalReport.DataSources.Add(rds14)

            dtT4 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
            Dim rds15 As New ReportDataSource("ScheduleNote_Third3", dtT4)
            ReportViewer1.LocalReport.DataSources.Add(rds15)

            dtT5 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
            Dim rds16 As New ReportDataSource("ScheduleNote_Third4", dtT5)
            ReportViewer1.LocalReport.DataSources.Add(rds16)

            dtT6 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "fShares")
            Dim rds17 As New ReportDataSource("ScheduleNote_Desc2", dtT6)
            ReportViewer1.LocalReport.DataSources.Add(rds17)

            dtT7 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
            Dim rds18 As New ReportDataSource("ScheduleNote_Fourth", dtT7)
            ReportViewer1.LocalReport.DataSources.Add(rds18)

            dtT8 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
            Dim rds19 As New ReportDataSource("ScheduleNote_Fourth1", dtT8)
            ReportViewer1.LocalReport.DataSources.Add(rds19)

            dtT9 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "footNote")
            Dim rds20 As New ReportDataSource("ScheduleNote_Desc3", dtT9)
            ReportViewer1.LocalReport.DataSources.Add(rds20)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptSchduleNote.rdlc")
            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)
            Dim FYear As ReportParameter() = New ReportParameter() {New ReportParameter("FYear", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(FYear)
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=""ScheduleNote " + ddlCustomerName.SelectedItem.Text + "-" + ddlFinancialYear.SelectedItem.Text + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkScheduleReport_Click(sender As Object, e As EventArgs) Handles lnkScheduleReport.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Response.Redirect(String.Format("ScheduleReport.aspx?"), False)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
