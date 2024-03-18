Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Public Class AssetDeletion
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssetDeletion"
    Private objerrorclass As New BusinesLayer.Components.ErrorClass
    Dim objAssetDeletion As New ClsAssetDeletion
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGraceGeneral As New clsGRACeGeneral

    Dim objGen As New clsGRACeGeneral
    Private Shared sSession As AllSession

    Dim objAsstTrn As New ClsAssetTransactionAddition
    Private objAsst As New ClsAssetMaster
    Private objLocationSetup As New ClsLocationSetup
    Private Shared sIKBBackStatus As String
    Private Shared iMID As Integer
    Private Shared sUMBackStatus As String
    Private Shared dRateofDep As Double = 0
    Private Shared iMasterID As Integer = 0

    Private Shared FStartDate As Date
    Private Shared FEndDate As Date
    Private Shared bLoginUserIsPartner As Boolean
    Private objclsStandardAudit As New clsStandardAudit
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        'imgbtnsave.ImageUrl = "~/Images/Save24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnDelete.ImageUrl = "~/Images/Save24.png"
        ImgBtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim sMasterID As String = ""
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCustomer()
                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    ddlFinancialYear_SelectedIndexChanged(sender, e)
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                    ddlFinancialYear_SelectedIndexChanged(sender, e)
                End If
                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        loadAssetType()
                        BindToLocation()
                        BindLocation()
                    End If
                Else
                    loadAssetType()
                End If
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                Else
                    bLoginUserIsPartner = False
                End If
                txtDelTransNo.Text = objAssetDeletion.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue)
                iMID = 0
                sMasterID = Request.QueryString("MasterID")
                If sMasterID <> "" Then
                    iMID = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    'ddlExtTrnNo.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    BindDetails(iMID)
                    iMasterID = iMID
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sUMBackStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYear(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                FStartDate = objclsGraceGeneral.FormatDtForRDBMS(objclsGraceGeneral.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
                FEndDate = objclsGraceGeneral.FormatDtForRDBMS(objclsGraceGeneral.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
                If ddlCustomerName.SelectedIndex > 0 Then
                    btnCustYes_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindDetails(ByVal iMID As Integer)
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Dim sender As Object, e As EventArgs
        Dim dt1, dt2 As New DataTable
        Dim iLocation, iDivision, iDepartment, iBay As Integer
        Try

            lblError.Text = ""
            dt = objAssetDeletion.showDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iMID)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1

                    If IsDBNull(dt.Rows(i).Item("AFAD_CustomerName")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_CustomerName") > 0) Then
                                ddlCustomerName.SelectedValue = dt.Rows(i)("AFAD_CustomerName")
                            Else
                                ddlCustomerName.SelectedIndex = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_TransNo")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_TransNo") <> "") Then
                                txtDelTransNo.Text = dt.Rows(i)("AFAD_TransNo")
                            Else
                                txtDelTransNo.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Location")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Location") <> 0) Then
                                ddlLocatn.SelectedValue = dt.Rows(i)("AFAD_Location")
                            Else
                                ddlLocatn.SelectedIndex = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Division")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Division") <> 0) Then
                                ddlDivision.SelectedValue = dt.Rows(i)("AFAD_Division")
                                ddlLocatn_SelectedIndexChanged(sender, e)

                            Else
                                ddlDivision.SelectedIndex = -1
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Department")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Department") <> 0) Then
                                ddlDeptmnt.SelectedValue = dt.Rows(i)("AFAD_Department")
                                ddlDivision_SelectedIndexChanged(sender, e)
                            Else
                                ddlDeptmnt.SelectedIndex = -1
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Bay")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Bay") <> 0) Then
                                ddlBay.SelectedValue = dt.Rows(i)("AFAD_Bay")
                                ddlDeptmnt_SelectedIndexChanged(sender, e)
                            Else
                                ddlBay.SelectedIndex = -1
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_AssetClass")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_AssetClass") <> 0) Then
                                ddlAssetClass.SelectedValue = dt.Rows(i)("AFAD_AssetClass")
                            Else
                                ddlAssetClass.SelectedIndex = 0
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_Asset")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Asset") <> 0) Then
                                ddlAsset.SelectedValue = dt.Rows(i)("AFAD_Asset")
                                ddlAssetClass_SelectedIndexChanged(sender, e)
                            Else
                                ddlAsset.SelectedIndex = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_AssetDeletion")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_AssetDeletion") <> 0) Then
                                ddlDeletion.SelectedValue = dt.Rows(i)("AFAD_AssetDeletion")

                                If ddlDeletion.SelectedValue = 1 Then
                                    PnlSold.Visible = False
                                    PnlTransfer.Visible = False
                                    lblPorL.Visible = True
                                    lblAmount.Visible = True

                                ElseIf ddlDeletion.SelectedValue = 2 Then
                                    PnlSold.Visible = False
                                    PnlTransfer.Visible = True
                                    lblPorL.Visible = False
                                    lblAmount.Visible = False

                                ElseIf ddlDeletion.SelectedValue = 3 Then
                                    PnlSold.Visible = False
                                    PnlTransfer.Visible = False
                                    lblPorL.Visible = False
                                    lblAmount.Visible = False

                                ElseIf ddlDeletion.SelectedValue = 4 Then
                                    PnlSold.Visible = False
                                    PnlTransfer.Visible = False
                                    lblPorL.Visible = False
                                    lblAmount.Visible = False

                                ElseIf ddlDeletion.SelectedValue = 5 Then
                                    PnlSold.Visible = False
                                    PnlTransfer.Visible = False
                                    lblPorL.Visible = False
                                    lblAmount.Visible = False

                                End If
                                If ddlLocatn.SelectedIndex > 0 Then
                                    iLocation = ddlLocatn.SelectedValue
                                End If
                                If ddlDivision.SelectedIndex > 0 Then
                                    iDivision = ddlDivision.SelectedValue
                                End If
                                If ddlDeptmnt.SelectedIndex > 0 Then
                                    iDepartment = ddlDeptmnt.SelectedValue
                                End If
                                If ddlBay.SelectedIndex > 0 Then
                                    iBay = ddlBay.SelectedValue
                                End If
                                sStatus = objAssetDeletion.GetAssetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                                If sStatus = "S" Then
                                    lblstatus.Text = "This Asset is Already Sold"
                                    imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False

                                ElseIf sStatus = "St" Then
                                    lblstatus.Text = "This Asset is Already Stolen"
                                    imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False

                                ElseIf sStatus = "D" Then
                                    lblstatus.Text = "This Asset is Destroyed"
                                    imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False

                                ElseIf sStatus = "O" Then
                                    lblstatus.Text = "This Asset is Obsolete"
                                    imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False

                                Else
                                    'imgbtnWaiting.Visible = True : imgbtnDelete.Visible = True
                                    'lblstatus.Text = "Open"
                                End If

                                dt1 = objAssetDeletion.GetMastersDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                                If dt1.Rows.Count > 0 Then
                                    If IsDBNull(dt1.Rows(0)("OriginalCost")) = False Then
                                        txtOriginalCost.Text = dt1.Rows(0)("OriginalCost")
                                    Else
                                        txtOriginalCost.Text = ""
                                    End If

                                    If IsDBNull(dt1.Rows(0)("Quantity")) = False Then
                                        txtOrigQuantity.Text = dt1.Rows(0)("Quantity")
                                    Else
                                        txtOrigQuantity.Text = ""
                                    End If
                                End If

                                dt2 = objAssetDeletion.GetFYAmount(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                                If dt2.Rows.Count > 0 Then
                                    If IsDBNull(dt2.Rows(0)("DepreciationforFY")) = False Then
                                        txtDepAmount.Text = dt1.Rows(0)("OriginalCost") - dt2.Rows(0)("DepreciationforFY")
                                    Else
                                        txtDepAmount.Text = ""
                                    End If
                                End If

                            Else
                                ddlDeletion.SelectedIndex = 0
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_PorLStatus")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            lblPorL.Text = dt.Rows(i)("AFAD_PorLStatus") & " " & ":"
                        Else
                            lblPorL.Text = ""
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_PorLAmount")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            lblAmount.Text = dt.Rows(i)("AFAD_PorLAmount")
                        Else
                            lblAmount.Text = ""
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_AssetDeletionType")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_AssetDeletionType") <> 0) Then
                                If (dt.Rows(i)("AFAD_AssetDeletionType") = 1) Then
                                    rboPartial.Checked = True
                                ElseIf (dt.Rows(i)("AFAD_AssetDeletionType") = 2) Then
                                    rboFully.Checked = True
                                End If
                            Else
                                rboPartial.Checked = False
                                rboFully.Checked = False
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_DeletionDate")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_DeletionDate") <> "01/01/1900") Then
                                txtdeletionDate.Text = dt.Rows(i)("AFAD_DeletionDate")
                            Else
                                txtdeletionDate.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Amount")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtDeletionAmount.Text = dt.Rows(i)("AFAD_Amount")
                        Else
                            txtDeletionAmount.Text = ""
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_Quantity")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Quantity") <> 0) Then
                                txtQuantity.Text = dt.Rows(i)("AFAD_Quantity")
                            Else
                                txtQuantity.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_Paymenttype")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Paymenttype") <> 0) Then
                                ddlPaymenttype.SelectedValue = dt.Rows(i)("AFAD_Paymenttype")
                            Else
                                ddlPaymenttype.SelectedIndex = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_CostofTransport")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtCostofTransport.Text = dt.Rows(i)("AFAD_CostofTransport")
                        Else
                            txtCostofTransport.Text = 0
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_InstallationCost")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtInstallationCost.Text = dt.Rows(i)("AFAD_InstallationCost")
                        Else
                            txtInstallationCost.Text = 0
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_DateofInitiate")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_DateofInitiate") <> "01/01/1900") Then
                                txtDateofInitiate.Text = dt.Rows(i)("AFAD_DateofInitiate")
                            Else
                                txtDateofInitiate.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_DateofReceived")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_DateofReceived") <> "01/01/1900") Then
                                txtDateofReceived.Text = dt.Rows(i)("AFAD_DateofReceived")
                            Else
                                txtDateofReceived.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_ToLocation")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_ToLocation") <> 0) Then
                                ddlToLocation.SelectedValue = dt.Rows(i)("AFAD_ToLocation")
                            Else
                                ddlToLocation.SelectedIndex = -1
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_ToDivision")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_ToDivision") <= 0) Then
                                ddlToDivision.SelectedIndex = -1
                                ddlToLocation_SelectedIndexChanged(sender, e)
                            Else
                                ddlToDivision.SelectedValue = dt.Rows(i)("AFAD_ToDivision")
                                ddlToLocation_SelectedIndexChanged(sender, e)
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_ToDepartment")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_ToDepartment") = 0) Then
                                ddlToDepartment.SelectedIndex = -1

                            Else
                                ddlToDepartment.SelectedValue = dt.Rows(i)("AFAD_ToDepartment")
                                ddlToDivision_SelectedIndexChanged(sender, e)
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_ToBay")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_ToBay") = 0) Then
                                ddlToBay.SelectedIndex = -1

                            Else
                                ddlToBay.SelectedValue = dt.Rows(i)("AFAD_ToBay")
                                ddlToDepartment_SelectedIndexChanged(sender, e)
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_AssetDelDesc")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_AssetDelDesc") <> "") Then
                                txtdeldesc.Text = dt.Rows(i)("AFAD_AssetDelDesc")
                            Else
                                txtdeldesc.Text = ""
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_ContAssetValue")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtContValue.Text = dt.Rows(i)("AFAD_ContAssetValue")
                        Else
                            txtContValue.Text = ""
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_ContDep")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtContDep.Text = dt.Rows(i)("AFAD_ContDep")
                        Else
                            txtContDep.Text = ""
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_ContWDV")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            txtContWDVValue.Text = dt.Rows(i)("AFAD_ContWDV")
                        Else
                            txtContWDVValue.Text = ""
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_InsClaimedNo")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsClaimedNo") <> "") Then
                                txtClaimedNo.Text = dt.Rows(i)("AFAD_InsClaimedNo")
                            Else
                                txtClaimedNo.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_InsAmtClaimed")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsAmtClaimed") <> 0) Then
                                txtAmtClaimed.Text = dt.Rows(i)("AFAD_InsAmtClaimed")
                            Else
                                txtAmtClaimed.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_InsClaimedDate")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsClaimedDate") <> "01/01/1900") Then
                                txtClaimedDate.Text = dt.Rows(i)("AFAD_InsClaimedDate")
                            Else
                                txtClaimedDate.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_InsAmtRecvd")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsAmtRecvd") <> 0) Then
                                txtAmtRecved.Text = dt.Rows(i)("AFAD_InsAmtRecvd")
                            Else
                                txtAmtRecved.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_InsRefNo")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsRefNo") <> "") Then
                                txtAmtRefNo.Text = dt.Rows(i)("AFAD_InsRefNo")
                            Else
                                txtAmtRefNo.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_InsRefDate")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_InsRefDate") <> "01/01/1900") Then
                                txtReceivedDate.Text = dt.Rows(i)("AFAD_InsRefDate")
                            Else
                                txtReceivedDate.Text = ""
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(i).Item("AFAD_Remarks")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Remarks") <> "") Then
                                txtRemarks.Text = dt.Rows(i)("AFAD_Remarks")
                            Else
                                txtRemarks.Text = ""
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_SalesPrice")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_SalesPrice") <> 0) Then
                                txtSalesPrice.Text = dt.Rows(i)("AFAD_SalesPrice")
                            Else
                                txtSalesPrice.Text = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_DelDeprec")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_DelDeprec") <> 0) Then
                                txtDepreciation.Text = dt.Rows(i)("AFAD_DelDeprec")
                            Else
                                txtDepreciation.Text = 0
                            End If
                        End If
                    End If
                    If IsDBNull(dt.Rows(i).Item("AFAD_WDVValue")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_WDVValue") <> 0) Then
                                txtWDVValue.Text = dt.Rows(i)("AFAD_WDVValue")
                            Else
                                txtWDVValue.Text = 0
                            End If
                        End If
                    End If



                    If IsDBNull(dt.Rows(i).Item("AFAD_Delflag")) = False Then
                        If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            If (dt.Rows(i)("AFAD_Delflag") <> "") Then
                                If dt.Rows(i)("AFAD_Delflag") = "A" Then
                                    If bLoginUserIsPartner = True Then
                                        If dt.Rows(0)("AFAD_Delflag") = "A" Then
                                            imgbtnDelete.ImageUrl = "~/Images/Update24.png"
                                            imgbtnDelete.ToolTip = "Update"
                                            imgbtnDelete.Visible = True
                                        Else
                                            imgbtnDelete.ImageUrl = "~/Images/Update24.png"
                                            imgbtnDelete.ToolTip = "Update"
                                            imgbtnDelete.Visible = True
                                        End If
                                    Else
                                        If dt.Rows(0)("AFAD_Delflag") <> "A" Then
                                            imgbtnDelete.ImageUrl = "~/Images/Update24.png"
                                            imgbtnDelete.ToolTip = "Update"
                                            imgbtnDelete.Visible = True
                                        Else
                                            imgbtnDelete.ImageUrl = "~/Images/Update24.png"
                                            imgbtnDelete.ToolTip = "Update"
                                            imgbtnDelete.Visible = False
                                        End If
                                    End If
                                ElseIf dt.Rows(i)("AFAD_Delflag") = "W" Then
                                    lblstatus.Text = "Waiting for approval"
                                    imgbtnDelete.Visible = True
                                    imgbtnWaiting.Visible = True
                                Else
                                        lblstatus.Text = "Open"
                                    imgbtnDelete.Visible = True
                                    imgbtnWaiting.Visible = True
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
                    btnCustYes_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnCustYes_Click(sender As Object, e As EventArgs) Handles btnCustYes.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    loadAssetType()
                    BindToLocation()
                    BindLocation()
                Catch ex As Exception
                    Throw
                End Try
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnCustNo_Click(sender As Object, e As EventArgs) Handles btnCustNo.Click
        Try
            lblError.Text = ""
            If sSession.CustomerID <> 0 Then
                ddlCustomerName.SelectedValue = sSession.CustomerID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlLocatn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocatn.SelectedIndexChanged
        'If ddlLocatn.SelectedIndex > 0 Then
        '    loadDepartments(ddlLocatn.SelectedValue)
        'End If
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlLocatn.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlLocatn.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlDivision.DataSource = dt
            ddlDivision.DataTextField = "LS_Description"
            ddlDivision.DataValueField = "LS_ID"
            ddlDivision.DataBind()
            ddlDivision.Items.Insert(0, "Select ")

            ddlAssetClass.SelectedIndex = 0
            ddlAsset.DataSource = "" : ddlAsset.DataBind()
            ddlDeptmnt.DataSource = "" : ddlDeptmnt.DataBind()
            ddlBay.DataSource = "" : ddlBay.DataBind()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocatn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If

            ddlLocatn.DataSource = dt
            ddlLocatn.DataTextField = "LS_Description"
            ddlLocatn.DataValueField = "LS_ID"
            ddlLocatn.DataBind()
            ddlLocatn.Items.Insert(0, "Select ")


            ddlDivision.DataSource = "" : ddlDivision.DataBind()
            ddlDeptmnt.DataSource = "" : ddlDeptmnt.DataBind()
            ddlBay.DataSource = "" : ddlBay.DataBind()
            ddlAssetClass.SelectedIndex = 0
            ddlAsset.DataSource = "" : ddlAsset.DataBind()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindToLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If

            ddlToLocation.DataSource = dt
            ddlToLocation.DataTextField = "LS_Description"
            ddlToLocation.DataValueField = "LS_ID"
            ddlToLocation.DataBind()
            ddlToLocation.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindToLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                dt = objAsstTrn.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                ddlAssetClass.DataTextField = "AM_Description"
                ddlAssetClass.DataValueField = "AM_ID"
                ddlAssetClass.DataSource = dt
                ddlAssetClass.DataBind()
                ddlAssetClass.Items.Insert(0, "Select Asset Class")

                ddlAsset.DataSource = "" : ddlAsset.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivision.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDivision.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlDeptmnt.DataSource = dt
            ddlDeptmnt.DataTextField = "LS_Description"
            ddlDeptmnt.DataValueField = "LS_ID"
            ddlDeptmnt.DataBind()
            ddlDeptmnt.Items.Insert(0, "Select ")


            ddlAssetClass.SelectedIndex = 0
            ddlAsset.DataSource = "" : ddlAsset.DataBind()
            ddlBay.DataSource = "" : ddlBay.DataBind()

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDeptmnt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeptmnt.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDeptmnt.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlDeptmnt.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlBay.DataSource = dt
            ddlBay.DataTextField = "LS_Description"
            ddlBay.DataValueField = "LS_ID"
            ddlBay.DataBind()
            ddlBay.Items.Insert(0, "Select ")
            ddlAssetClass.SelectedIndex = 0
            ddlAsset.DataSource = "" : ddlAsset.DataBind()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDeptmnt_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAssetClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetClass.SelectedIndexChanged
        Dim iCount As Integer
        Dim AssetLen As String
        Dim ilen As Integer : Dim increment As Integer = 0
        Dim dtamount As New DataTable
        Try
            If ddlAssetClass.SelectedIndex > 0 Then
                loadAssets()
                'AssetLen = objAsstTrn.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue,ddlFinancialYear.SelectedValue , ddlCustomerName.SelectedValue)
                'txtAssetNo.Text = AssetLen
                lblSalValue.Text = objAsstTrn.LoadSalVal(sSession.AccessCode, sSession.AccessCodeID, ddlAssetClass.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssetClass_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub loadAssets()
        Dim ilocation, idepartment, idevision, ibay As New Integer
        Try

            If ddlLocatn.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Location','', 'success');", True)
                Exit Sub
            End If

            If ddlLocatn.SelectedIndex > 0 Then
                ilocation = ddlLocatn.SelectedValue
            Else
                ilocation = 0
            End If

            If ddlDivision.SelectedIndex > 0 Then
                idevision = ddlDivision.SelectedValue
            End If

            If ddlDeptmnt.SelectedIndex > 0 Then
                idepartment = ddlDeptmnt.SelectedValue
            End If

            If ddlBay.SelectedIndex > 0 Then
                ibay = ddlBay.SelectedValue
            End If
            If iMID > 0 Then

                ddlAsset.DataSource = objAsstTrn.ExistingOAsset(sSession.AccessCode, sSession.AccessCodeID, ddlAssetClass.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
                ddlAsset.DataTextField = "AFAM_ItemDescription"
                ddlAsset.DataValueField = "AFAM_ID"
                ddlAsset.DataBind()
                ddlAsset.Items.Insert(0, "Select Asset")

            Else
                ddlAsset.DataSource = objAsstTrn.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, ddlAssetClass.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
                ddlAsset.DataTextField = "AFAM_ItemDescription"
                ddlAsset.DataValueField = "AFAM_ID"
                ddlAsset.DataBind()
                ddlAsset.Items.Insert(0, "Select Asset")

            End If

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssets" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDeletion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeletion.SelectedIndexChanged
        Try
            txtSalesPrice.Enabled = False
            lblPorL.Visible = False : lblAmount.Visible = False

            If ddlDeletion.SelectedIndex = 0 Then
            ElseIf ddlDeletion.SelectedIndex = 1 Then
                PnlSold.Visible = False
                PnlTransfer.Visible = False
                txtSalesPrice.Enabled = True
                lblPorL.Visible = True : lblAmount.Visible = True

            ElseIf ddlDeletion.SelectedIndex = 2 Then
                PnlSold.Visible = False
                PnlTransfer.Visible = True
                txtSalesPrice.Enabled = False
                lblPorL.Visible = False : lblAmount.Visible = False

            ElseIf ddlDeletion.SelectedIndex = 3 Then
                PnlSold.Visible = False
                PnlTransfer.Visible = False
                txtSalesPrice.Enabled = False
                lblPorL.Visible = False : lblAmount.Visible = False

            ElseIf ddlDeletion.SelectedIndex = 4 Then
                PnlSold.Visible = False
                PnlTransfer.Visible = False
                txtSalesPrice.Enabled = False
                lblPorL.Visible = False : lblAmount.Visible = False

            ElseIf ddlDeletion.SelectedIndex = 5 Then
                PnlSold.Visible = False
                PnlTransfer.Visible = False
                rboPartial.Checked = True
                txtSalesPrice.Enabled = False
                lblPorL.Visible = False : lblAmount.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDeletion_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnDelete_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDelete.Click
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim Transdate As Date
        Try
            lblError.Text = ""
            dDate = Date.ParseExact(FStartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtdeletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m < 0 Then
                lblError.Text = "Asset Transaction Date (" & txtdeletionDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & FStartDate & ")."
                lblDeletionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                txtdeletionDate.Focus()
                Exit Sub
            End If

            dDate = Date.ParseExact(FEndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtdeletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            m = DateDiff(DateInterval.Day, dDate, dSDate)
            If m > 0 Then
                lblError.Text = "Asset Transaction Date (" & txtdeletionDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & FEndDate & ")."
                lblDeletionValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                txtdeletionDate.Focus()
                Exit Sub
            End If

            If rboPartial.Checked = False And rboFully.Checked = False Then
                lblError.Text = "Select Partialy Delete  or Fully Delete"
                Exit Sub
            End If

            If ddlDeletion.SelectedIndex = 1 Then
                lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to Sold?"
            ElseIf ddlDeletion.SelectedIndex = 2 Then
                lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to Transfer?"
            ElseIf ddlDeletion.SelectedIndex = 3 Then
                lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to Delete?"
            ElseIf ddlDeletion.SelectedIndex = 4 Then
                lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to Delete?"
            ElseIf ddlDeletion.SelectedIndex = 5 Then
                lblAssetdeletionValidationMsg1.Text = "Are you Sure, you want to Delete?"
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-success');$('#ModalDeletionValidation1').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDelete_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles BtnYES.Click
        Try

            Dim Arr As Array
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Customer','', 'success');", True)
                Exit Sub
            End If

            If ddlLocatn.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Location','', 'success');", True)
                Exit Sub
            End If


            If iMasterID <> 0 Then
                objAssetDeletion.iAFAD_ID = iMasterID
            Else
                objAssetDeletion.iAFAD_ID = 0
            End If

            objAssetDeletion.iAFAD_CustomerName = ddlCustomerName.SelectedValue

            objAssetDeletion.sAFAD_TransNo = txtDelTransNo.Text

            If (ddlLocatn.SelectedIndex > 0) Then
                objAssetDeletion.iAFAD_Location = ddlLocatn.SelectedValue
            Else
                objAssetDeletion.iAFAD_Location = 0
            End If

            If (ddlDivision.SelectedIndex > 0) Then
                objAssetDeletion.iAFAD_Division = ddlDivision.SelectedValue
            Else
                objAssetDeletion.iAFAD_Division = 0
            End If

            If (ddlDeptmnt.SelectedIndex > 0) Then
                objAssetDeletion.iAFAD_Department = ddlDeptmnt.SelectedValue
            Else
                objAssetDeletion.iAFAD_Department = 0
            End If

            If (ddlBay.SelectedIndex > 0) Then
                objAssetDeletion.iAFAD_Bay = ddlBay.SelectedValue
            Else
                objAssetDeletion.iAFAD_Bay = 0
            End If

            If ddlAssetClass.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_AssetClass = ddlAssetClass.SelectedValue
            Else
                objAssetDeletion.iAFAD_AssetClass = 0
            End If

            If ddlAsset.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_Asset = ddlAsset.SelectedValue
            Else
                objAssetDeletion.iAFAD_Asset = 0
            End If

            If ddlDeletion.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_AssetDeletion = ddlDeletion.SelectedValue
            Else
                objAssetDeletion.iAFAD_AssetDeletion = 0
            End If

            If rboPartial.Checked = True Then
                objAssetDeletion.iAFAD_AssetDeletionType = 1
            ElseIf rboFully.Checked = True Then
                objAssetDeletion.iAFAD_AssetDeletionType = 2
            Else
                objAssetDeletion.iAFAD_AssetDeletionType = 0
            End If

            If ddlDeletion.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_AssetDeletion = ddlDeletion.SelectedValue
            Else
                objAssetDeletion.iAFAD_AssetDeletion = 0
            End If

            If txtdeletionDate.Text <> "" Then
                objAssetDeletion.dAFAD_DeletionDate = Date.ParseExact(txtdeletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAssetDeletion.dAFAD_DeletionDate = "01/01/1900"
            End If

            If txtDeletionAmount.Text <> "" Then
                objAssetDeletion.dAFAD_Amount = txtDeletionAmount.Text
            Else
                objAssetDeletion.dAFAD_Amount = "0.00"
            End If

            If txtQuantity.Text <> "" Then
                objAssetDeletion.iAFAD_Quantity = txtQuantity.Text
            Else
                objAssetDeletion.iAFAD_Quantity = 0
            End If

            If ddlPaymenttype.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_Paymenttype = ddlPaymenttype.SelectedValue
            Else
                objAssetDeletion.iAFAD_Paymenttype = 0
            End If

            If txtCostofTransport.Text <> "" Then
                objAssetDeletion.dAFAD_CostofTransport = txtCostofTransport.Text
            Else
                objAssetDeletion.dAFAD_CostofTransport = 0
            End If

            If txtInstallationCost.Text <> "" Then
                objAssetDeletion.dAFAD_InstallationCost = txtInstallationCost.Text
            Else
                objAssetDeletion.dAFAD_InstallationCost = 0
            End If

            If txtDateofInitiate.Text <> "" Then
                objAssetDeletion.dAFAD_DateofInitiate = Date.ParseExact(txtDateofInitiate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAssetDeletion.dAFAD_DateofInitiate = "01/01/1900"
            End If

            If txtDateofReceived.Text <> "" Then
                objAssetDeletion.dAFAD_DateofReceived = Date.ParseExact(txtDateofReceived.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAssetDeletion.dAFAD_DateofReceived = "01/01/1900"
            End If

            If ddlToLocation.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_ToLocation = ddlToLocation.SelectedValue
            Else
                objAssetDeletion.iAFAD_ToLocation = 0
            End If

            If ddlToDivision.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_ToDivision = ddlToDivision.SelectedValue
            Else
                objAssetDeletion.iAFAD_ToDivision = 0
            End If

            If ddlToDepartment.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_ToDepartment = ddlToDepartment.SelectedValue
            Else
                objAssetDeletion.iAFAD_ToDepartment = 0
            End If

            If ddlToBay.SelectedIndex > 0 Then
                objAssetDeletion.iAFAD_ToBay = ddlToBay.SelectedValue
            Else
                objAssetDeletion.iAFAD_ToBay = 0
            End If

            If txtdeldesc.Text <> "" Then
                objAssetDeletion.sAFAD_AssetDelDesc = txtdeldesc.Text
            Else
                objAssetDeletion.sAFAD_AssetDelDesc = ""
            End If

            If txtSalesPrice.Text <> "" Then
                objAssetDeletion.dAFAD_SalesPrice = txtSalesPrice.Text
            Else
                objAssetDeletion.dAFAD_SalesPrice = 0
            End If

            If txtDepreciation.Text <> "" Then
                objAssetDeletion.dAFAD_DelDeprec = txtDepreciation.Text
            Else
                objAssetDeletion.dAFAD_DelDeprec = 0
            End If

            If txtWDVValue.Text <> "" Then
                objAssetDeletion.dAFAD_WDVValue = txtWDVValue.Text
            Else
                objAssetDeletion.dAFAD_WDVValue = 0
            End If

            objAssetDeletion.iAFAD_CreatedBy = sSession.UserID
            objAssetDeletion.dAFAD_CreatedOn = DateTime.Today
            objAssetDeletion.iAFAD_ApprovedBy = sSession.UserID
            objAssetDeletion.dAFAD_ApprovedOn = DateTime.Today
            objAssetDeletion.iAFAD_Deletedby = sSession.UserID
            objAssetDeletion.dAFAD_DeletedOn = DateTime.Today

            objAssetDeletion.sAFAD_Delflag = "W"
            objAssetDeletion.sAFAD_Status = "C"
            'objAssetDeletion.sAFAd_Operation = "U"
            objAssetDeletion.sAFAD_IPAddress = sSession.IPAddress
            objAssetDeletion.iAFAD_YearID = ddlFinancialYear.SelectedValue
            objAssetDeletion.iAFAD_CompID = sSession.AccessCodeID


            'Dim dAmount1, dAmount2, dAmount3, dAmount4 As Double
            'dAmount1 = txtDeletionAmount.Text
            'dAmount2 = txtDepAmount.Text
            'dAmount3 = dAmount1 + dAmount2
            'dAmount4 = txtOriginalCost.Text

            'lblAmount.Text = dAmount4 - dAmount3
            'If dAmount3 > dAmount4 Then
            '    lblPorL.Text = "Profit :"
            '    objAssetDeletion.sAFAD_PorLStatus = "Profit"
            'Else
            '    lblPorL.Text = "Loss :"
            '    objAssetDeletion.sAFAD_PorLStatus = "Loss"
            'End If

            'If lblAmount.Text <> "" Then
            '    objAssetDeletion.dAFAD_PorLAmount = Val(lblAmount.Text)
            'Else
            '    objAssetDeletion.dAFAD_PorLAmount = 0.0
            'End If

            objAssetDeletion.sAFAD_PorLStatus = lblPorL.Text

            If lblAmount.Text = "" Then
                objAssetDeletion.dAFAD_PorLAmount = 0.0
            Else
                objAssetDeletion.dAFAD_PorLAmount = lblAmount.Text
            End If


            objAssetDeletion.dAFAD_ContAssetValue = txtContValue.Text
            objAssetDeletion.dAFAD_ContDep = txtContDep.Text
            objAssetDeletion.dAFAD_ContWDV = txtContWDVValue.Text

            objAssetDeletion.sAFAD_InsClaimedNo = txtClaimedNo.Text
            If txtAmtClaimed.Text = "" Then
                objAssetDeletion.dAFAD_InsAmtClaimed = 0
            Else
                objAssetDeletion.dAFAD_InsAmtClaimed = txtAmtClaimed.Text
            End If
            If txtClaimedDate.Text = "" Then
                objAssetDeletion.dAFAD_InsClaimedDate = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAssetDeletion.dAFAD_InsClaimedDate = Date.ParseExact(txtClaimedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If txtDateofInitiate.Text <> "" Then
                objAssetDeletion.dAFAD_DateofInitiate = Date.ParseExact(txtDateofInitiate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objAssetDeletion.dAFAD_DateofInitiate = "01/01/1900"
            End If

            If txtAmtRecved.Text = "" Then
                objAssetDeletion.dAFAD_InsAmtRecvd = 0
            Else
                objAssetDeletion.dAFAD_InsAmtRecvd = txtAmtRecved.Text
            End If
            objAssetDeletion.sAFAD_InsRefNo = txtAmtRefNo.Text
            If txtReceivedDate.Text = "" Then
                objAssetDeletion.dAFAD_InsRefDate = "01/01/1900"
            Else
                objAssetDeletion.dAFAD_InsRefDate = Date.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If txtRemarks.Text = "" Then
                objAssetDeletion.sAFAD_Remarks = ""
            Else
                objAssetDeletion.sAFAD_Remarks = txtRemarks.Text
            End If
            Arr = objAssetDeletion.SaveFixedAssetDeletion(sSession.AccessCode, sSession.AccessCodeID, objAssetDeletion)
            'iMasterID = Arr(1)

            'If Arr(0) = "3" Then

            '    lblDeletionValidationMsg.Text = "Successfully Saved"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
            'End If

            If Arr(0) = "2" Then
                lblDeletionValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Deletion", "Saved", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblDeletionValidationMsg.Text = "Successfully Saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Deletion", "Saved", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            End If

            lblstatus.Text = "Waiting for approval"
            'lblDeletionValidationMsg.Text = "Waiting for approval"
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
            BindDetails(iMID)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BtnYES_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub

    Private Sub ddlToLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToLocation.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlToLocation.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlToLocation.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlToDivision.DataSource = dt
            ddlToDivision.DataTextField = "LS_Description"
            ddlToDivision.DataValueField = "LS_ID"
            ddlToDivision.DataBind()
            ddlToDivision.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToLocation_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlToDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToDepartment.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlToDepartment.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlToDepartment.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlToBay.DataSource = dt
            ddlToBay.DataTextField = "LS_Description"
            ddlToBay.DataValueField = "LS_ID"
            ddlToBay.DataBind()
            ddlToBay.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToDepartment_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlToDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlToDivision.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlToDivision.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, ddlToDivision.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlToDepartment.DataSource = dt
            ddlToDepartment.DataTextField = "LS_Description"
            ddlToDepartment.DataValueField = "LS_ID"
            ddlToDepartment.DataBind()
            ddlToDepartment.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlToDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sStatus As String = ""
        Dim iAppBy As Integer

        Dim iTolocation, iTodepartment, iTodevision, iTobay As New Integer
        Dim ilocation, idepartment, idevision, ibay As New Integer
        Try
            If ddlLocatn.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Location','', 'success');", True)
                Exit Sub
            End If
            ''From Location
            If ddlLocatn.SelectedIndex > 0 Then
                ilocation = ddlLocatn.SelectedValue
            Else
                ilocation = 0
            End If

            If ddlDivision.SelectedIndex > 0 Then
                idevision = ddlDivision.SelectedValue
            Else
                idevision = 0
            End If

            If ddlDeptmnt.SelectedIndex > 0 Then
                idepartment = ddlDeptmnt.SelectedValue
            Else
                idepartment = 0
            End If

            If ddlBay.SelectedIndex > 0 Then
                ibay = ddlBay.SelectedValue
            Else
                ibay = 0
            End If

            'tolocation
            If ddlToLocation.SelectedIndex > 0 Then
                iTolocation = ddlToLocation.SelectedValue
            Else
                iTolocation = 0
            End If

            If ddlToDivision.SelectedIndex > 0 Then
                iTodevision = ddlToDivision.SelectedValue
            Else
                iTodevision = 0
            End If

            If ddlToDepartment.SelectedIndex > 0 Then
                iTodepartment = ddlToDepartment.SelectedValue
            Else
                iTodepartment = 0
            End If

            If ddlToBay.SelectedIndex > 0 Then
                iTobay = ddlToBay.SelectedValue
            Else
                iTobay = 0
            End If

            iAppBy = sSession.UserID

            If txtDelTransNo.Text <> "" Then
                sStatus = objAssetDeletion.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, txtDelTransNo.Text, ddlCustomerName.SelectedValue)
                If sStatus = "A" Then
                    lblstatus.Text = "Approved"
                    lblError.Text = "This Transaction Already Approved." : lblDeletionValidationMsg.Text = "This Transaction Already Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                ElseIf sStatus = "W" Then
                    objAssetDeletion.StatusCheck(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, txtDelTransNo.Text, "A", "A", iAppBy, ddlCustomerName.SelectedValue)
                    If ddlDeletion.SelectedIndex = 1 Then
                        objAssetDeletion.UpdateStatusAsset(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "S", "S", iAppBy, ddlCustomerName.SelectedValue)
                    ElseIf ddlDeletion.SelectedIndex = 2 Then
                        objAssetDeletion.UpdateStatusTransfer(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "T", "T", iAppBy, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
                        objAssetDeletion.InsertintoAssetMaster(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "T", "T", iAppBy, ddlCustomerName.SelectedValue, iTolocation, iTodevision, iTodepartment, iTobay, txtDateofReceived.Text)
                        objAssetDeletion.InsertintoAssetMasterAdd(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, ddlAssetClass.SelectedValue, "T", "T", iAppBy, ddlCustomerName.SelectedValue, iTolocation, iTodevision, iTodepartment, iTobay, txtContValue.Text, txtContDep.Text, txtContWDVValue.Text, sSession.UserID)
                    ElseIf ddlDeletion.SelectedIndex = 3 Then
                        objAssetDeletion.UpdateStatusAsset(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "St", "St", iAppBy, ddlCustomerName.SelectedValue)
                    ElseIf ddlDeletion.SelectedIndex = 4 Then
                        objAssetDeletion.UpdateStatusAsset(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "D", "D", iAppBy, ddlCustomerName.SelectedValue)
                    ElseIf ddlDeletion.SelectedIndex = 5 Then
                        objAssetDeletion.UpdateStatusAsset(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAsset.SelectedValue, "O", "O", iAppBy, ddlCustomerName.SelectedValue)
                    End If
                    lblstatus.Text = "Approved"

                    lblDeletionValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDeletionValidation').modal('show');", True)
                End If
                imgbtnDelete.Visible = False : imgbtnWaiting.Visible = False
            Else
                lblError.Text = "Select Existing Transaction No."
            End If
            BindDetails(iMID)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            Response.Redirect(String.Format("~/FixedAsset/AssetDeletion.aspx?"), False)
            txtDelTransNo.Text = objAssetDeletion.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAsset_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAsset.SelectedIndexChanged
        Dim sStatus As String = ""
        Dim dt, dt1 As New DataTable
        Dim iLocation, iDivision, iDepartment, iBay As Integer
        Try
            txtOriginalCost.Text = "" : txtOrigQuantity.Text = "" : txtDepAmount.Text = "" : ddlDeletion.SelectedIndex = 0
            txtdeletionDate.Text = "" : txtQuantity.Text = "" : txtdeldesc.Text = "" : txtSalesPrice.Text = "" : txtDeletionAmount.Text = ""
            txtDepreciation.Text = "" : txtWDVValue.Text = "" : ddlPaymenttype.SelectedIndex = 0 : txtContValue.Text = "" : txtContDep.Text = ""
            txtContWDVValue.Text = "" : lblAmount.Text = "" : txtCostofTransport.Text = "" : txtInstallationCost.Text = "" : txtDateofInitiate.Text = ""
            'ddlToLocation.SelectedIndex = 0 : ddlToDivision.SelectedIndex = 0 : ddlToDepartment.SelectedIndex = 0 : ddlToBay.SelectedIndex = 0
            txtClaimedNo.Text = "" : txtAmtClaimed.Text = "" : txtClaimedDate.Text = "" : txtAmtRecved.Text = "" : txtAmtRefNo.Text = ""
            txtReceivedDate.Text = ""

            If ddlLocatn.SelectedIndex > 0 Then
                iLocation = ddlLocatn.SelectedValue
            Else
                iLocation = 0
            End If
            If ddlDivision.SelectedIndex > 0 Then
                iDivision = ddlDivision.SelectedValue
            Else
                iDivision = 0
            End If
            If ddlDeptmnt.SelectedIndex > 0 Then
                iDepartment = ddlDeptmnt.SelectedValue
            Else
                iDepartment = 0
            End If
            If ddlBay.SelectedIndex > 0 Then
                iBay = ddlBay.SelectedValue
            Else
                iBay = 0
            End If
            If ddlAsset.SelectedIndex <> 0 Then
                dt = objAssetDeletion.GetMastersDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("OriginalCost")) = False Then
                        txtOriginalCost.Text = dt.Rows(0)("OriginalCost")
                    Else
                        txtOriginalCost.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("Quantity")) = False Then
                        txtOrigQuantity.Text = dt.Rows(0)("Quantity")
                    Else
                        txtOrigQuantity.Text = ""
                    End If
                    'If IsDBNull(dt.Rows(0)("DepreciationforFY")) = False Then
                    '    txtDepAmount.Text = dt.Rows(0)("DepreciationforFY")
                    'Else
                    '    txtDepAmount.Text = ""
                    'End If

                    dt1 = objAssetDeletion.GetFYAmount(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                    If dt1.Rows.Count > 0 Then
                        If IsDBNull(dt1.Rows(0)("DepreciationforFY")) = False Then
                            Dim damount As Double = 0.0
                            Dim oAmount As Double = 0.0
                            damount = dt1.Rows(0)("DepreciationforFY")
                            oAmount = txtOriginalCost.Text
                            txtDepAmount.Text = oAmount - damount
                        Else
                            txtDepAmount.Text = ""
                        End If

                    End If
                    Dim dt2 As New DataTable
                    dt2 = objAssetDeletion.GetReate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                    If dt2.Rows.Count > 0 Then
                        If IsDBNull(dt2.Rows(0)("ADep_RateofDep")) = False Then
                            dRateofDep = dt2.Rows(0)("ADep_RateofDep")
                        Else
                            dRateofDep = 0
                        End If
                    End If

                    imgbtnDelete.Visible = True : imgbtnWaiting.Visible = True
                    sStatus = objAssetDeletion.GetAssetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAssetClass.SelectedValue, ddlAsset.SelectedValue, ddlCustomerName.SelectedValue, iLocation, iDivision, iDepartment, iBay)
                    If sStatus = "S" Then
                        lblstatus.Text = "This Asset is Already Sold"
                        imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    ElseIf sStatus = "St" Then
                        lblstatus.Text = "This Asset is Already Stolen"
                        imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    ElseIf sStatus = "D" Then
                        lblstatus.Text = "This Asset is Destroyed"
                        imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    ElseIf sStatus = "O" Then
                        lblstatus.Text = "This Asset is Obsolete"
                        imgbtnWaiting.Visible = False : imgbtnDelete.Visible = False
                        Exit Sub
                    Else
                        imgbtnWaiting.Visible = True : imgbtnDelete.Visible = True
                        lblstatus.Text = "Open"
                    End If
                Else
                    lblError.Text = "There is Not any Transaction done for this Asset " : ddlAsset.Focus()
                    imgbtnDelete.Visible = False : imgbtnWaiting.Visible = False
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAsset_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ImgBtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBack.Click

        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/FixedAsset/AssetDeletionDashboard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim ddepAmount As Double = 0.0
        Dim dOriginalCost As Double = 0.0
        Dim dsalesPrice As Double = 0.0
        Dim dsetbyDep As Double = 0.0
        Dim dsetbyWDV As Double = 0.0
        Dim sYear As String
        Dim sYear1 As String
        Dim ayear As Array
        Dim Startdate As Date
        Dim Enddate As Date
        Dim Deldate As Date
        Dim Noofdays As Integer
        Dim TotalNofdays As Integer

        Dim dcontDep As Double = 0.0
        Dim dcontWDV As Double = 0.0
        Dim dcontTotaldep As Double = 0.0
        Dim dcontDepTill As Double = 0.0
        Dim dSalVal As Double = 0.0


        Dim dSalAmt As Double = 0.0
        Try
            lblError.Text = ""
            If ddlDeletion.SelectedIndex = 0 Then
                lblDeletionValidationMsg.Text = "Select Transaction  Type" : lblError.Text = "Select Transaction  Type" : ddlDeletion.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDeletionValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDeletionAmount.Text = "" Then
                lblDeletionValidationMsg.Text = "Enter deletion Amount" : lblError.Text = "Enter deletion Amount" : txtDeletionAmount.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDeletionValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAssetClass.SelectedIndex = 0 Then
                lblDeletionValidationMsg.Text = "Select AssetClass." : lblError.Text = "Select AssetClass." : txtDeletionAmount.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDeletionValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAsset.SelectedIndex = 0 Then
                lblDeletionValidationMsg.Text = "Select Asset." : lblError.Text = "Select Asset." : txtDeletionAmount.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDeletionValidation').modal('show');", True)
                Exit Sub
            End If


            ayear = ddlFinancialYear.SelectedItem.Text.Split("-")
            sYear1 = Trim(ayear(1))
            sYear = Trim(ayear(0))

            Startdate = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Enddate = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If txtdeletionDate.Text = "" Then
                lblError.Text = "Enter Deletion Date" : txtdeletionDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDeletionValidation').modal('show');", True)

                Exit Sub
            End If
            Deldate = Date.ParseExact(txtdeletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim ts As TimeSpan = Deldate - Startdate
            Noofdays = ts.TotalDays + 1

            Dim ts1 As TimeSpan = Enddate - Startdate
            TotalNofdays = ts1.TotalDays + 1

            If txtDepAmount.Text <> "" Then
                ddepAmount = txtDepAmount.Text
            End If
            If txtOriginalCost.Text <> "" Then
                dOriginalCost = txtOriginalCost.Text
            End If
            If txtDeletionAmount.Text <> "" Then
                dsalesPrice = txtDeletionAmount.Text
            End If
            dsetbyDep = Math.Round((ddepAmount * dsalesPrice) / dOriginalCost)
            dsetbyWDV = Math.Round(dsalesPrice - dsetbyDep)

            Dim dep As Double = 0.0
            If ddlDeletion.SelectedIndex = 1 Or ddlDeletion.SelectedIndex = 3 Or ddlDeletion.SelectedIndex = 4 Then
                dep = Math.Round((((dsetbyWDV * dRateofDep) / 100) * Noofdays) / TotalNofdays)
                'txtDepreciation.Text = Math.Round(dep + dsetbyDep)
                txtDepreciation.Text = Math.Round(dep)
                txtWDVValue.Text = dsetbyWDV - dep


                lblAmount.Text = Math.Round(Val(txtSalesPrice.Text) - txtWDVValue.Text)

                txtContValue.Text = txtOriginalCost.Text - txtDeletionAmount.Text
                dcontDep = ddepAmount - dsetbyDep
                dcontWDV = txtContValue.Text - dcontDep
                txtContWDVValue.Text = dcontWDV
                dcontDepTill = Math.Round((((dcontWDV * dRateofDep) / 100) * TotalNofdays) / TotalNofdays)
                txtContDep.Text = dcontDep + dcontDepTill


                If txtWDVValue.Text < Val(txtSalesPrice.Text) Then
                    lblPorL.Text = "Profit"
                ElseIf txtWDVValue.Text > Val(txtSalesPrice.Text) Then
                    lblPorL.Text = "Loss"
                End If

            ElseIf ddlDeletion.SelectedIndex = 2 Then

                dSalVal = lblSalValue.Text
                dSalAmt = (dSalVal) * (txtOriginalCost.Text) / 100

                dep = Math.Round((((dsetbyWDV * dRateofDep) / 100) * Noofdays) / TotalNofdays)
                'txtDepreciation.Text = Math.Round(dep + dsetbyDep)

                Dim dOrifginalCost As Double = 0.0
                Dim dDepamounts As Double = 0.0
                Dim ddiffamounts As Double = 0.0
                dOrifginalCost = txtOriginalCost.Text
                dDepamounts = txtDepAmount.Text
                ddiffamounts = dOrifginalCost - dDepamounts
                If ddiffamounts > dSalAmt Then
                    txtDepreciation.Text = Math.Round(dep)
                    txtWDVValue.Text = dOrifginalCost - dDepamounts - dep
                Else
                    txtDepreciation.Text = 0
                    dep = 0
                    txtWDVValue.Text = dOrifginalCost - dDepamounts - dep
                End If
                Dim depamount As Double = 0.0
                depamount = txtDepreciation.Text

                txtContValue.Text = dOrifginalCost
                txtContDep.Text = dDepamounts + depamount
                txtContWDVValue.Text = txtContValue.Text - txtContDep.Text

            ElseIf ddlDeletion.SelectedIndex = 5 And rboFully.Checked = True Then
                    dSalVal = lblSalValue.Text
                    dSalAmt = (dSalVal) * (txtOriginalCost.Text) / 100

                    dep = Math.Round((dsetbyWDV - dSalAmt))
                    txtDepreciation.Text = Math.Round(dep)
                    txtWDVValue.Text = dSalAmt

                    lblAmount.Text = 0
                    txtContValue.Text = 0
                    dcontDep = ddepAmount - dsetbyDep
                    dcontWDV = txtContValue.Text
                    txtContWDVValue.Text = dSalAmt
                    dcontDepTill = Math.Round((((dcontWDV * dRateofDep) / 100) * TotalNofdays) / TotalNofdays)
                    txtContDep.Text = dcontDep + dcontDepTill

                    If txtWDVValue.Text < Val(txtSalesPrice.Text) Then
                        lblPorL.Text = "Profit"
                    ElseIf dcontWDV > Val(txtSalesPrice.Text) Then
                        lblPorL.Text = "Loss"
                    End If
                Else                                 ' Absolite delete but partially same as 1St one code
                    dep = Math.Round((((dsetbyWDV * dRateofDep) / 100) * Noofdays) / TotalNofdays)
                txtDepreciation.Text = Math.Round(dep + dsetbyDep)
                txtWDVValue.Text = dsetbyWDV - dep
                lblAmount.Text = Math.Round(Val(txtSalesPrice.Text) - txtWDVValue.Text)
                txtContValue.Text = txtOriginalCost.Text - txtDeletionAmount.Text
                dcontDep = ddepAmount - dsetbyDep
                dcontWDV = txtContValue.Text - dcontDep
                txtContWDVValue.Text = dcontWDV
                dcontDepTill = Math.Round((((dcontWDV * dRateofDep) / 100) * TotalNofdays) / TotalNofdays)
                txtContDep.Text = dcontDep + dcontDepTill

                If txtWDVValue.Text < Val(txtSalesPrice.Text) Then
                    lblPorL.Text = "Profit"
                ElseIf dcontWDV > Val(txtSalesPrice.Text) Then
                    lblPorL.Text = "Loss"
                End If
            End If

        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlBay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBay.SelectedIndexChanged
        Try
            ddlAssetClass.SelectedIndex = 0
            ddlAsset.DataSource = "" : ddlAsset.DataBind()
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBay_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub rboPartial_CheckedChanged(sender As Object, e As EventArgs) Handles rboPartial.CheckedChanged
        Try
            If ddlDeletion.SelectedIndex = 5 Then
                txtDeletionAmount.Text = "" : txtDeletionAmount.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboPartial_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub rboFully_CheckedChanged(sender As Object, e As EventArgs) Handles rboFully.CheckedChanged
        Try
            If ddlDeletion.SelectedIndex = 5 Then
                txtDeletionAmount.Text = txtOriginalCost.Text
            End If
        Catch ex As Exception
            lblError.Text = objerrorclass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboFully_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click

    End Sub

    Private Sub BtnYES_Command(sender As Object, e As CommandEventArgs) Handles BtnYES.Command

    End Sub
End Class