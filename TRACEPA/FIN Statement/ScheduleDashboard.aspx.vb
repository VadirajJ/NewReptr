Imports System
Imports System.Data
Imports BusinesLayer

Public Class ScheduleDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "ScheduleDashboard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACePermission As New clsGRACePermission
    Private Shared sSession As AllSession
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleReport As New clsScheduleReport
    Private objclsScheduleTemplate As New clsScheduleTemplate

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sModule As String
        Dim iFormID As Integer = 0
        Dim sFormButtons As String
        Dim dtSampleFormat As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sModule = objclsGRACePermission.GetLoginUserModulePermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 1)
                If sModule = "False" Then
                    Response.Redirect("~/Permissions/SysAdminPermissionModule.aspx", False) 'Permissions/SysAdminPermissionModule
                    Exit Sub
                End If
                LoadFinalcialYear(sSession.AccessCode)
                LoadExistingCustomer()
                If sSession.CustomerID <> 0 Then
                    ddlCustName.SelectedValue = sSession.CustomerID
                    If ddlCustName.SelectedIndex > 0 Then
                        ddlCustName_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
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
                sSession.YearID = ddlFinancialYear.SelectedValue
                sSession.YearName = ddlFinancialYear.SelectedItem.Text
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
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
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            sSession.YearID = ddlFinancialYear.SelectedValue
            sSession.YearName = ddlFinancialYear.SelectedItem.Text
            Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Master.aspx", False) 'HomePages/Master
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim Orgtype As Integer = 0
        Dim ProgStatus As String = ""
        Dim dt1 As New System.Data.DataSet
        Try
            UlProgressbas.Visible = False
            lblError.Text = ""
            liCustAssgn.Attributes.Remove("class")
            liRpyFormat.Attributes.Remove("class")
            If ddlCustName.SelectedIndex > 0 Then
                Orgtype = objclsSchduleReport.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
                If Orgtype = 0 Then
                    lblError.Text = "Please assign Customer type to the custmer"
                    Exit Sub
                Else
                    ProgStatus = "AssignC"
                    liCustAssgn.Attributes.Add("class", "is-active")
                End If
                dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, 0, Orgtype)
                UlProgressbas.Visible = True
                sSession.CustomerID = ddlCustName.SelectedValue
                Session("AllSession") = sSession
                ProgStatus = ""
                If dt.Rows.Count > 0 Then
                    liCustAssgn.Attributes.Remove("class")
                    ProgStatus = "FormatC"
                    liRpyFormat.Attributes.Add("class", "is-active")
                Else
                    lblError.Text = "No Data Found. Please create a format"
                    Exit Sub
                End If
                dt1 = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID, 0, 0, "0")
                dt = dt1.Tables(0)
                If dt.Rows.Count > 0 Then
                    liCustAssgn.Attributes.Remove("class")
                    liRpyFormat.Attributes.Remove("class")
                    ProgStatus = "UplaodSchedule"
                    lirptgen.Attributes.Remove("class")
                Else
                    lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                    Exit Sub
                End If
                dt = objUT.LoadItemsfromJE(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID)
                If dt.Rows.Count > 0 Then
                    ProgStatus = "JeExist"
                    'lirptJe.Attributes.Add("class", "is-active")
                    lirptDownload.Attributes.Remove("class")
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class