Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Http.Headers
Imports System.IO
Imports System.Threading.Tasks
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Net
Public Class DigitalDocument
    Inherits System.Web.UI.Page
    Private sFormName As String = "Document Page"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    '  Private objDB As New clsHomeDashboard
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Private Shared sImgPath As String = ""
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnIndex.ImageUrl = "~/Images/Index24.png"
        imgbtnWorkFlow.ImageUrl = "~/Images/sent24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                Getfiles(sSession.UserID)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Async Function Getfiles(ByVal patnerid As Integer) As Task
        Dim dt As New DataTable
        Try
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim URL As String
            URL = String.Format("https://tracepacore.multimedia.interactivedns.com/api/main/getfiles?patnerid={0}", patnerid)
            Dim json As String = (New WebClient).DownloadString(URL)
            dt = JsonConvert.DeserializeObject(Of DataTable)(json)
            If dt.Rows.Count > 0 Then
                gvDocuments.DataSource = dt
                gvDocuments.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Getfiles" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Async Function RemoveFiles(ByVal patnerid As Integer, ByVal filename As String) As Task
        Dim dt As New DataTable
        Try
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim URL As String
            URL = String.Format("https://tracepacore.multimedia.interactivedns.com/api/main/removefile?patnerid={0}&filename={1}", patnerid, filename)
            Dim json As String = (New WebClient).DownloadString(URL)
            dt = JsonConvert.DeserializeObject(Of DataTable)(json)
            Getfiles(patnerid)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "RemoveFiles" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Private Sub gvDocuments_PreRender(sender As Object, e As EventArgs) Handles gvDocuments.PreRender
        Try
            If gvDocuments.Rows.Count > 0 Then
                gvDocuments.UseAccessibleHeader = True
                gvDocuments.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDocuments.FooterRow.TableSection = TableRowSection.TableFooter
            Else
                gvDocuments.HeaderRow.BackColor = System.Drawing.ColorTranslator.FromHtml("#ddeaf9")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocuments_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDocuments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDocuments.RowCommand
        Dim lblImgPath As New Label
        Dim url As String = ""
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            If e.CommandName = "View" Or e.CommandName = "Delete" Then
                If e.CommandName = "View" Then
                    lblImgPath = DirectCast(clickedRow.FindControl("lblfilepath"), Label)
                    url = lblImgPath.Text
                    Dim sExtension As String = Path.GetExtension(lblImgPath.Text)
                    sExtension = sExtension.ToUpper
                    If (sExtension = ".JPG" Or sExtension = ".JPEG" Or sExtension = ".BMP" Or sExtension = ".GIF" Or sExtension = ".PNG") Then
                        pnlImgViewer.Visible = True
                        documentImgViewer.ImageUrl = url
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalDocumentViewer').modal('show');", True)
                    Else
                        pnlImgViewer.Visible = False
                        lblValidationMsg.Text = "Unable to open document"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-success');$('#ModalMSGValidation').modal('show');", True)
                    End If
                End If
                If e.CommandName = "Delete" Then
                    lblImgPath = DirectCast(clickedRow.FindControl("lblfilename"), Label)
                    sImgPath = lblImgPath.Text
                    lblImgdeletionValidationMsg.Text = "Do you really want to delete this Document?"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocuments_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDocuments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDocuments.RowDataBound
        Dim imgbtnView As New ImageButton, imgbtnDelete As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnView = CType(e.Row.FindControl("imgbtnView"), ImageButton)
                imgbtnView.ImageUrl = "~/Images/View16.png"
                imgbtnDelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtnDelete.ImageUrl = "~/Images/Trash16.png"
                imgbtnDelete.ImageAlign = ImageAlign.Middle
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocuments_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles BtnYES.Click
        Try
            lblError.Text = ""
            RemoveFiles(sSession.UserID, sImgPath)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnYES_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        Try
            Exit Sub
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnNo_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDocuments_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvDocuments.RowDeleting
        Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDocuments_RowDeleting" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkSelect As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            If gvDocuments.Rows.Count > 0 Then
                chkAll = CType(sender, CheckBox)
                If chkAll.Checked = True Then
                    For iIndx = 0 To gvDocuments.Rows.Count - 1
                        chkSelect = gvDocuments.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = True
                    Next
                Else
                    For iIndx = 0 To gvDocuments.Rows.Count - 1
                        chkSelect = gvDocuments.Rows(iIndx).FindControl("chkSelect")
                        chkSelect.Checked = False
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnIndex_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndex.Click
        Dim chkSelect As CheckBox
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim lblfileID, lblfilepath, lblfilename, lblFileInfo As Label
        Dim sExtension As String = ""
        Dim oStatus As Object
        Try
            Session("Attachment") = Nothing
            If gvDocuments.Rows.Count > 0 Then
                dt.Columns.Add("ID")
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                For i = 0 To gvDocuments.Rows.Count - 1
                    chkSelect = gvDocuments.Rows(i).FindControl("chkSelect")
                    If chkSelect.Checked = True Then
                        oStatus = 1
                        lblfileID = gvDocuments.Rows(i).FindControl("lblPKID")
                        lblfilepath = gvDocuments.Rows(i).FindControl("lblfilepath")
                        lblfilename = gvDocuments.Rows(i).FindControl("lblfilename")
                        lblFileInfo = gvDocuments.Rows(i).FindControl("lblfileInfo")
                        sExtension = Path.GetExtension(lblfilename.Text)
                        dRow = dt.NewRow()
                        dRow("ID") = lblfileID.Text
                        dRow("FilePath") = lblfilepath.Text
                        dRow("FileName") = lblfilename.Text
                        dRow("CreatedOn") = lblFileInfo.Text
                        dRow("Extension") = sExtension
                        dt.Rows.Add(dRow)
                    End If
                Next
            End If
            If Val(oStatus) > 0 Then
                oStatus = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(1))
                Session("Attachment") = dt
                Response.Redirect(String.Format("~/DigitalFilling/Indexing.aspx?Status={0}&flag={1}", oStatus, "1"), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndex_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWorkFlow_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWorkFlow.Click
        Dim chkSelect As CheckBox
        Dim dRow As DataRow
        Dim dt As New DataTable
        Dim lblfileID, lblfilepath, lblfilename As Label
        Dim oStatus As Object
        Try
            Session("Attachment") = Nothing
            If gvDocuments.Rows.Count > 0 Then
                dt.Columns.Add("ID")
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                For i = 0 To gvDocuments.Rows.Count - 1
                    chkSelect = gvDocuments.Rows(i).FindControl("chkSelect")
                    If chkSelect.Checked = True Then
                        oStatus = 1
                        lblfileID = gvDocuments.Rows(i).FindControl("lblPKID")
                        lblfilepath = gvDocuments.Rows(i).FindControl("lblfilepath")
                        lblfilename = gvDocuments.Rows(i).FindControl("lblfilename")
                        dRow = dt.NewRow()
                        dRow("ID") = lblfileID.Text
                        dRow("FilePath") = lblfilepath.Text
                        dRow("FileName") = lblfilename.Text
                        dt.Rows.Add(dRow)
                    End If
                Next
            End If
            If Val(oStatus) > 0 Then
                oStatus = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(1))
                Session("Attachment") = dt
                Response.Redirect(String.Format("~/DigitalFilling/Inward.aspx?StatusAtchFlg={0}", oStatus), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWorkFlow_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class