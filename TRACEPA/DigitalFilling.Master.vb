Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class Digital_AuditOffice
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Digital_AuditOffice Masterpage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsLogin As New clsLogin
    Private objclsCPFP As New clsCPFP
    Private objUser As New clsCPFP.UserProfile
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnTRACeLog.ImageUrl = "Images/logo.png"
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Page.ViewStateUserKey = Session.SessionID
        If Session("AuthToken") IsNot Nothing AndAlso Request.Cookies("AuthToken") IsNot Nothing Then
            If Not Session("AuthToken").ToString().Equals(Request.Cookies("AuthToken").Value) Then
                Response.Redirect("~/LoginPage.aspx", False)
                Exit Sub
            End If
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim intSessionTimeOut As Integer, intSessionTimeOutWarning As Integer
        Try
            sSession = Session("AllSession")
            BindModules(sSession.Modules)
            'mainForm.Action=Request.RawUrl
            intSessionTimeOut = sSession.TimeOut
            intSessionTimeOutWarning = sSession.TimeOutWarning
            lblTimeOutWarning.Text = "Your TRACe session will expire in " & (sSession.TimeOutWarning / 60000) & " mins! Please Save the data before the session expires."
            bdyProgramMaster.Attributes.Add("onload", "javascript:return checkTime(" + intSessionTimeOut.ToString + "," + intSessionTimeOutWarning.ToString + ");")
            lblUserName.Text = "Welcome" & " " & sSession.UserFullNameCode

            RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & sSession.MinPasswordCharacter & "," & sSession.MaxPasswordCharacter & "}"
            lblCONote.Text = "Password must contain minimum " & sSession.MinPasswordCharacter & " characters, maximum " & sSession.MaxPasswordCharacter & " characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special character."
            CVCurrentPasssword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            CVCheckPassword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            REVMobNo.ErrorMessage = "Enter valid Mobile No." : REVMobNo.ValidationExpression = "^[0-9]{10}$"

            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            RFVSecurityQuestion.ErrorMessage = "Enter Security Question." : REVSecurityQuestion.ValidationExpression = "^(.{0,250})$"
            REVSecurityQuestion.ErrorMessage = "Security Question exceeded maximum size(max 250 characters)."

            RFVAnswer.ErrorMessage = "Enter Answer." : REVAnswer.ValidationExpression = "^(.{0,250})$"
            REVAnswer.ErrorMessage = "Answer exceeded maximum size(max 250 characters)."

            REVExperiencesummary.ValidationExpression = "^(.{0,8000})$" : REVExperiencesummary.ErrorMessage = "Experience Summary exceeded maximum size(max 8000 characters)."

            REVOthers.ValidationExpression = "^(.{0,5000})$" : REVOthers.ErrorMessage = "Other qualification exceeded maximum size(max 5000 characters)."

            lnkbtnMyProfile.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('show');$('#txtCheckPassword').focus();return false;")
            lnkbtnChangePassword.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('show');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');return false;")
            If sSession.Menu = "Digital_AuditOffice" Then
                GetSubMenuOpen()
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Public Sub BindModules(ByVal sModules As String)
        Try
            lnkbtnAUDIT.Visible = False
            lnkbtnASSIGNMENTS.Visible = False
            lnkbtnCorporateAudit.Visible = False
            lnkbtnAccounts.Visible = False
            lnkbtnDigital_AuditOffice.Visible = True
            lnkbtnRISK.Visible = False
            lnkbtnCOMPLIANCE.Visible = False

            'If sModules.ToUpper.Contains(",AUDIT,") Then
            '    lnkbtnAUDIT.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",ASSIGNMENTS,") Then
            '    lnkbtnASSIGNMENTS.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",CHECKLIST BASED AUDIT,") Then
            '    lnkbtnCorporateAudit.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",FINALISATION OF ACCOUNTS,") Then
            '    lnkbtnAccounts.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",DIGITAL OFFICE,") Then
            '    lnkbtnDigital_AuditOffice.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",RISK,") Then
            '    lnkbtnRISK.Visible = True
            'End If
            'If sModules.ToUpper.Contains(",COMPLIANCE,") Then
            '    lnkbtnCOMPLIANCE.Visible = True
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindExperience()
        Try
            ddlExperience.Items.Clear()
            ddlExperience.Items.Insert(0, "0")
            ddlExperience.Items.Insert(1, "1")
            ddlExperience.Items.Insert(2, "2")
            ddlExperience.Items.Insert(3, "3")
            ddlExperience.Items.Insert(4, "4")
            ddlExperience.Items.Insert(5, "5")
            ddlExperience.Items.Insert(6, "6")
            ddlExperience.Items.Insert(7, "7")
            ddlExperience.Items.Insert(8, "8")
            ddlExperience.Items.Insert(9, "9")
            ddlExperience.Items.Insert(10, "10")
            ddlExperience.Items.Insert(11, "11")
            ddlExperience.Items.Insert(12, "12")
            ddlExperience.Items.Insert(13, "13")
            ddlExperience.Items.Insert(14, "14")
            ddlExperience.Items.Insert(15, "15")
            ddlExperience.Items.Insert(16, "16")
            ddlExperience.Items.Insert(17, "17")
            ddlExperience.Items.Insert(18, "18")
            ddlExperience.Items.Insert(19, "19")
            ddlExperience.Items.Insert(20, "20")
            ddlExperience.Items.Insert(21, "21")
            ddlExperience.Items.Insert(22, "22")
            ddlExperience.Items.Insert(23, "23")
            ddlExperience.Items.Insert(24, "24")
            ddlExperience.Items.Insert(25, "25")
            ddlExperience.Items.Insert(26, "26")
            ddlExperience.Items.Insert(27, "27")
            ddlExperience.Items.Insert(28, "28")
            ddlExperience.Items.Insert(29, "29")
            ddlExperience.Items.Insert(30, "30")
            ddlExperience.Items.Insert(31, "31")
            ddlExperience.Items.Insert(32, "32")
            ddlExperience.Items.Insert(33, "33")
            ddlExperience.Items.Insert(34, "34")
            ddlExperience.Items.Insert(35, "35")
            ddlExperience.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindQualification()
        Try
            cblQualification.Items.Clear()
            cblQualification.Items.Add(New ListItem("Bachelor Degree", "1"))
            cblQualification.Items.Add(New ListItem("Master Degree", "2"))
            cblQualification.Items.Add(New ListItem("PG", "3"))
            cblQualification.Items.Add(New ListItem("Chartered Accountant", "4"))
            cblQualification.Items.Add(New ListItem("CIA Part1", "5"))
            cblQualification.Items.Add(New ListItem("CIA Part2", "6"))
            cblQualification.Items.Add(New ListItem("CIA Part3", "7"))
            cblQualification.Items.Add(New ListItem("ICWA", "8"))
            cblQualification.Items.Add(New ListItem("CISA", "9"))
            cblQualification.Items.Add(New ListItem("CISSP", "10"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetSubMenuOpen()
        Try
            lnkbtnInward.Attributes.Remove("class") : lnkbtnOutward.Attributes.Remove("class") : lnkbtnGmail.Attributes.Remove("class") : lnkbtnBRRPlanning.Attributes.Remove("class")
            liScan.Attributes.Remove("class")
            'liFolder.Attributes.Remove("class") : liDescriptors.Attributes.Remove("class") : liDocType.Attributes.Remove("class")
            'liCabinet.Attributes.Remove("class") : liSubCabinet.Attributes.Remove("class")

            lnkbtnInward.Font.Italic = False : lnkbtnInward.Font.Bold = False
            lnkbtnOutward.Font.Italic = False : lnkbtnOutward.Font.Bold = False
            lnkbtnGmail.Font.Italic = False : lnkbtnGmail.Font.Bold = False
            lnkbtnBRRPlanning.Font.Italic = False : lnkbtnBRRPlanning.Font.Bold = False
            lnkbtnCabinet.Font.Italic = False : lnkbtnCabinet.Font.Bold = False
            lnkbtnSubCabinet.Font.Italic = False : lnkbtnSubCabinet.Font.Bold = False
            lnkbtnFolder.Font.Italic = False : lnkbtnFolder.Font.Bold = False
            lnkbtnDescriptor.Font.Italic = False : lnkbtnDescriptor.Font.Bold = False
            lnkbtnDocType.Font.Italic = False : lnkbtnDocType.Font.Bold = False
            lnkIndexing.Font.Italic = False : lnkIndexing.Font.Bold = False
            lnkSearch.Font.Italic = False : lnkSearch.Font.Bold = False
            lnkbtnNormalScan.Font.Italic = False : lnkbtnNormalScan.Font.Bold = False

            lnkbtnAssignmentsDashboard.Font.Italic = False : lnkbtnAssignmentsDashboard.Font.Bold = False
            lnkbtnScheduleAssignments.Font.Italic = False : lnkbtnScheduleAssignments.Font.Bold = False
            lnkbtnTaskAssignments.Font.Italic = False : lnkbtnTaskAssignments.Font.Bold = False
            lnkbtnDynamicReports.Font.Italic = False : lnkbtnDynamicReports.Font.Bold = False
            lnkbtnComplianceTask.Font.Italic = False : lnkbtnComplianceTask.Font.Bold = False
            lnkbtnInvoice.Font.Italic = False : lnkbtnInvoice.Font.Bold = False

            liAssignmentMain.Attributes.Remove("class")

            liMasters.Attributes.Remove("class") : liAuxilaryReport.Attributes.Remove("class")
            liReport.Attributes.Remove("class") : liUPT.Attributes.Remove("class")
            liJE.Attributes.Remove("class")

            lnkbtnJournalEntry.Font.Italic = False : lnkbtnJournalEntry.Font.Bold = False
            lnkbtnAuxilaryReport.Font.Italic = False : lnkbtnAuxilaryReport.Font.Bold = False
            lnkbtnReport.Font.Italic = False : lnkbtnReport.Font.Bold = False
            If sSession.SubMenu = "OfficeCorrespondence" Then
                liOC.Attributes.Add("class", "open")
                If sSession.Form = "Inward" Then
                    lnkbtnInward.Font.Italic = True : lnkbtnInward.Font.Bold = True
                ElseIf sSession.Form = "Outward" Then
                    lnkbtnOutward.Font.Italic = True : lnkbtnOutward.Font.Bold = True
                ElseIf sSession.Form = "Gmail" Then
                    lnkbtnGmail.Font.Italic = True : lnkbtnGmail.Font.Bold = True
                ElseIf sSession.Form = "InwardTracker" Then
                    lnkbtnBRRPlanning.Font.Italic = True : lnkbtnBRRPlanning.Font.Bold = True
                End If

            ElseIf sSession.SubMenu = "AuditAssignments" Then
                liAssignmentMain.Attributes.Add("class", "open")
                If sSession.Form = "AssignmentsDashboard" Then
                    lnkbtnAssignmentsDashboard.Font.Italic = True : lnkbtnAssignmentsDashboard.Font.Bold = True
                ElseIf sSession.Form = "ScheduleAssignments" Then
                    lnkbtnScheduleAssignments.Font.Italic = True : lnkbtnScheduleAssignments.Font.Bold = True
                ElseIf sSession.Form = "TaskAssignments" Then
                    lnkbtnTaskAssignments.Font.Italic = True : lnkbtnTaskAssignments.Font.Bold = True
                ElseIf sSession.Form = "DynamicReports" Then
                    lnkbtnDynamicReports.Font.Italic = True : lnkbtnDynamicReports.Font.Bold = True
                ElseIf sSession.Form = "ComplianceTask" Then
                    lnkbtnComplianceTask.Font.Italic = True : lnkbtnComplianceTask.Font.Bold = True
                ElseIf sSession.Form = "Invoice" Then
                    lnkbtnInvoice.Font.Italic = True : lnkbtnInvoice.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Digital_AuditOffice" Then
                liDF.Attributes.Add("class", "open")
                If sSession.Form = "Cabinet" Then
                    lnkbtnCabinet.Font.Italic = True : lnkbtnCabinet.Font.Bold = True
                ElseIf sSession.Form = "SubCabinet" Then
                    lnkbtnSubCabinet.Font.Italic = True : lnkbtnSubCabinet.Font.Bold = True
                ElseIf sSession.Form = "Folders" Then
                    lnkbtnFolder.Font.Italic = True : lnkbtnFolder.Font.Bold = True

                ElseIf sSession.Form = "Descriptors" Then
                    lnkbtnDescriptor.Font.Italic = True : lnkbtnDescriptor.Font.Bold = True
                ElseIf sSession.Form = "DocType" Then
                    lnkbtnDocType.Font.Italic = True : lnkbtnDocType.Font.Bold = True
                ElseIf sSession.Form = "Index" Then
                    lnkIndexing.Font.Italic = True : lnkIndexing.Font.Bold = True
                ElseIf sSession.Form = "Search" Then
                    lnkSearch.Font.Italic = True : lnkSearch.Font.Bold = True
                End If
                '    liCabinet.Attributes.Add("class", "open")
                '    If sSession.Form = "Cabinet" Then
                '        lnkbtnCabinet.Font.Italic = True : lnkbtnCabinet.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "SubCabinet" Then
                '    If sSession.Form = "SubCabinet" Then
                '        liSubCabinet.Attributes.Add("class", "open")
                '        lnkbtnSubCabinet.Font.Italic = True : lnkbtnSubCabinet.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "Folders" Then
                '    If sSession.Form = "Folders" Then
                '        liFolder.Attributes.Add("class", "open")
                '        lnkbtnFolder.Font.Italic = True : lnkbtnFolder.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "Descriptors" Then
                '    If sSession.Form = "Descriptors" Then
                '        liDescriptors.Attributes.Add("class", "open")
                '        lnkbtnDescriptor.Font.Italic = True : lnkbtnDescriptor.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "DocType" Then
                '    If sSession.Form = "DocType" Then
                '        liDocType.Attributes.Add("class", "open")
                '        lnkbtnDocType.Font.Italic = True : lnkbtnDocType.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "Index" Then
                '    If sSession.Form = "Index" Then
                '        liIndex.Attributes.Add("class", "open")
                '        lnkIndexing.Font.Italic = True : lnkIndexing.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "Search" Then
                '    If sSession.Form = "Search" Then
                '        liSearch.Attributes.Add("class", "open")
                '        lnkSearch.Font.Italic = True : lnkSearch.Font.Bold = True
                '    End If
            ElseIf sSession.SubMenu = "Scan" Then
                liScan.Attributes.Add("class", "open")
                If sSession.Form = "NormalScan" Then
                    lnkbtnNormalScan.Font.Italic = True : lnkbtnNormalScan.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "ADO" Then
                liScan.Attributes.Add("class", "open")
                If sSession.Form = "ADODashboard" Then
                    lnkbtnADODashBroard.Font.Italic = True : lnkbtnADODashBroard.Font.Bold = True
                End If
                '''''''''''''''''''''''''
            ElseIf sSession.SubMenu = "DepreciationComputation" Then
                FXASTDepComp.Attributes.Add("class", "open")
                If sSession.Form = "DepreciationComputation" Then
                    lnkbtnDepComp.Font.Italic = True : lnkbtnDepComp.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetTransaction" Then
                FXASTTrns.Attributes.Add("class", "open")
                If sSession.Form = "AssetTransactionAddition" Then
                    lnkbtnAssetTransactionadd.Font.Italic = True : lnkbtnAssetTransactionadd.Font.Bold = True
                ElseIf sSession.Form = "AssetDeletion" Then
                    lnkbtnAssetTransactionDel.Font.Italic = True : lnkbtnAssetTransactionDel.Font.Bold = True
                ElseIf sSession.Form = "AssetAdditionalDetails" Then
                    'lnkbtnAssetAddlnDtls.Font.Italic = True : lnkbtnAssetAddlnDtls.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetExcelUplaod" Then
                FXAOPExcel.Attributes.Add("class", "open")
                If sSession.Form = "AssetMaster" Then
                    lnkbtnAssetAddition.Font.Italic = True : lnkbtnAssetAddition.Font.Bold = True
                ElseIf sSession.Form = "AssetAddition" Then
                    lnkbtnAssetMaster.Font.Italic = True : lnkbtnAssetMaster.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetOPeningBalExcelView" Then
                FXAOPExcelView.Attributes.Add("class", "open")
                If sSession.Form = "AssetOPeningBalExcelUplaod" Then
                    lnkbtnFXOPExcelview.Font.Italic = True : lnkbtnFXOPExcelview.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Planning" Then
                'liAudit.Attributes.Add("class", "open")
                'If sSession.Form = "Planning" Then
                '    lnkbtnPlanning.Font.Italic = True : lnkbtnPlanning.Font.Bold = True
                'End If
            ElseIf sSession.SubMenu = "Report" Then
                'lnkFAXDReport.Attributes.Add("class", "open")
                'If sSession.Form = "DynamicReport" Then
                '    lnkbtnDynamicReport.Font.Italic = True : lnkbtnDynamicReport.Font.Bold = True
                If sSession.Form = "PhysicalReport" Then
                    lnkbtnPhysicalReport.Font.Italic = True : lnkbtnPhysicalReport.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "Inward" Then
                sSession.SubMenu = "OfficeCorrespondence" : sSession.Form = "Inward"
                Response.Redirect("~/Digital_AuditOffice/InwardDashboard.aspx", False)
            ElseIf sForm = "Outward" Then
                sSession.SubMenu = "OfficeCorrespondence" : sSession.Form = "Outward"
                Response.Redirect("~/Digital_AuditOffice/OutwardDashboard.aspx", False)
            ElseIf sForm = "Gmail" Then
                sSession.SubMenu = "OfficeCorrespondence" : sSession.Form = "Gmail"
                Response.Redirect("~/Digital_AuditOffice/Gmail.aspx", False)
            ElseIf sForm = "InwardTracker" Then
                sSession.SubMenu = "OfficeCorrespondence" : sSession.Form = "InwardTracker"
                Response.Redirect("~/Digital_AuditOffice/InwardTracker.aspx", False)
            ElseIf sForm = "Cabinet" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "Cabinet"
                Response.Redirect("~/Digital_AuditOffice/Cabinet.aspx", False)
            ElseIf sForm = "SubCabinet" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "SubCabinet"
                Response.Redirect("~/Digital_AuditOffice/SubCabinet.aspx", False)
            ElseIf sForm = "Folders" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "Folders"
                Response.Redirect("~/Digital_AuditOffice/Folders.aspx", False)
            ElseIf sForm = "Descriptors" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "Descriptors"
                Response.Redirect("~/Digital_AuditOffice/Descriptor.aspx", False)
            ElseIf sForm = "DocType" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "DocType"
                Response.Redirect("~/Digital_AuditOffice/DocumentType.aspx", False)
            ElseIf sForm = "Index" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "Index"
                Response.Redirect("~/Digital_AuditOffice/Indexing.aspx", False)
            ElseIf sForm = "Search" Then
                sSession.SubMenu = "Digital_AuditOffice" : sSession.Form = "Search"
                Response.Redirect("~/Digital_AuditOffice/Search.aspx", False)
            ElseIf sForm = "NormalScan" Then
                sSession.SubMenu = "Scan" : sSession.Form = "NormalScan"
                Response.Redirect("~/Digital_AuditOffice/NormalScan.aspx", False)
            ElseIf sForm = "Planning" Then
                sSession.SubMenu = "Planning" : sSession.Form = "Planning"
                Response.Redirect("~/Digital_AuditOffice/FinancialAddAssign.aspx", False)
            ElseIf sForm = "ADODashboard" Then
                sSession.SubMenu = "ADO"
                Response.Redirect("~/Digital_AuditOffice/ADO_Dashboard.aspx", False)
                ''''''''''''''''''''''''''''''''''''
            ElseIf sForm = "AssetMasterPage" Then
                sSession.SubMenu = "AssetMasterPage" : sSession.Form = "AssetMasterPage"
                Response.Redirect("~/FixedAsset/AssetMasterPage.aspx", False)
            ElseIf sForm = "LocationSetup" Then
                sSession.SubMenu = "LocationSetup" : sSession.Form = "LocationSetup"
                Response.Redirect("~/FixedAsset/LocationSetUp.aspx", False)
            ElseIf sForm = "AssetTransactionAddition" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetAdditionDashBoard.aspx", False)
            ElseIf sForm = "AssetDeletion" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetDltnDashboard"
                Response.Redirect("~/FixedAsset/AssetDeletionDashboard.aspx", False)
            ElseIf sForm = "AssetDeletion" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetDeletion.aspx", False)
            ElseIf sForm = "AssetTransactionFileUpload" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/FXDAsstFileUpload.aspx", False)
            ElseIf sForm = "AssetTranFileUploadView" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetFileUploadView.aspx", False)
            ElseIf sForm = "AssetAdditionalDetails" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetAddlnDtls.aspx", False)

            ElseIf sForm = "AssignmentsDashboard" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "AssignmentsDashboard"
                Response.Redirect("~/Assignment/AssignmentsDashboard.aspx", False)
            ElseIf sForm = "ScheduleAssignments" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "ScheduleAssignments"
                Response.Redirect("~/Assignment/ScheduleAssignments.aspx", False)
            ElseIf sForm = "TaskAssignments" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "TaskAssignments"
                Response.Redirect("~/Assignment/EmpAssignmentSubTask.aspx", False)
            ElseIf sForm = "DynamicReports" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "DynamicReports"
                Response.Redirect("~/Assignment/DynamicReports.aspx", False)
            ElseIf sForm = "ComplianceTask" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "ComplianceTask"
                Response.Redirect("~/Assignment/ComplianceAsgTask.aspx", False)
            ElseIf sForm = "Invoice" Then
                sSession.SubMenu = "AuditAssignments" : sSession.Form = "Invoice"
                Response.Redirect("~/Assignment/Invoice.aspx", False)

                'ElseIf sForm = "ChartofAccounts" Then
                '    sSession.SubMenu = "ChartofAccounts" : sSession.Form = "ChartofAccounts"
                '    Response.Redirect("~/Digital_AuditOffice/ChartofAccounts.aspx", False)
            ElseIf sForm = "Schedules" Then
                sSession.SubMenu = "Schedules" : sSession.Form = "Schedules"
                Response.Redirect("~/Digital_AuditOffice/Schedules.aspx", False)
            ElseIf sForm = "Reports" Then
                sSession.SubMenu = "Reports" : sSession.Form = "Reports"
                Response.Redirect("~/Digital_AuditOffice/ScheduleReport.aspx", False)

            ElseIf sForm = "UploadTrailBalSchedule" Then
                sSession.SubMenu = "UploadTrailBalSchedule" : sSession.Form = "UploadTrailBalSchedule"
                Response.Redirect("~/Digital_AuditOffice/UploadTrailbalanceSchedule.aspx", False)
            ElseIf sForm = "JournalEntry" Then
                sSession.SubMenu = "JournalEntry" : sSession.Form = "JournalEntry"
                Response.Redirect("~/Digital_AuditOffice/JournalEntry.aspx", False)
            ElseIf sForm = "AuxilaryReport" Then
                sSession.SubMenu = "AuxilaryReport" : sSession.Form = "AuxilaryReport"
                Response.Redirect("~/Digital_AuditOffice/AuxilaryReport.aspx", False)
            ElseIf sForm = "Planning" Then
                sSession.SubMenu = "Planning" : sSession.Form = "Planning"
                Response.Redirect("~/Digital_AuditOffice/FinancialAddAssign.aspx", False)
            ElseIf sForm = "Report" Then
                sSession.SubMenu = "Report" : sSession.Form = "Report"
                Response.Redirect("~/Digital_AuditOffice/Report.aspx", False)
            ElseIf sForm = "Datacapture" Then
                sSession.SubMenu = "Datacapture" : sSession.Form = "Datacapture"
                Response.Redirect("~/Digital_AuditOffice/DataCapture.aspx", False)
            ElseIf sForm = "NextYearOpeningBalance" Then
                sSession.SubMenu = "NextYearOpeningBalance" : sSession.Form = "NextYearOpeningBalance"
                Response.Redirect("~/Digital_AuditOffice/NextYearOpeningBalance.aspx", False)
            ElseIf sForm = "Digital_AuditOfficeDashboard" Then
                sSession.SubMenu = "Digital_AuditOfficeDashboard" : sSession.Form = "Digital_AuditOfficeDashboard"
                Response.Redirect("~/Digital_AuditOffice/DigitalFilingDashboard.aspx", False)
                'ElseIf sForm = "ClosingStocks" Then
                '    sSession.SubMenu = "ClosingStocks" : sSession.Form = "ClosingStocks"
                '    Response.Redirect("~/Digital_AuditOffice/ClosingStock.aspx", False)
            End If

            If sForm = "AssetExcelUpload" Then
                sSession.SubMenu = "AssetMaster" : sSession.Form = "AssetMaster"
                Response.Redirect("~/FixedAsset/AssetOpeningBalExcelUpload.aspx", False)
            ElseIf sForm = "ExcelAssetAddition" Then
                sSession.SubMenu = "AssetAddition" : sSession.Form = "AssetAddition"
                Response.Redirect("~/FixedAsset/AssetAdditionExcelUpload.aspx", False)
            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnkbtnAssignmentsDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAssignmentsDashboard.Click
        Try
            GetClickedURL("AssignmentsDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssignmentsDashboard_Click")
        End Try
    End Sub
    Protected Sub lnkbtnScheduleAssignments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnScheduleAssignments.Click
        Try
            GetClickedURL("ScheduleAssignments")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleAssignments_Click")
        End Try
    End Sub
    Protected Sub lnkbtnTaskAssignments_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnTaskAssignments.Click
        Try
            GetClickedURL("TaskAssignments")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnTaskAssignments_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDynamicReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDynamicReports.Click
        Try
            GetClickedURL("DynamicReports")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamicReports_Click")
        End Try
    End Sub
    Protected Sub lnkbtnComplianceTask_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnComplianceTask.Click
        Try
            GetClickedURL("ComplianceTask")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnComplianceTask_Click")
        End Try
    End Sub
    Protected Sub lnkbtnInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInvoice.Click
        Try
            GetClickedURL("Invoice")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInvoice_Click")
        End Try
    End Sub
    Protected Sub lnkbtnADODashBroard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnADODashBroard.Click
        Try
            GetClickedURL("ADODashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnADODashBroard" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnNormalScan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNormalScan.Click
        Try
            GetClickedURL("NormalScan")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnNormalScan" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkIndexing_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkIndexing.Click
        Try
            GetClickedURL("Index")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkIndexing_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearch.Click
        Try
            GetClickedURL("Search")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkSearch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnInward_Click(sender As Object, e As EventArgs) Handles lnkbtnInward.Click
        Try
            GetClickedURL("Inward")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInward_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnOutward_Click(sender As Object, e As EventArgs) Handles lnkbtnOutward.Click
        Try
            GetClickedURL("Outward")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOutward_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnGmail_Click(sender As Object, e As EventArgs) Handles lnkbtnGmail.Click
        Try
            GetClickedURL("Gmail")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGmail_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnBRRPlanning_Click(sender As Object, e As EventArgs) Handles lnkbtnBRRPlanning.Click
        Try
            GetClickedURL("InwardTracker")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRPlanning_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnCabinet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCabinet.Click
        Try
            GetClickedURL("Cabinet")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCabinet_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnSubCabinet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSubCabinet.Click
        Try
            GetClickedURL("SubCabinet")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSubCabinet_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnFolder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFolder.Click
        Try
            GetClickedURL("Folders")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFolder_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnDescriptor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDescriptor.Click
        Try
            GetClickedURL("Descriptors")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDescriptor_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnDocType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDocType.Click
        Try
            GetClickedURL("DocType")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDocType_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCheckPwd_Click(sender As Object, e As EventArgs)
        Dim bFlag As Boolean
        Try
            bFlag = objclsCPFP.CheckUserPWD(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtCheckPassword.Text))
            If bFlag = True Then
                BindExperience() : BindQualification() : LoadUserProfile()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('show');$('#ModalPassword').modal('hide');", True)
            Else
                lblValidationMsg.Text = "Invalid Passsword."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckPwd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCheckCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCheckPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCPCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCurrentPasssword.Text = "" : txtNewPassword.Text = "" : txtConfirmPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        Dim iMinPassword As Integer, iMaxPassword As Integer
        Try
            lblCPError.Text = "" : lblUPError.Text = ""
            If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
                If (objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
                    txtCurrentPasssword.Focus()
                    lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
                    Exit Try
                End If

                iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

                If iMinPassword > txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password must have at least " & iMinPassword & " characters." : lblCPError.Text = "Password must have at least " & iMinPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If iMaxPassword < txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password is less than " & iMaxPassword & " characters." : lblCPError.Text = "Password is less than " & iMaxPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Enter New Password, different than your previous 5 passwords." : lblCPError.Text = "Enter New Password, different than your previous 5 passwords."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
                objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
                objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
                objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text))
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Change Password", "Password Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
                lblValidationMsg.Text = "Password Successfully Changed." : lblCPError.Text = "Password Successfully Changed."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            Else
                txtCurrentPasssword.Focus()
                lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            End If
        Catch ex As Exception
            lblCPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateChagePwd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Public Sub LoadUserProfile()
        Dim sArray As Array
        Dim j As Integer
        Try
            objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
            txtSAPcode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Code)
            txtEmpName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_fullName)
            txtMail.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Email)
            If objUser.sUsr_MobileNo = "&nbsp;" Then
                txtMobNo.Text = ""
            Else
                txtMobNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_MobileNo)
            End If

            txtDesignation.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Designation)
            If objUser.sUsr_GrpOrUserLvlPerm = 0 Then
                txtPermission.Text = "Role based"
            Else
                txtPermission.Text = "User based"
            End If
            txtRole.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LevelGrp)
            txtSecurityQuestion.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_SecurityQuestion)

            If objUser.sUsr_Answer <> "" Then
                txtAnswer.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(objUser.sUsr_Answer))
            End If
            txtExperiencesummary.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_SkillSet)
            ddlExperience.SelectedIndex = objUser.iUsr_Experience
            txtOthers.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Others)

            If objUser.sUsr_Qualification.Contains(",") = True Then
                sArray = objUser.sUsr_Qualification.Split(",")
                For i = 0 To sArray.Length - 1
                    If sArray(i) <> "" Then
                        For j = 0 To cblQualification.Items.Count - 1
                            If cblQualification.Items(j).Value = sArray(i) Then
                                cblQualification.Items(j).Selected = True
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub btnUpdateUserProfile_Click(sender As Object, e As EventArgs)
        Dim sQual As String = "", sSecurityAnswer As String
        Try
            lblCPError.Text = "" : lblUPError.Text = ""
            If txtMobNo.Text.Trim <> "" Then
                If txtMobNo.Text.Trim.Length > 10 Then
                    txtMobNo.Focus()
                    lblValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers)." : lblUPError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If

                If txtMobNo.Text.Trim.Length <> 10 Then
                    txtMobNo.Focus()
                    lblValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblUPError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtMail.Text.Trim = "" Then
                txtMail.Focus()
                lblValidationMsg.Text = "Enter E-Mail." : lblUPError.Text = "Enter E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtSecurityQuestion.Text.Trim = "" Then
                txtSecurityQuestion.Focus()
                lblValidationMsg.Text = "Enter Security Question." : lblUPError.Text = "Enter Security Question."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtSecurityQuestion.Text.Trim.Length > 250 Then
                txtSecurityQuestion.Focus()
                lblValidationMsg.Text = "Security Question exceeded maximum size(max 250 characters)." : lblUPError.Text = "Security Question exceeded maximum size(max 250 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAnswer.Text.Trim = "" Then
                txtAnswer.Focus()
                lblValidationMsg.Text = "Enter Answer." : lblUPError.Text = "Enter Answer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAnswer.Text.Trim.Length > 250 Then
                txtAnswer.Focus()
                lblValidationMsg.Text = "Answer exceeded maximum size(max 250 characters)." : lblUPError.Text = "Answer exceeded maximum size(max 250 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtExperiencesummary.Text.Trim.Length > 8000 Then
                txtExperiencesummary.Focus()
                lblValidationMsg.Text = "Experience Summary exceeded maximum size(max 8000 characters)." : lblUPError.Text = "Experience Summary exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
            sSecurityAnswer = objclsGRACeGeneral.EncryptPassword(Trim(txtAnswer.Text))
            For i = 0 To cblQualification.Items.Count - 1
                If cblQualification.Items(i).Selected = True Then
                    sQual = sQual & "," & cblQualification.Items(i).Value
                End If
            Next

            If txtOthers.Text.Trim.Length > 5000 Then
                lblValidationMsg.Text = "Others Details exceeded maximum size(max 5000 characters)." : lblUPError.Text = "Others Details exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                txtOthers.Focus()
                Exit Sub
            End If
            objclsCPFP.UpdateUserProfile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsGRACeGeneral.SafeSQL(txtMobNo.Text), objclsGRACeGeneral.SafeSQL(txtExperiencesummary.Text), ddlExperience.SelectedIndex, sQual, objclsGRACeGeneral.SafeSQL(txtOthers.Text), objclsGRACeGeneral.SafeSQL(txtSecurityQuestion.Text), sSecurityAnswer, objclsGRACeGeneral.SafeSQL(txtMail.Text), sSession.IPAddress)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "User Profile", "Profile Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
            lblValidationMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblUPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateUserProfile_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnHOME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHOME.Click
        Try
            sSession.Menu = "HOME" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHOME_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnMASTERS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnMASTERS.Click
        Try
            sSession.Menu = "MASTER" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Master.aspx", False) 'HomePages/Master
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMASTERS_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnAUDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAUDIT.Click
        Try
            sSession.Menu = "AUDIT" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Audit.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAUDIT_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnASSIGNMENTS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnASSIGNMENTS.Click
        Try
            sSession.Menu = "ASSIGNMENTS" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Assignment/Dashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnASSIGNMENTS_Click")
        End Try
    End Sub
    Protected Sub lnkbtnRISK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRISK.Click
        Try
            sSession.Menu = "RISK" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Risk.aspx", False) 'HomePages/Risk
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRISK_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnDigital_AuditOffice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDigital_AuditOffice.Click
        Try
            sSession.Menu = "Digital_AuditOffice" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Digital_AuditOfficeHome.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigital_AuditOffice_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnEmailManagement_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnEmailManagement.Click
        Try
            sSession.Menu = "EMailManagement" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/EMailManagement.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmailManagement_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnSearch_Click(sender As Object, e As EventArgs) Handles lnkbtnSearch.Click
        Try
            sSession.Menu = "Search" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Search/Search.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSearch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnAccounts_Click(sender As Object, e As EventArgs) Handles lnkbtnAccounts.Click
        Try
            sSession.Menu = "Accounts" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Accounts.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAccounts_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnCOMPLIANCE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCOMPLIANCE.Click
        Try
            sSession.Menu = "COMPLIANCE" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Complaince.aspx", False) 'HomePages/Complaince
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCOMPLIANCE_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtncorporateAudit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtncorporateAudit.Click
        Try
            sSession.Menu = "CorporateAudit" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/CorporateAudit.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtncorporateAudit_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click
        Try
            If (sSession.UserID) <> 0 Then
                '  objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-60)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogout_Click")
        End Try
    End Sub
    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (sSession.UserID) <> 0 Then
                ' objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-60)
            End If

            If IsNothing(Request.Cookies("AuthToken")) = False Then
                Response.Cookies("AuthToken").Value = String.Empty
                Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-60)
            End If

            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub
    Private Sub lnkbtnAstReg_Click(sender As Object, e As EventArgs) Handles lnkbtnAstReg.Click
        Try
            sSession.Menu = "AssetRegister" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetRegister.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAstReg_Click")
        End Try
    End Sub
    Private Sub lnkbtnDepComp_Click(sender As Object, e As EventArgs) Handles lnkbtnDepComp.Click
        Try
            sSession.Menu = "DepreciationComputation" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/DepreciationComputation.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDepComp_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetTransactionadd_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetTransactionadd.Click
        Try
            GetClickedURL("AssetTransactionAddition")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetTransactionadd_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetTransactionDel_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetTransactionDel.Click
        Try
            GetClickedURL("AssetDeletion")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetTransactionDel_Click")
        End Try
    End Sub

    Private Sub lnkbtnFXOPExcelview_Click(sender As Object, e As EventArgs) Handles lnkbtnFXOPExcelview.Click
        Try
            sSession.Menu = "AssetOPeningBalExcelView" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/PhysicalRPTVerification.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXOPExcelview_Click")
        End Try
    End Sub
    'Private Sub lnkbtnDynamicReport_Click(sender As Object, e As EventArgs) Handles lnkbtnDynamicReport.Click
    '    Try
    '        sSession.Menu = "DynamicReport" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/FixedAsset/FXADynamicReport.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamicReport_Click")
    '    End Try
    'End Sub

    'Private Sub lnkbtnAssetAddlnDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetAddlnDtls.Click
    '    Try
    '        GetClickedURL("AssetAdditionalDetails")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetAddlnDtls_Click")
    '    End Try
    'End Sub
    Private Sub lnkbtnPhysicalReport_Click(sender As Object, e As EventArgs) Handles lnkbtnPhysicalReport.Click
        Try
            sSession.Menu = "PhysicalReport" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXAPhysicalReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPhysicalReport_Click")
        End Try
    End Sub
    Private Sub lnkbtnDynamicReport_Click(sender As Object, e As EventArgs) Handles lnkbtnDynamicReport.Click
        Try
            sSession.Menu = "DynamicReport" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXADynamicReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamicReport_Click")
        End Try
    End Sub
    Private Sub lnkbtnIndReport_Click(sender As Object, e As EventArgs) Handles lnkbtnIndReport.Click
        Try
            sSession.Menu = "IndReport" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXAInvReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnIndReport_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetMaster.Click
        Try
            GetClickedURL("AssetExcelUpload")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkbtnAssetAddition_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetAddition.Click
        Try
            GetClickedURL("ExcelAssetAddition")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkbtnJournalEntry_Click(sender As Object, e As EventArgs) Handles lnkbtnJournalEntry.Click
        Try
            GetClickedURL("JournalEntry")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnJournalEntry_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnAuxilaryReport_Click(sender As Object, e As EventArgs) Handles lnkbtnAuxilaryReport.Click
        Try
            GetClickedURL("AuxilaryReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAuxilaryReport_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnReport_Click(sender As Object, e As EventArgs) Handles lnkbtnReport.Click
        Try
            GetClickedURL("Report")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReport_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnSchedules_Click(sender As Object, e As EventArgs) Handles lnkbtnSchedules.Click
        Try
            GetClickedURL("Schedules")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleLinkage_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub

    Private Sub lnkbtnReports_Click(sender As Object, e As EventArgs) Handles lnkbtnReports.Click
        Try
            GetClickedURL("Reports")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleLinkage_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Private Sub lnkbtnTrailBalanceSchedule_Click(sender As Object, e As EventArgs) Handles lnkbtnTrailBalanceSchedule.Click
        Try
            GetClickedURL("UploadTrailBalSchedule")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleLinkage_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub

    Private Sub lnkbtnAssetMasterPage_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetMasterPage.Click
        Try
            GetClickedURL("AssetMasterPage")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleLinkage_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub

    Private Sub lnkbtnDigitalFillinhDashboard_Click(sender As Object, e As EventArgs) Handles lnkbtnDigitalFillinhDashboard.Click
        Try
            GetClickedURL("Digital_AuditOfficeDashboard")
        Catch ex As Exception

        End Try
    End Sub



    'Private Sub lnkbtnPlanning_Click(sender As Object, e As EventArgs) Handles lnkbtnPlanning.Click
    '    Try
    '        GetClickedURL("Planning")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReport_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
    '    End Try
    'End Sub
    Private Sub lnkbtnLocationSetup_Click(sender As Object, e As EventArgs) Handles lnkbtnLocationSetup.Click
        Try
            GetClickedURL("LocationSetup")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLocationSetup_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub

End Class


