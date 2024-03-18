Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Public Class UploadStockEntry
    Inherits System.Web.UI.Page
    Private sFormName As String = "StockEntry"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objUT As New ClsUploadTailBal
    Private objCGLLink As New ClsCustomerGLLink
    Private objclsOpeningBalance As New clsOpeningBalance
    Private objclsUSEntry As New clsUploadStockEntry
    Private Shared sExcelSave As String
    Private Shared sFile As String = ""
    Private Shared TotalOpeningCredit As Decimal = 0
    Private Shared TotalOpeningDebit As Double = 0
    Private Shared TOtaltrCredit As Double = 0
    Private Shared TOtaltrDebit As Double = 0
    Private Shared TOtalClosingCredit As Double = 0
    Private Shared TOtalClosingDebit As Double = 0
    Private Shared Unmapped As Integer = 0

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgLinkageForYear.ImageUrl = "~/Images/Submit24.png"
        ImgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim iFormID As Integer = 0
        Dim sFormButtons As String
        Dim dtSampleFormat As New DataTable
        Try
            dgGeneral.Enabled = True
            ddlSheetName.Enabled = False

            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadExistingCustomer()
                ImgbtnApprove.Visible = False
                If sSession.CustomerID <> 0 Then
                    ddlCustName.SelectedValue = sSession.CustomerID
                    If ddlCustName.SelectedIndex > 0 Then
                        ddlCustName_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
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
                Session("AllSession") = sSession
                dt = objclsUSEntry.GetCustStockEntryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID)
                lblTotal.Text = objclsUSEntry.GetCustStockEntryTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID)
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
    Private Function SaveTrailbalanceSchedule()
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
                objclsUSEntry.iACSI_YEARId = sSession.YearID
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
    End Function
End Class