Imports System
Imports System.Data
Imports System.Drawing
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class DepreciationComputation
    Inherits System.Web.UI.Page

    Private sFormName As String = "DepreciationComputation"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGRACeGeneral
    Dim objGen As New clsGRACeGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objDepComp As New ClsDepreciationComputation
    Dim dtDep As New DataTable
    Dim dtIt As New DataTable
    Dim Startdate As New Date
    Dim Enddate As New Date
    Private Shared dt As New DataTable
    'Private Shared dt As New DataTable
    Dim dOpeningBal As Double = 0, dDepreciation As Double = 0, dWDV As Double = 0
    Private objAsst As New ClsAssetMaster
    Private Shared dtITAct As New DataTable
    Private Shared FStartDate As Date
    Private Shared FEndDate As Date
    Private objclsGraceGeneral As New clsGRACeGeneral

    Dim dOriginalCost As Double = 0, dResidualValue As Double = 0
    Dim dWDVOpeningValue As Double = 0, dWDVClosingValue As Double = 0, dDepPeriod As Double = 0
    Dim dWDVOpeningValueIT As Double = 0, dWDVClosingValueIT As Double = 0, dAdditionYear As Double = 0, dDepPeriodIT As Double = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Pageload(sender As Object, e As EventArgs) Handles Me.Load

        Try
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
                End If
                'lblFinYear.Text = sSession.YearName
                'BindDepreciationComputation()
                'BindItRateComputation()

                'loadDurationMonth()
                'DurationHalfYearly()
                'DurationQuarterly()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Pageload" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYear(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
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
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                Catch ex As Exception

                End Try
            End If
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
    'Public Sub BindDepreciationComputation()
    '    Dim dt As New DataTable
    '    Dim i As Integer
    '    Try
    '        lblError.Text = ""
    '        btnCalculate.Enabled = True
    '        dt = objDepComp.LoadDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue )
    '        dgDepComp.DataSource = dt
    '        dgDepComp.DataBind()
    '        'If dt.Rows.Count > 0 Then
    '        '    For i = 0 To dt.Rows.Count - 1
    '        '        If dt.Rows(i)("DepreciationRate").ToString() = "" Then
    '        '            lblError.Text = "Enter Depreciation Rate in Asset Master for " & dt.Rows(i)("Assettype") & "-" & dt.Rows(i)("AssetDescription")
    '        '            btnCalculate.Enabled = False
    '        '            Exit Sub
    '        '        End If
    '        '    Next
    '        'End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDepreciationComputation")
    '    End Try
    'End Sub
    'Public Sub BindItRateComputation()
    '    Dim dt As New DataTable
    '    Dim i As Integer
    '    Try
    '        lblError.Text = ""
    '        btnCalculate.Enabled = True
    '        dt = objDepComp.LoadItRateComputation(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue )
    '        dgItComp.DataSource = dt
    '        dgItComp.DataBind()
    '        If dt.Rows.Count > 0 Then
    '            For i = 0 To dt.Rows.Count - 1
    '                If dt.Rows(i)("ItRate").ToString() = "" Then
    '                    lblError.Text = "Enter Depreciation Rate in Asset Master for " & dt.Rows(i)("Assettype") & "-" & dt.Rows(i)("AssetDescription")
    '                    btnCalculate.Enabled = False
    '                    Exit Sub
    '                End If
    '            Next
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindItRateComputation")
    '    End Try
    'End Sub
    'Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
    '    Try
    '        lblError.Text = ""
    '        dtDep = objDepComp.CalculateDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue , lblError.Text)
    '        dgDepComp.DataSource = dtDep
    '        dgDepComp.DataBind()
    '        dtIt = objDepComp.CalculateItRateComputation(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue , lblError.Text)
    '        dgItComp.DataSource = dtIt
    '        dgItComp.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click")
    '    End Try
    'End Sub
    Private Sub dgDepComp_PreRender(sender As Object, e As EventArgs) Handles dgDepComp.PreRender
        Try
            If dgDepComp.Rows.Count > 0 Then
                dgDepComp.UseAccessibleHeader = True
                dgDepComp.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDepComp.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDepComp_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDepAsperITAct_PreRender(sender As Object, e As EventArgs) Handles dgDepAsperITAct.PreRender
        Try
            If dgDepAsperITAct.Rows.Count > 0 Then
                dgDepAsperITAct.UseAccessibleHeader = True
                dgDepAsperITAct.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDepAsperITAct.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDepAsperITAct_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objDepComp As New ClsDepreciationComputation
        Dim objItComp As New ClsDepreciationComputation
        Dim Arr() As String
        Dim lblAssetMasterPKID As New Label
        Dim lblAssetTypeID As New Label
        Dim Assettype As New Label
        Dim Item As New Label
        Dim RateofDep As New Label
        Dim OPBForYR As New Label
        Dim Addamount As New Label
        Dim DepreciationforFY As New Label
        Dim WrittenDownValue As New Label
        Dim lblLocationID, lblDivisionID, lblDepartmentID, lblBayID, lblTrType As New Label
        Dim pkid As Integer

        Dim lblRateofDep As New Label
        Dim lblBfrQtrAmount As New Label
        Dim lblBfrQtrDep As New Label
        Dim lblAftQtrAmount As New Label
        Dim lblAftQtrDep As New Label
        Dim lblDelAmount As New Label
        Dim lblWDVOpeningValue As New Label
        Dim lblAdditionDuringtheYear As New Label
        Dim lblDepfortheperiod As New Label
        Dim lblWDVClosingValue As New Label
        Dim lblAssetClassID As New Label
        Dim lblInitDepAmt As New Label

        Try
            lblError.Text = ""

            If ddlDepBasis.SelectedIndex = 1 Then

                objDepComp.RemoveOpBal(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlMethod.SelectedIndex, ddlCustomerName.SelectedValue)

                If dgDepComp.Rows.Count > 0 Then

                    For i = 0 To dgDepComp.Rows.Count - 1

                        lblAssetTypeID = dgDepComp.Rows(i).FindControl("lblAssetClassID")
                        If lblAssetTypeID.Text = 3 Then
                            lblError.Text = ""
                        End If
                        If lblAssetTypeID.Text <> "" Then
                            objDepComp.iADep_ID = 0

                            If lblAssetTypeID.Text <> "" Then
                                objDepComp.iADep_AssetID = lblAssetTypeID.Text
                            End If

                            Item = dgDepComp.Rows(i).FindControl("lblAssetID")
                            If Item.Text <> "" Then
                                objDepComp.sADep_Item = Item.Text
                            End If

                            RateofDep = dgDepComp.Rows(i).FindControl("lblDepreciationRate")
                            If RateofDep.Text <> "" Then
                                objDepComp.dADep_RateofDep = RateofDep.Text
                            End If


                            OPBForYR = dgDepComp.Rows(i).FindControl("lblOPBForYR")
                            If Val(OPBForYR.Text) = 0 Then
                                Addamount = dgDepComp.Rows(i).FindControl("lblOrignalCost")
                                objDepComp.dADep_OPBForYR = Addamount.Text
                            Else
                                If OPBForYR.Text <> "" Then
                                    objDepComp.dADep_OPBForYR = OPBForYR.Text
                                End If
                            End If

                            DepreciationforFY = dgDepComp.Rows(i).FindControl("lblDepreciationforFY")
                            If DepreciationforFY.Text <> "" Then
                                objDepComp.dADep_DepreciationforFY = DepreciationforFY.Text
                            End If

                            WrittenDownValue = dgDepComp.Rows(i).FindControl("lblwrtnvalue")
                            If WrittenDownValue.Text <> "" Then
                                objDepComp.dADep_WrittenDownValue = WrittenDownValue.Text
                            End If

                            lblLocationID = dgDepComp.Rows(i).FindControl("lblLocationID")
                            lblLocationID.Text = objDepComp.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblLocationID.Text, ddlCustomerName.SelectedValue)
                            If lblLocationID.Text <> "" Then
                                objDepComp.iADep_Location = lblLocationID.Text
                            Else
                                objDepComp.iADep_Location = 0
                            End If

                            lblDivisionID = dgDepComp.Rows(i).FindControl("lblDivisionID")
                            lblDivisionID.Text = objDepComp.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblDivisionID.Text, ddlCustomerName.SelectedValue)
                            If lblDivisionID.Text <> "" Then
                                objDepComp.iADep_Division = lblDivisionID.Text
                            Else
                                objDepComp.iADep_Division = 0
                            End If

                            lblDepartmentID = dgDepComp.Rows(i).FindControl("lblDepartmentID")
                            lblDepartmentID.Text = objDepComp.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblDepartmentID.Text, ddlCustomerName.SelectedValue)
                            If lblDepartmentID.Text <> "" Then
                                objDepComp.iADep_Department = lblDepartmentID.Text
                            Else
                                objDepComp.iADep_Department = 0
                            End If

                            lblBayID = dgDepComp.Rows(i).FindControl("lblBayID")
                            lblBayID.Text = objDepComp.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblBayID.Text, ddlCustomerName.SelectedValue)
                            If lblBayID.Text <> "" Then
                                objDepComp.iADep_Bay = lblBayID.Text
                            Else
                                objDepComp.iADep_Bay = 0
                            End If

                            lblTrType = dgDepComp.Rows(i).FindControl("lblTrType")
                            If lblTrType.Text <> "" Then
                                objDepComp.iADep_TransType = lblTrType.Text
                            End If

                            'objDepComp.sADep_Item = dgDepComp.Rows(i).Cells(4).Text
                            'objDepComp.dADep_RateofDep = dgDepComp.Rows(i).Cells(9).Text
                            'objDepComp.dADep_OPBForYR = dgDepComp.Rows(i).Cells(10).Text
                            'objDepComp.dADep_DepreciationforFY = dgDepComp.Rows(i).Cells(11).Text
                            'objDepComp.dADep_WrittenDownValue = dgDepComp.Rows(i).Cells(12).Text
                            objDepComp.dADep_ClosingDate = Date.Today

                            objDepComp.iADep_CreatedBy = sSession.UserID
                            objDepComp.dADep_CreatedOn = DateTime.Today
                            objDepComp.iADep_UpdatedBy = sSession.UserID
                            objDepComp.dADep_UpdatedOn = DateTime.Today
                            objDepComp.sADep_DelFlag = "X"
                            objDepComp.sADep_Status = "W"
                            objDepComp.iADep_YearID = ddlFinancialYear.SelectedValue
                            objDepComp.iADep_CompID = sSession.AccessCodeID
                            objDepComp.sADep_Opeartion = "C"
                            objDepComp.sADep_IPAddress = sSession.IPAddress
                            objDepComp.iADep_CustId = ddlCustomerName.SelectedValue
                            objDepComp.iADep_Method = ddlMethod.SelectedValue
                            Arr = objDepComp.SaveDepreciationComputation(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objDepComp)
                        End If
                    Next
                End If
            End If
            If ddlDepBasis.SelectedIndex = 2 Then
                If dgDepAsperITAct.Rows.Count > 0 Then
                    objDepComp.RemoveITAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    For i = 0 To dgDepAsperITAct.Rows.Count - 1
                        objDepComp.iADITAct_ID = 0
                        lblAssetClassID = dgDepAsperITAct.Rows(i).FindControl("lblClassID")
                        If lblAssetClassID.Text <> "" Then
                            objDepComp.iADITAct_AssetClassID = lblAssetClassID.Text
                        Else
                            objDepComp.iADITAct_AssetClassID = 0
                        End If
                        lblBfrQtrAmount = dgDepAsperITAct.Rows(i).FindControl("lblBfrQtrAmount")
                        If lblBfrQtrAmount.Text <> 0 Then
                            objDepComp.dADITAct_BfrQtrAmount = lblBfrQtrAmount.Text
                        Else
                            objDepComp.dADITAct_BfrQtrAmount = 0
                        End If
                        lblRateofDep = dgDepAsperITAct.Rows(i).FindControl("lblRateofDep")
                        If lblRateofDep.Text <> 0 Then
                            objDepComp.dADITAct_RateofDep = lblRateofDep.Text
                        Else
                            objDepComp.dADITAct_RateofDep = 0
                        End If
                        lblBfrQtrDep = dgDepAsperITAct.Rows(i).FindControl("lblBfrQtrDep")
                        If lblBfrQtrDep.Text <> 0 Then
                            objDepComp.dADITAct_BfrQtrDep = lblBfrQtrDep.Text
                        Else
                            objDepComp.dADITAct_BfrQtrDep = 0
                        End If
                        lblAftQtrAmount = dgDepAsperITAct.Rows(i).FindControl("lblAftQtrAmount")
                        If lblAftQtrAmount.Text <> 0 Then
                            objDepComp.dADITAct_AftQtrAmount = lblAftQtrAmount.Text
                        Else
                            objDepComp.dADITAct_AftQtrAmount = 0
                        End If
                        lblAftQtrDep = dgDepAsperITAct.Rows(i).FindControl("lblAftQtrDep")
                        If lblAftQtrDep.Text <> 0 Then
                            objDepComp.dADITAct_AftQtrDep = lblAftQtrDep.Text
                        Else
                            objDepComp.dADITAct_AftQtrDep = 0
                        End If
                        lblDelAmount = dgDepAsperITAct.Rows(i).FindControl("lblDelAmount")
                        If lblDelAmount.Text <> 0 Then
                            objDepComp.dADITAct_DelAmount = lblDelAmount.Text
                        Else
                            objDepComp.dADITAct_DelAmount = 0
                        End If
                        lblWDVOpeningValue = dgDepAsperITAct.Rows(i).FindControl("lblWDVOpeningValue")
                        If lblWDVOpeningValue.Text <> 0 Then
                            objDepComp.dADITAct_OPBForYR = lblWDVOpeningValue.Text
                        Else
                            objDepComp.dADITAct_OPBForYR = 0
                        End If
                        lblDepfortheperiod = dgDepAsperITAct.Rows(i).FindControl("lblDepfortheperiod")
                        If lblDepfortheperiod.Text <> 0 Then
                            objDepComp.dADITAct_DepreciationforFY = lblDepfortheperiod.Text
                        Else
                            objDepComp.dADITAct_DepreciationforFY = 0
                        End If
                        lblWDVClosingValue = dgDepAsperITAct.Rows(i).FindControl("lblWDVClosingValue")
                        If lblWDVClosingValue.Text <> 0 Then
                            objDepComp.dADITAct_WrittenDownValue = lblWDVClosingValue.Text
                        Else
                            objDepComp.dADITAct_WrittenDownValue = 0
                        End If
                        lblInitDepAmt = dgDepAsperITAct.Rows(i).FindControl("lblNextYrCarry")
                        If Val(lblInitDepAmt.Text) <> 0 Then
                            objDepComp.dADITAct_InitAmt = lblInitDepAmt.Text
                        Else
                            objDepComp.dADITAct_InitAmt = 0
                        End If
                        objDepComp.iADITAct_CreatedBy = sSession.UserID
                        objDepComp.dADITAct_CreatedOn = DateTime.Today
                        objDepComp.iADITAct_UpdatedBy = sSession.UserID
                        objDepComp.dADITAct_UpdatedOn = DateTime.Today
                        objDepComp.iADITAct_ApprovedBy = sSession.UserID
                        objDepComp.dADITAct_ApprovedOn = DateTime.Today
                        objDepComp.sADITAct_DelFlag = "X"
                        objDepComp.sADITAct_Status = "W"
                        objDepComp.iADITAct_YearID = ddlFinancialYear.SelectedValue
                        objDepComp.iADITAct_CompID = sSession.AccessCodeID
                        objDepComp.iADITAct_CustId = ddlCustomerName.SelectedValue
                        objDepComp.iADITAct_Opeartion = "C"
                        objDepComp.sADITAct_IPAddress = sSession.IPAddress
                        Arr = objDepComp.SaveDepreciationITAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objDepComp)
                    Next
                End If

            End If


            'If Arr(0) = "2" Then
            '    lblError.Text = "Successfully Updated"
            '    lblPaymentMasterValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)

            'ElseIf Arr(0) = "3" Then
            lblError.Text = "Successfully Saved"
            lblPaymentMasterValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Depreciation Computation", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            'End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Private Sub dgItComp_PreRender(sender As Object, e As EventArgs) Handles dgItComp.PreRender
    '    Try
    '        If dgItComp.Rows.Count > 0 Then
    '            dgItComp.UseAccessibleHeader = True
    '            dgItComp.HeaderRow.TableSection = TableRowSection.TableHeader
    '            dgItComp.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgItComp_PreRender")
    '    End Try
    'End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            lblError.Text = ""

            ReportViewer1.Reset()
            If ddlDepBasis.SelectedValue = 1 Then

                If dt.Rows.Count = 0 Then
                    lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                    Exit Sub
                End If

                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetDepComp.rdlc")
            ElseIf ddlDepBasis.SelectedValue = 2 Then

                If dtITAct.Rows.Count = 0 Then
                    lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                    Exit Sub
                End If

                Dim rds As New ReportDataSource("DataSet1", dtITAct)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetITAct.rdlc")
            End If
            ReportViewer1.LocalReport.Refresh()
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim Method As ReportParameter() = New ReportParameter() {New ReportParameter("Method", ddlMethod.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Method)
            Dim DepreciaitonforFY As ReportParameter() = New ReportParameter() {New ReportParameter("DepreciaitonforFY", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(DepreciaitonforFY)
            Dim DepBasis As ReportParameter() = New ReportParameter() {New ReportParameter("DepBasis", ddlDepBasis.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(DepBasis)

            If rbtDuration.SelectedIndex = 3 Then
                Dim Month As ReportParameter() = New ReportParameter() {New ReportParameter("Month", "Month :" + " " + ddlDurationmonth.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Month)
            Else
                'Dim Month As ReportParameter() = New ReportParameter() {New ReportParameter("Month", "")}
                'ReportViewer1.LocalReport.SetParameters(Month)
            End If

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            '  objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "PDF", ddlFinancialYear.SelectedValue , sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetDepreciation" + ".xls")
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
            If dt.Rows.Count = 0 Then
                lblPaymentMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetDepComp.rdlc")
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            '  objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "fixedasset", "AssetRegReport", "PDF", ddlFinancialYear.SelectedValue , sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=AssetDepreciation" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub loadDurationMonth()
        Try
            ddlDurationmonth.Items.Add(New ListItem("Select Month", 0))
            ddlDurationmonth.Items.Add(New ListItem("January", 1))
            ddlDurationmonth.Items.Add(New ListItem("February", 2))
            ddlDurationmonth.Items.Add(New ListItem("March", 3))
            ddlDurationmonth.Items.Add(New ListItem("April", 4))
            ddlDurationmonth.Items.Add(New ListItem("May", 5))
            ddlDurationmonth.Items.Add(New ListItem("June", 6))
            ddlDurationmonth.Items.Add(New ListItem("July", 7))
            ddlDurationmonth.Items.Add(New ListItem("August", 8))
            ddlDurationmonth.Items.Add(New ListItem("September", 9))
            ddlDurationmonth.Items.Add(New ListItem("October", 10))
            ddlDurationmonth.Items.Add(New ListItem("November", 11))
            ddlDurationmonth.Items.Add(New ListItem("December", 12))
            ddlDurationmonth.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadDurationMonth" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub DurationQuarterly()
        Try
            ddlDurationQuarter.Items.Add(New ListItem("Select Month", 0))
            ddlDurationQuarter.Items.Add(New ListItem("Apr-Jun", 1))
            ddlDurationQuarter.Items.Add(New ListItem("Jul-Sep", 2))
            ddlDurationQuarter.Items.Add(New ListItem("Oct-Dec", 3))
            ddlDurationQuarter.Items.Add(New ListItem("Jan-Mar", 4))
            ddlDurationQuarter.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DurationQuarterly" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub DurationHalfYearly()
        Try
            ddlDurationhalfyear.Items.Add(New ListItem("Select Month", 0))
            ddlDurationhalfyear.Items.Add(New ListItem("Apr-Sep", 1))
            ddlDurationhalfyear.Items.Add(New ListItem("Oct-Mar", 2))
            ddlDurationhalfyear.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DurationHalfYearly" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub dgDepAsperITAct_DataBound1(sender As Object, e As EventArgs)

    End Sub

    Protected Sub OnRadio_Changed(sender As Object, e As EventArgs)
        Try
            dgDepComp.DataSource = Nothing
            dgDepComp.DataBind()

            PnlDurationMonthly.Visible = False
            pnlyear.Visible = False : pnlQuarterly.Visible = False
            pnlBankDaybook.Visible = False : pnlHalfYearly.Visible = False
            'ddlDurationmonth.SelectedIndex = 0
            'ddlDurationQuarter.SelectedIndex = 0 : ddlDurationhalfyear.SelectedIndex = 0
            txtFromDate.Text = "" : txtToDate.Text = ""

            If rbtDuration.SelectedIndex = 0 Then
            ElseIf rbtDuration.SelectedIndex = 1 Then
                pnlHalfYearly.Visible = True
                DurationHalfYearly()
            ElseIf rbtDuration.SelectedIndex = 2 Then
                pnlQuarterly.Visible = True
                DurationQuarterly()
            ElseIf rbtDuration.SelectedIndex = 3 Then
                PnlDurationMonthly.Visible = True
                loadDurationMonth()
            ElseIf rbtDuration.SelectedIndex = 4 Then
                pnlBankDaybook.Visible = True
            ElseIf rbtDuration.SelectedIndex = 5 Then
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "OnRadio_Changed" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim dttotal As DataTable
        Dim m As Integer
        Dim dDate, dSDate As Date
        Dim dToDate, dSToDate As Date
        'Dim dtITAct As New DataTable
        ' Dim dOpenBal As Double : Dim dDepreciation As Double : Dim dWDV As Double
        Try
            lblError.Text = ""

            dt = Nothing
            If (txtFromDate.Text <> "") Then
                dDate = Date.ParseExact(FStartDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "Start Date (" & txtFromDate.Text & ") should be Greater than or equal to Financial Year Start Date(" & FStartDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If
                dDate = Date.ParseExact(FEndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m > 0 Then
                    lblError.Text = "Start Date (" & txtFromDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & FEndDate & ")."
                    txtFromDate.Focus()
                    Exit Sub
                End If
            End If

            If (txtToDate.Text <> "") Then
                dToDate = Date.ParseExact(FEndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dToDate, dSToDate)
                If m > 0 Then
                    lblError.Text = "Stop Date Date (" & txtToDate.Text & ") should be Lesser than or equal to Financial Year End Date(" & FEndDate & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If
                dDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                m = DateDiff(DateInterval.Day, dDate, dSDate)
                If m < 0 Then
                    lblError.Text = "To Date (" & txtToDate.Text & ") should be Greater than From Date(" & txtFromDate.Text & ")."
                    txtToDate.Focus()
                    Exit Sub
                End If
            End If

            Dim sYear As String
            Dim sYear1 As String
            Dim ayear As Array
            ayear = ddlFinancialYear.SelectedItem.Text.Split("-")
            sYear1 = Trim(ayear(1))
            sYear = Trim(ayear(0))

            Dim Yearly As Integer
            Startdate = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) ' Yearly 
            Enddate = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim ts As TimeSpan = Enddate - Startdate
            Yearly = ts.TotalDays + 1

            '  Dim iMethod As Integer = objDepComp.LoadFixedAsesetSetting(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

            'If iMethod = 0 Then
            '    lblError.Text = "Set the Depreciation Method in Masters."
            '    lblPaymentMasterValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)
            '    Exit Sub
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name."
                lblPaymentMasterValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDepBasis.SelectedIndex = 0 Then
                lblError.Text = "Select Depreciation Basis."
                lblPaymentMasterValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlMethod.SelectedIndex = 0 Then
                lblError.Text = "Select Method of Depreciation."
                lblPaymentMasterValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDepreciationValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDepBasis.SelectedIndex = 1 Then

                dgDepComp.Visible = True
                dgDepAsperITAct.Visible = False

                If ddlMethod.SelectedIndex = 1 Then

                    If rbtDuration.SelectedIndex = 0 Then
                        dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Yearly, Yearly, rbtDuration.SelectedIndex, Startdate, Enddate, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    ElseIf rbtDuration.SelectedIndex = 1 Then

                        If ddlDurationhalfyear.SelectedIndex = 1 Then
                            Dim Frommonth As Date = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) '  halfyear
                            Dim Tomonth As Date = Date.ParseExact("30/09/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim halfyear As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, halfyear, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        ElseIf ddlDurationhalfyear.SelectedIndex = 2 Then

                            Dim Frommonth As Date = Date.ParseExact("01/10/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) '  halfyear
                            Dim Tomonth As Date = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim halfyear As Integer = hyear.TotalDays


                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, halfyear, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        End If

                    ElseIf rbtDuration.SelectedIndex = 2 Then 'Quaterly
                        If ddlDurationQuarter.SelectedIndex = 1 Then

                            Dim Frommonth As Date = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("30/06/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 2 Then

                            Dim Frommonth As Date = Date.ParseExact("01/07/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("30/09/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays


                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 3 Then

                            Dim Frommonth As Date = Date.ParseExact("01/10/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("31/12/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 4 Then

                            Dim Frommonth As Date = Date.ParseExact("01/01/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        End If

                    ElseIf rbtDuration.SelectedIndex = 3 Then 'Monthly
                        Dim iMonth As Integer
                        If ddlDurationmonth.SelectedIndex = 1 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 1)
                        ElseIf ddlDurationmonth.SelectedIndex = 2 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 2)
                        ElseIf ddlDurationmonth.SelectedIndex = 3 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 3)
                        ElseIf ddlDurationmonth.SelectedIndex = 4 Then
                            iMonth = DateTime.DaysInMonth(sYear, 4)
                        ElseIf ddlDurationmonth.SelectedIndex = 5 Then
                            iMonth = DateTime.DaysInMonth(sYear, 5)
                        ElseIf ddlDurationmonth.SelectedIndex = 6 Then
                            iMonth = DateTime.DaysInMonth(sYear, 6)
                        ElseIf ddlDurationmonth.SelectedIndex = 7 Then
                            iMonth = DateTime.DaysInMonth(sYear, 7)
                        ElseIf ddlDurationmonth.SelectedIndex = 8 Then
                            iMonth = DateTime.DaysInMonth(sYear, 8)
                        ElseIf ddlDurationmonth.SelectedIndex = 9 Then
                            iMonth = DateTime.DaysInMonth(sYear, 9)
                        ElseIf ddlDurationmonth.SelectedIndex = 10 Then
                            iMonth = DateTime.DaysInMonth(sYear, 10)
                        ElseIf ddlDurationmonth.SelectedIndex = 11 Then
                            iMonth = DateTime.DaysInMonth(sYear, 11)
                        ElseIf ddlDurationmonth.SelectedIndex = 12 Then
                            iMonth = DateTime.DaysInMonth(sYear, 12)
                        End If

                        Dim sdate As String
                        Dim s2date As String
                        If ddlDurationmonth.SelectedIndex = 1 Or ddlMethod.SelectedIndex = 2 Or ddlMethod.SelectedIndex = 3 Then
                            sdate = "01/" & ddlDurationmonth.SelectedIndex & "/" & sYear1
                            s2date = iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear1
                        Else
                            sdate = "01/" & ddlDurationmonth.SelectedIndex & "/" & sYear
                            s2date = iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear
                        End If

                        Dim Frommonth As Date = Convert.ToDateTime(sdate).ToString("dd/MM/yyyy")
                        Dim Tomonth As Date = Convert.ToDateTime(s2date).ToString("dd/MM/yyyy")

                        'Dim Frommonth As Date = Date.ParseExact("01/" & ddlDurationmonth.SelectedIndex & "/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        ' Dim Tomonth As Date = Date.ParseExact(iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim hyear As TimeSpan = Tomonth - Frommonth
                        Dim iMonthly As Integer = hyear.TotalDays + 1
                        dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iMonthly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    ElseIf rbtDuration.SelectedIndex = 4 Then 'Customized

                        Dim Frommonth As Date = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim Tomonth As Date = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        Dim hyear As TimeSpan = Tomonth - Frommonth
                        Dim Customized As Integer = hyear.TotalDays

                        dt = objDepComp.LoadDepreciationCompSLM(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Customized, Yearly, rbtDuration.SelectedIndex, Frommonth, Enddate, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    End If

                ElseIf ddlMethod.SelectedIndex = 2 Then

                    If rbtDuration.SelectedIndex = 0 Then
                        dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Yearly, Yearly, rbtDuration.SelectedIndex, Startdate, Enddate, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    ElseIf rbtDuration.SelectedIndex = 1 Then

                        If ddlDurationhalfyear.SelectedIndex = 1 Then
                            Dim Frommonth As Date = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) '  halfyear
                            Dim Tomonth As Date = Date.ParseExact("30/09/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim halfyear As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, halfyear, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        ElseIf ddlDurationhalfyear.SelectedIndex = 2 Then

                            Dim Frommonth As Date = Date.ParseExact("01/10/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) '  halfyear
                            Dim Tomonth As Date = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim halfyear As Integer = hyear.TotalDays


                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, halfyear, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        End If

                    ElseIf rbtDuration.SelectedIndex = 2 Then 'Quaterly
                        If ddlDurationQuarter.SelectedIndex = 1 Then

                            Dim Frommonth As Date = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("30/06/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 2 Then

                            Dim Frommonth As Date = Date.ParseExact("01/07/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("30/09/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays


                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 3 Then

                            Dim Frommonth As Date = Date.ParseExact("01/10/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("31/12/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                        ElseIf ddlDurationQuarter.SelectedIndex = 4 Then

                            Dim Frommonth As Date = Date.ParseExact("01/01/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Dim Tomonth As Date = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            Dim hyear As TimeSpan = Tomonth - Frommonth
                            Dim Quaterly As Integer = hyear.TotalDays

                            dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Quaterly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)
                        End If

                    ElseIf rbtDuration.SelectedIndex = 3 Then 'Monthly
                        Dim iMonth As Integer
                        If ddlDurationmonth.SelectedIndex = 1 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 1)
                        ElseIf ddlDurationmonth.SelectedIndex = 2 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 2)
                        ElseIf ddlDurationmonth.SelectedIndex = 3 Then
                            iMonth = DateTime.DaysInMonth(sYear1, 3)
                        ElseIf ddlDurationmonth.SelectedIndex = 4 Then
                            iMonth = DateTime.DaysInMonth(sYear, 4)
                        ElseIf ddlDurationmonth.SelectedIndex = 5 Then
                            iMonth = DateTime.DaysInMonth(sYear, 5)
                        ElseIf ddlDurationmonth.SelectedIndex = 6 Then
                            iMonth = DateTime.DaysInMonth(sYear, 6)
                        ElseIf ddlDurationmonth.SelectedIndex = 7 Then
                            iMonth = DateTime.DaysInMonth(sYear, 7)
                        ElseIf ddlDurationmonth.SelectedIndex = 8 Then
                            iMonth = DateTime.DaysInMonth(sYear, 8)
                        ElseIf ddlDurationmonth.SelectedIndex = 9 Then
                            iMonth = DateTime.DaysInMonth(sYear, 9)
                        ElseIf ddlDurationmonth.SelectedIndex = 10 Then
                            iMonth = DateTime.DaysInMonth(sYear, 10)
                        ElseIf ddlDurationmonth.SelectedIndex = 11 Then
                            iMonth = DateTime.DaysInMonth(sYear, 11)
                        ElseIf ddlDurationmonth.SelectedIndex = 12 Then
                            iMonth = DateTime.DaysInMonth(sYear, 12)
                        End If
                        Dim sdate As String
                        Dim s2date As String
                        'If ddlDurationmonth.SelectedIndex = 1 Or ddlMethod.SelectedIndex = 2 Or ddlMethod.SelectedIndex = 3 Then "cmntd by Darshan(23-04-24) April mnth corection
                        If ddlDurationmonth.SelectedIndex = 1 Or ddlDurationmonth.SelectedIndex = 2 Or ddlDurationmonth.SelectedIndex = 3 Then
                            sdate = "01/" & ddlDurationmonth.SelectedIndex & "/" & sYear1
                            s2date = iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear1
                        Else
                            sdate = "01/" & ddlDurationmonth.SelectedIndex & "/" & sYear
                            s2date = iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear
                        End If


                        Dim Frommonth As Date = Convert.ToDateTime(sdate).ToString("dd/MM/yyyy")
                        Dim Tomonth As Date = Convert.ToDateTime(s2date).ToString("dd/MM/yyyy")

                        '  Dim Frommonth As Date = Date.ParseExact("01/" & ddlDurationmonth.SelectedIndex & "/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        ' Dim Tomonth As Date = Date.ParseExact(iMonth & "/" & ddlDurationmonth.SelectedIndex & "/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim hyear As TimeSpan = Tomonth - Frommonth
                        Dim iMonthly As Integer = hyear.TotalDays + 1
                        dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iMonthly, Yearly, rbtDuration.SelectedIndex, Frommonth, Tomonth, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    ElseIf rbtDuration.SelectedIndex = 4 Then 'Customized

                        Dim Frommonth As Date = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Dim Tomonth As Date = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        Dim hyear As TimeSpan = Tomonth - Frommonth
                        Dim Customized As Integer = hyear.TotalDays

                        dt = objDepComp.LoadDepreciationCompWDV(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, Customized, Yearly, rbtDuration.SelectedIndex, Frommonth, Enddate, ddlCustomerName.SelectedValue, ddlMethod.SelectedValue)

                    End If
                End If

                dgDepComp.DataSource = dt
                dgDepComp.DataBind()
            End If
            If ddlDepBasis.SelectedIndex = 2 Then
                dgDepComp.Visible = False
                dgDepAsperITAct.Visible = True

                dtITAct = objDepComp.LoadDepreciationITAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, Enddate)
                ' dts = objDepComp.DbExport(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue , dtITAct, ddlCustomerName.SelectedValue, objDepComp, sSession.UserID, sSession.IPAddress)

                dgDepAsperITAct.DataSource = Nothing
                dgDepAsperITAct.DataSource = dtITAct
                dgDepAsperITAct.DataBind()

            End If

            Try
                If (ddlCustomerName.SelectedIndex > 0) Then
                    Dim dtAdd As New DataTable
                    dtAdd = objDepComp.getCount(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    If dtAdd.Rows.Count > 0 Then
                        Dim openingBalanceCount As Integer = 0
                        Dim additionCount As Integer = 0

                        If dtAdd.Rows.Count > 0 Then
                            Dim trType1 As Integer = Convert.ToInt32(dtAdd.Rows(0)("Count"))
                            openingBalanceCount = trType1
                            Dim trType2 As Integer = Convert.ToInt32(dtAdd.Rows(1)("Count"))
                            additionCount = trType2
                        End If

                        If openingBalanceCount > 0 Then
                                lblOpeningBalance.Text = "Opening Balance: " & openingBalanceCount.ToString()
                            Else
                                lblOpeningBalance.Text = "Opening Balance: 0"
                            End If

                            If additionCount > 0 Then
                                lblAddition.Text = "Addition: " & additionCount.ToString()
                            Else
                                lblAddition.Text = "Addition: 0"
                            End If
                        Else
                            lblOpeningBalance.Text = "Opening Balance: 0"
                        lblAddition.Text = "Addition: 0"
                    End If
                End If
            Catch ex As Exception
            End Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDepBasis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepBasis.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlMethod.Items.Clear()
            If ddlDepBasis.SelectedIndex = 1 Then
                PnlCompanyAct.Visible = True
                dgDepComp.Visible = True
                dgDepAsperITAct.Visible = False

                ddlMethod.Items.Add(New ListItem("Select", 0))
                ddlMethod.Items.Add(New ListItem("SLM", 1))
                ddlMethod.Items.Add(New ListItem("WDV", 2))
                ddlMethod.SelectedIndex = 0

            ElseIf ddlDepBasis.SelectedIndex = 2 Then
                PnlCompanyAct.Visible = True
                dgDepComp.Visible = False
                dgDepAsperITAct.Visible = True

                ddlMethod.Items.Add(New ListItem("Select", 0))
                ddlMethod.Items.Add(New ListItem("WDV", 1))
                ddlMethod.SelectedIndex = 0

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDepBasis_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Private Sub dgDepAsperITAct_DataBound1(sender As Object, e As GridViewRowEventArgs) Handles dgDepAsperITAct.RowDataBound
    '    Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
    '    Dim cell As New TableHeaderCell()
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            cell.Text = "Customers"
    '            cell.ColumnSpan = 2
    '            row.Controls.Add(cell)

    '            cell = New TableHeaderCell()
    '            cell.ColumnSpan = 2
    '            cell.Text = "Employees"
    '            row.Controls.Add(cell)

    '            row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
    '            YourGridView.HeaderRow.Parent.Controls.AddAt(0, row)
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub


    Private Sub dgDepAsperITAct_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDepAsperITAct.RowDataBound
        Dim lblWDVOpeningValue As New Label
        Dim lblFWDVOpeningValue As New Label

        Dim lblAdditionDuringtheYear As New Label
        Dim lblFAdditionDuringtheYear As New Label

        Dim lblDepfortheperiod As New Label
        Dim lblFDepfortheperiod As New Label

        Dim lblWDVClosingValue As New Label
        Dim lblFWDVClosingValue As New Label

        Try


            If e.Row.RowType = DataControlRowType.DataRow Then
                lblWDVOpeningValue = e.Row.FindControl("lblWDVOpeningValue")
                If lblWDVOpeningValue.Text <> "" Then
                    dWDVOpeningValueIT = dWDVOpeningValueIT + Convert.ToDecimal(lblWDVOpeningValue.Text)
                End If



                lblAdditionDuringtheYear = e.Row.FindControl("lblAdditionDuringtheYear")
                If lblAdditionDuringtheYear.Text <> "" Then
                    dAdditionYear = dAdditionYear + Convert.ToDecimal(lblAdditionDuringtheYear.Text)
                End If

                lblDepfortheperiod = e.Row.FindControl("lblDepfortheperiod")
                If lblDepfortheperiod.Text <> "" Then
                    dDepPeriodIT = dDepPeriodIT + Convert.ToDecimal(lblDepfortheperiod.Text)
                End If

                lblWDVClosingValue = e.Row.FindControl("lblWDVClosingValue")
                If lblWDVClosingValue.Text <> "" Then
                    dWDVClosingValueIT = dWDVClosingValueIT + Convert.ToDecimal(lblWDVClosingValue.Text)
                End If
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                lblFWDVOpeningValue = e.Row.FindControl("lblFWDVOpeningValue")
                lblFWDVOpeningValue.Text = Convert.ToDecimal(dWDVOpeningValueIT).ToString("#,##0")

                lblFAdditionDuringtheYear = e.Row.FindControl("lblFAdditionDuringtheYear")
                lblFAdditionDuringtheYear.Text = Convert.ToDecimal(dAdditionYear).ToString("#,##0")

                lblFDepfortheperiod = e.Row.FindControl("lblFDepfortheperiod")
                lblFDepfortheperiod.Text = Convert.ToDecimal(dDepPeriodIT).ToString("#,##0")

                lblFWDVClosingValue = e.Row.FindControl("lblFWDVClosingValue")
                lblFWDVClosingValue.Text = Convert.ToDecimal(dWDVClosingValueIT).ToString("#,##0")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDepAsperITAct_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlMethod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMethod.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlDepBasis.SelectedIndex = 1 Then
                dgDepAsperITAct.DataSource = Nothing
                dgDepAsperITAct.Visible = False
                dgDepComp.Visible = True

            ElseIf ddlDepBasis.SelectedIndex = 2 Then
                dgDepComp.DataSource = Nothing
                dgDepComp.Visible = False
                dgDepAsperITAct.Visible = True

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMethod_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
    Private Sub dgDepAsperITAct_DataBound(sender As Object, e As EventArgs) Handles dgDepAsperITAct.DataBound
        dgDepAsperITAct.Columns(5).ItemStyle.HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub dgDepComp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDepComp.RowDataBound
        'Dim TxtOpeningBalance As New Label
        'Dim txtDepreciation As New Label
        'Dim txtWDV As New Label
        'Dim lblOPBForYRTotal As New Label
        'Dim lblDepreciationforFYTotal As New Label
        'Dim lblwrtnvalueTotal As New Label

        Dim lblFOrignalCost As New Label
        Dim lblOrignalCost As New Label

        Dim lblFSalvageValue As New Label
        Dim lblSalvageValue As New Label

        Dim lblFOPBForYR As New Label
        Dim lblOPBForYR As New Label

        Dim lblFwrtnvalue As New Label
        Dim lblwrtnvalue As New Label

        Dim lblFDepreciationforFY As New Label
        Dim lblDepreciationforFY As New Label

        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblOrignalCost = e.Row.FindControl("lblOrignalCost")
                If lblOrignalCost.Text <> "" Then
                    dOriginalCost = dOriginalCost + Convert.ToDecimal(lblOrignalCost.Text)
                End If

                lblSalvageValue = e.Row.FindControl("lblSalvageValue")
                If lblSalvageValue.Text <> "" Then
                    dResidualValue = dResidualValue + Convert.ToDecimal(lblSalvageValue.Text)
                End If

                lblOPBForYR = e.Row.FindControl("lblOPBForYR")
                If lblOPBForYR.Text <> "" Then
                    dWDVOpeningValue = dWDVOpeningValue + Convert.ToDecimal(lblOPBForYR.Text)
                End If

                lblwrtnvalue = e.Row.FindControl("lblwrtnvalue")
                If lblwrtnvalue.Text <> "" Then
                    dWDVClosingValue = dWDVClosingValue + Convert.ToDecimal(lblwrtnvalue.Text)
                End If

                lblDepreciationforFY = e.Row.FindControl("lblDepreciationforFY")
                If lblDepreciationforFY.Text <> "" Then
                    dDepPeriod = dDepPeriod + Convert.ToDecimal(lblDepreciationforFY.Text)
                End If
            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                lblFOrignalCost = e.Row.FindControl("lblFOrignalCost")
                lblFOrignalCost.Text = Convert.ToDecimal(dOriginalCost).ToString("#,##0")

                lblFSalvageValue = e.Row.FindControl("lblFSalvageValue")
                lblFSalvageValue.Text = Convert.ToDecimal(dResidualValue).ToString("#,##0")

                lblFOPBForYR = e.Row.FindControl("lblFOPBForYR")
                lblFOPBForYR.Text = Convert.ToDecimal(dWDVOpeningValue).ToString("#,##0")

                lblFwrtnvalue = e.Row.FindControl("lblFwrtnvalue")
                lblFwrtnvalue.Text = Convert.ToDecimal(dWDVClosingValue).ToString("#,##0")

                lblFDepreciationforFY = e.Row.FindControl("lblFDepreciationforFY")
                lblFDepreciationforFY.Text = Convert.ToDecimal(dDepPeriod).ToString("#,##0")
            End If
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    TxtOpeningBalance = e.Row.FindControl("lblOPBForYR")
            '    txtDepreciation = e.Row.FindControl("lblDepreciationforFY")
            '    txtWDV = e.Row.FindControl("lblwrtnvalue")

            '    If TxtOpeningBalance.Text <> "" Then
            '        dOpeningBal = dOpeningBal + Convert.ToDecimal(TxtOpeningBalance.Text)
            '    End If
            '    If txtDepreciation.Text <> "" Then
            '        dDepreciation = dDepreciation + Convert.ToDecimal(txtDepreciation.Text)
            '    End If
            '    If txtWDV.Text <> "" Then
            '        dWDV = dDepreciation + Convert.ToDecimal(txtWDV.Text)
            '    End If
            'End If

            'If e.Row.RowType = DataControlRowType.Footer Then

            '    lblOPBForYRTotal = e.Row.FindControl("lblOPBForYRTotal")
            '    lblOPBForYRTotal.Text = Convert.ToDecimal(dOpeningBal).ToString("#,##0")

            '    lblDepreciationforFYTotal = e.Row.FindControl("lblDepreciationforFYTotal")
            '    lblDepreciationforFYTotal.Text = Convert.ToDecimal(dDepreciation).ToString("#,##0")

            '    lblwrtnvalueTotal = e.Row.FindControl("lblwrtnvalueTotal")
            '    lblwrtnvalueTotal.Text = Convert.ToDecimal(dWDV).ToString("#,##0")

            '    e.Row.Cells(7).Font.Bold = True
            'End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlMethod_TextChanged(sender As Object, e As EventArgs) Handles ddlMethod.TextChanged

    End Sub

    'Private Sub YourGridView_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles YourGridView.RowDataBound
    '    Dim row As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal)
    '    Dim cell As New TableHeaderCell()
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            cell.Text = "Customers"
    '            cell.ColumnSpan = 2
    '            row.Controls.Add(cell)

    '            cell = New TableHeaderCell()
    '            cell.ColumnSpan = 2
    '            cell.Text = "Employees"
    '            row.Controls.Add(cell)

    '            row.BackColor = ColorTranslator.FromHtml("#3AC0F2")
    '            YourGridView.HeaderRow.Parent.Controls.AddAt(0, row)
    '        End If


    '    Catch ex As Exception

    '    End Try
    'End Sub


    'Private Sub dgDepComp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDepComp.RowDataBound
    '    Try
    '        lblError.Text = ""
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            e.Row.Cells(7).HorizontalAlign = HorizontalAlign.Right
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Sub dgDepComp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDepComp.RowDataBound
    '    Dim TxtOpeningBalance As New Label
    '    Dim txtDepreciation As New Label
    '    Dim txtWDV As New Label
    '    Dim lblOPBForYRTotal As New Label
    '    Dim lblDepreciationforFYTotal As New Label
    '    Dim lblwrtnvalueTotal As New Label
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            TxtOpeningBalance = e.Row.FindControl("lblOPBForYR")
    '            txtDepreciation = e.Row.FindControl("lblDepreciationforFY")
    '            txtWDV = e.Row.FindControl("lblwrtnvalue")

    '            If TxtOpeningBalance.Text <> "" Then
    '                dOpeningBal = dOpeningBal + Convert.ToDecimal(TxtOpeningBalance.Text)
    '            End If
    '            If txtDepreciation.Text <> "" Then
    '                dDepreciation = dDepreciation + Convert.ToDecimal(txtDepreciation.Text)
    '            End If
    '            If txtWDV.Text <> "" Then
    '                dWDV = dDepreciation + Convert.ToDecimal(txtWDV.Text)
    '            End If
    '        End If

    '        If e.Row.RowType = DataControlRowType.Footer Then

    '            lblOPBForYRTotal = e.Row.FindControl("lblOPBForYRTotal")
    '            lblOPBForYRTotal.Text = Convert.ToDecimal(dOpeningBal).ToString("#,##0")

    '            lblDepreciationforFYTotal = e.Row.FindControl("lblDepreciationforFYTotal")
    '            lblDepreciationforFYTotal.Text = Convert.ToDecimal(dDepreciation).ToString("#,##0")

    '            lblwrtnvalueTotal = e.Row.FindControl("lblwrtnvalueTotal")
    '            lblwrtnvalueTotal.Text = Convert.ToDecimal(dWDV).ToString("#,##0")

    '            e.Row.Cells(7).Font.Bold = True
    '        End If

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Private Sub ddlDurationmonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationmonth.SelectedIndexChanged
    '    Dim sFdate As String
    '    Dim ayear As Array
    '    Dim sYear As String
    '    Dim smonthid As String
    '    Dim EOMDate As Date
    '    Try
    '        If rbtDuration.SelectedIndex = 4 Then

    '        Else
    '            If ddlDurationmonth.SelectedIndex = 1 Or ddlDurationmonth.SelectedIndex = 2 Or ddlDurationmonth.SelectedIndex = 3 Then
    '                ayear = sSession.YearName.Split("-")
    '                sYear = ayear(1)
    '            Else
    '                ayear = sSession.YearName.Split("-")
    '                sYear = ayear(0)
    '            End If
    '            If ddlDurationmonth.SelectedIndex = 10 Or ddlDurationmonth.SelectedIndex = 11 Or ddlDurationmonth.SelectedIndex = 12 Then
    '                smonthid = ddlDurationmonth.SelectedIndex
    '            Else
    '                smonthid = "0" & ddlDurationmonth.SelectedIndex
    '            End If
    '            If rbtDuration.SelectedIndex = 3 Then
    '                drtFRMMonth = smonthid & "/01/" & sYear
    '                sFdate = sYear & "-" & smonthid & "-01"
    '                EOMDate = objReports.getEomDate(sSession.AccessCode, sFdate)
    '                drtTOmonth = Convert.ToString(EOMDate.ToString("MM/dd/yyyy"))
    '            End If
    '            btnGo_Click(sender, e)
    '        End If


    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Private Sub ddlDurationQuarter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationQuarter.SelectedIndexChanged
    '    Dim sYear As String
    '    Dim ayear As Array
    '    Try
    '        If ddlDurationQuarter.SelectedIndex = 4 Then
    '            ayear = sSession.YearName.Split("-")
    '            sYear = ayear(1)
    '        Else
    '            ayear = sSession.YearName.Split("-")
    '            sYear = ayear(0)
    '        End If
    '        If ddlDurationQuarter.SelectedIndex = 1 Then
    '            drtFRMMonth = "04/01/" & sYear
    '            drtTOmonth = "06/30/" & sYear
    '        ElseIf ddlDurationQuarter.SelectedIndex = 2 Then
    '            drtFRMMonth = "07/01/" & sYear
    '            drtTOmonth = "09/30/" & sYear
    '        ElseIf ddlDurationQuarter.SelectedIndex = 3 Then
    '            drtFRMMonth = "10/01/" & sYear
    '            drtTOmonth = "12/31/" & sYear
    '        ElseIf ddlDurationQuarter.SelectedIndex = 4 Then
    '            drtFRMMonth = "01/01/" & sYear
    '            drtTOmonth = "03/31/" & sYear
    '        End If
    '        btnGo_Click(sender, e)
    '        ' ddlReports_SelectedIndexChanged(sender, e)
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub ddlDurationhalfyear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDurationhalfyear.SelectedIndexChanged
    '    Dim sYear As String
    '    Dim sYear1 As String
    '    Dim ayear As Array
    '    Try
    '        ' If ddlDurationhalfyear.SelectedIndex = 1 Then
    '        ayear = sSession.YearName.Split("-")
    '        sYear1 = ayear(1)
    '        sYear = ayear(0)
    '        ' Else
    '        'ayear = sSession.YearName.Split("-")
    '        '    sYear = ayear(1)
    '        '    sYear1 = ayear(0)
    '        ' End If
    '        If ddlDurationhalfyear.SelectedIndex = 1 Then
    '            drtFRMMonth = "04/01/" & sYear
    '            drtTOmonth = "09/30/" & sYear
    '        Else
    '            drtFRMMonth = "10/01/" & sYear
    '            drtTOmonth = "03/31/" & sYear1
    '        End If
    '        btnGo_Click(sender, e)
    '        ' ddlReports_SelectedIndexChanged(sender, e)
    '    Catch ex As Exception
    '    End Try
    'End Sub
End Class
