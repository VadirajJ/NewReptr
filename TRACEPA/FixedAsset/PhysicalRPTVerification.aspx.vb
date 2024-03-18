Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports System.Globalization
Imports Microsoft.Reporting.WebForms
Partial Class PhysicalRPTVerification
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "PhysicalRPTVerification"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objExcelView As New ClsPhysicalRPTVerification
    Private Shared sSession As AllSession
    Private Shared dttable As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadBranch()
                dttable = Nothing
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadBranch()
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objExcelView.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "Org_Parent"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBranch" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvExcelView_PreRender(sender As Object, e As EventArgs) Handles GvExcelView.PreRender
        Try
            lblError.Text = ""
            If GvExcelView.Rows.Count > 0 Then
                GvExcelView.UseAccessibleHeader = True
                GvExcelView.HeaderRow.TableSection = TableRowSection.TableHeader
                GvExcelView.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvExcelView_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged1(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvExcelView.Rows.Count - 1
                    chkField = GvExcelView.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvExcelView.Rows.Count - 1
                    chkField = GvExcelView.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged1" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            lblError.Text = ""
            GvExcelView.DataSource = Nothing : GvExcelView.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAccBrnch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccBrnch.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlAccBrnch.SelectedIndex > 0 Then
                LoadAssetType()
                LoadArea(ddlAccBrnch.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccZone.Items.Clear() : ddlAccRgn.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccBrnch_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadAssetType()
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objExcelView.LoadAssetType1(sSession.AccessCode, sSession.AccessCodeID)
            drpAstype.DataTextField = "AM_Description"
            drpAstype.DataValueField = "AM_ID"
            drpAstype.DataSource = dt
            drpAstype.DataBind()
            drpAstype.Items.Insert(0, "Select AssetType")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iBrnch As Integer)
        Dim dt As New DataTable
        Try
            dt = objExcelView.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iBrnch)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "Org_Parent"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "Select Area")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadArea" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            lblError.Text = ""
            If drpAstype.SelectedIndex = 0 Then
                lblFXOPBalExcelViewMsg.Text = "Select Asset Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASFXDOpExcelView').modal('show');", True)
                Exit Sub
            Else
                dtdetails = objExcelView.LoadAllReportDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, drpAstype.SelectedValue)
            End If
            If dtdetails.Rows.Count = 0 Or dtdetails Is Nothing Then
                lblFXOPBalExcelViewMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/PhysicalRPTVerification.rdlc")

            Dim sPvrpt As String = "Physical Verification Report"
            Dim PhysicalVerification As ReportParameter() = New ReportParameter() {New ReportParameter("PhysicalVerification", sPvrpt)}
            ReportViewer1.LocalReport.SetParameters(PhysicalVerification)

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=PhysicalRepot" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            If drpAstype.SelectedIndex = 0 Then
                lblFXOPBalExcelViewMsg.Text = "Select Asset Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "$('#divMsgType').addClass('alert alert-info');$('#ModalFASFXDOpExcelView').modal('show');", True)
                Exit Sub
            End If
            If dttable.Rows.Count = 0 Or dttable Is Nothing Then
                lblFXOPBalExcelViewMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dttable)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/PhysicalRPTVerification.rdlc")
            Dim sPvrpt As String = "Physical Verification Report"
            Dim PhysicalVerification As ReportParameter() = New ReportParameter() {New ReportParameter("PhysicalVerification", sPvrpt)}
            ReportViewer1.LocalReport.SetParameters(PhysicalVerification)

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=PhysicalRepot" + ".PDF")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub drpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpAstype.SelectedIndexChanged
        Try
            If drpAstype.SelectedIndex > 0 Then
                dttable = objExcelView.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, drpAstype.SelectedValue)
                GvExcelView.DataSource = dttable
                GvExcelView.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "drpAstype_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadRegion(ddlAccArea.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccArea As Integer)
        Dim dt As New DataTable
        Try
            dt = objExcelView.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccArea)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "Org_Parent"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "Select Region")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRegion" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadAccZone(ddlAccRgn.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadAccZone(ByVal iRegion As Integer)
        Dim dt As New DataTable
        Try
            dt = objExcelView.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID, iRegion)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "Org_Parent"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAccZone" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
