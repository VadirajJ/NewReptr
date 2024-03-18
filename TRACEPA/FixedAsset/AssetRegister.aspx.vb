Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class AssetRegister
    Inherits System.Web.UI.Page

    Private sFormName As String = "AssetRegister"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGRACeGeneral
    Dim objGen As New clsGRACeGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objAstReg As New ClsAssetRegister
    Private Shared dt As DataTable
    Private objAsst As New ClsAssetMaster
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub pageload(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCustomer()
                LoadFinalcialYear(sSession.AccessCode)
                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        loadAssetType()
                    End If
                End If
                ' dt = Nothing
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYears(sAC, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.ScheduleYearId = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged

        Try
            If sSession.CustomerID <> 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    lblModal.Text = "Do you wish to change Customer?Click Yes to change."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                If ddlCustomerName.SelectedIndex > 0 Then
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    loadAssetType()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    loadAssetType()
                Catch ex As Exception

                End Try
            End If

            Try
                dgRegister.DataSource = Nothing
                dgRegister.DataBind()
                dt = Nothing

                GVAssetTrasOPB.DataSource = Nothing
                GVAssetTrasOPB.DataBind()

                GVAssetTrasAdd.DataSource = Nothing
                GVAssetTrasAdd.DataBind()
            Catch ex As Exception
                Throw
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            lblError.Text = ""
            If sSession.CustomerID <> 0 Then
                ddlCustomerName.SelectedValue = sSession.CustomerID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCustomer() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataSource = dt
            ddlCustomerName.DataTextField = "CUST_NAME"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub BindAssetRegister(ByVal AFAM_AssetType As String)
        Try
            dt = objAstReg.LoadAssetRegister(sSession.AccessCode, sSession.AccessCodeID, AFAM_AssetType, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            dgRegister.DataSource = dt
            dgRegister.DataBind()

            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAssetRegister" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dtAsset As New DataTable
        Try
            dtAsset = objAstReg.loadAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            ddlpAstype.DataTextField = "AM_Description"
            ddlpAstype.DataValueField = "AM_ID"
            ddlpAstype.DataSource = dtAsset
            ddlpAstype.DataBind()
            ddlpAstype.Items.Insert(0, "All Asset Class")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgRegister_PreRender(sender As Object, e As EventArgs) Handles dgRegister.PreRender
        Try
            If dgRegister.Rows.Count > 0 Then
                dgRegister.UseAccessibleHeader = True
                dgRegister.HeaderRow.TableSection = TableRowSection.TableHeader
                dgRegister.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GVAssetTrasOPB_PreRender(sender As Object, e As EventArgs) Handles GVAssetTrasOPB.PreRender
        Try
            If GVAssetTrasOPB.Rows.Count > 0 Then
                GVAssetTrasOPB.UseAccessibleHeader = True
                GVAssetTrasOPB.HeaderRow.TableSection = TableRowSection.TableHeader
                GVAssetTrasOPB.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVAssetTrasOPB_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""
            If dgRegister.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetRegReport.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            '  objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "Excel", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetRegister" + ".xls")
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
            If dgRegister.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalBRRValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetRegReport.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            '  objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "PDF", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetRegister" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgRegister_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgRegister.RowCommand
        Dim ID As Object
        Dim AssetID As Object
        Dim lblID, lblAssetID As New Label
        Dim lblAsset As New Label
        Try
            lblError.Text = ""

            If e.CommandName.Equals("EditFREG") Then

                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblAssetID = DirectCast(clickedRow.FindControl("lblAssetID"), Label)

                ID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblID.Text)))
                AssetID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblAssetID.Text)))
                Response.Redirect(String.Format("~/FixedAsset/AssetMaster.aspx?AFAM_ID={0}&AFAM_AssetType={1}", ID, AssetID), False)
            End If

            If e.CommandName = "Asset" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblAssetID = DirectCast(clickedRow.FindControl("lblAssetID"), Label)


                Dim iTransType As Integer = objAstReg.GetTransType(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, lblID.Text, lblAssetID.Text)
                If iTransType = 1 Then
                    pnlAssetTrasOPB.Visible = True
                    pnlAssetTrasAdd.Visible = False
                    dt = objAstReg.LoadTransOPB(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, lblID.Text, lblAssetID.Text)
                    If dt.Rows.Count > 0 Then
                        GVAssetTrasOPB.DataSource = dt
                        GVAssetTrasOPB.DataBind()
                    Else
                        GVAssetTrasOPB.DataSource = Nothing
                        GVAssetTrasOPB.DataBind()
                    End If
                Else
                    pnlAssetTrasAdd.Visible = True
                    pnlAssetTrasOPB.Visible = False
                    dt = objAstReg.LoadTransOPB(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, lblID.Text, lblAssetID.Text)
                    If dt.Rows.Count > 0 Then
                        GVAssetTrasAdd.DataSource = dt
                        GVAssetTrasAdd.DataBind()
                    Else
                        GVAssetTrasAdd.DataSource = Nothing
                        GVAssetTrasAdd.DataBind()
                    End If

                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgRegister_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgRegister.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Dim lnkDocuments As New LinkButton
        Dim lblTRAssetType As New Label
        Try
            lblTRAssetType = CType(e.Row.FindControl("lblTRStatus"), Label)
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                If lblTRAssetType.Text = "1" Then
                    e.Row.BackColor = System.Drawing.Color.Yellow
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgRegister_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Try
                Response.Redirect(String.Format("~/FixedAsset/AssetMaster.aspx?"), False)
            Catch ex As Exception
                lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
                Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub BtnSearch_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            If ddlpAstype.SelectedIndex > 0 Then
                BindAssetRegister(ddlpAstype.SelectedValue)
            Else
                BindAssetRegister(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnSearch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlpAstype.SelectedIndexChanged
        Try
            dgRegister.DataSource = Nothing
            dgRegister.DataBind()
            dt = Nothing

            GVAssetTrasOPB.DataSource = Nothing
            GVAssetTrasOPB.DataBind()

            GVAssetTrasAdd.DataSource = Nothing
            GVAssetTrasAdd.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlpAstype_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub GVAssetTrasAdd_PreRender(sender As Object, e As EventArgs) Handles GVAssetTrasAdd.PreRender
        Try
            If GVAssetTrasAdd.Rows.Count > 0 Then
                GVAssetTrasAdd.UseAccessibleHeader = True
                GVAssetTrasAdd.HeaderRow.TableSection = TableRowSection.TableHeader
                GVAssetTrasAdd.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVAssetTrasAdd_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            Else
                lblError.Text = "Select FInancial Year."
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
