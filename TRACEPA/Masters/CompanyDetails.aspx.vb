Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Partial Class CompanyDetails
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Masters_CompanyDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsControlLibrary As New clsControlLibrary
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsCompanyDetails As New clsCompanyDetails
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnAddBranch.ImageUrl = "~/Images/Add24.png"
        imgbtnSaveUpdateBranch.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                imgbtnAddBranch.Visible = False : imgbtnSaveUpdateBranch.Visible = False
                BindExistingCompanyName()
                ClientSideValidationCompanyDetails()
                liCompanyDetails.Attributes.Add("class", "active") : divCompanyDetails.Attributes.Add("class", "tab-pane active")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub lnkbtnCompanyDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnCompanyDetails.Click
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            imgbtnAddBranch.Visible = False : imgbtnSaveUpdateBranch.Visible = False

            liCompanyDetails.Attributes.Add("class", "active") : divCompanyDetails.Attributes.Add("class", "tab-pane active")
            liBranchDetails.Attributes.Remove("class") : divBranchDetails.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCompanyDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnBranchDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnBranchDetails.Click
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnAddBranch.Visible = True : imgbtnSaveUpdateBranch.Visible = True

            liCompanyDetails.Attributes.Remove("class") : divCompanyDetails.Attributes.Add("class", "tab-pane")
            liBranchDetails.Attributes.Add("class", "active") : divBranchDetails.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBranchDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub ClientSideValidationCompanyDetails()
        Try
            RFVCompanyCode.ControlToValidate = "txtCompanyCode" : RFVCompanyCode.ErrorMessage = "Enter Firm’s reg no."
            REVCompanyCode.ValidationExpression = "^[\s\S]{0,25}$" : REVCompanyCode.ErrorMessage = "Firm’s reg no exceeded maximum size(max 25 character)."

            RFVCompanyName.ControlToValidate = "txtCompanyName" : RFVCompanyName.ErrorMessage = "Enter Name."
            REVCompanyName.ValidationExpression = "^[\s\S]{0,500}$" : REVCompanyName.ErrorMessage = " Name exceeded maximum size(max 500 character)."

            RFVCompanyAddress.ControlToValidate = "txtCompanyAddress" : RFVCompanyAddress.ErrorMessage = "Enter Head Office Address."
            REVCompanyAddress.ValidationExpression = "^[\s\S]{0,1000}$" : REVCompanyAddress.ErrorMessage = "Head Office Address exceeded maximum size(max 1000 character)."

            RFVCompanyCity.ControlToValidate = "txtCompanyCity" : RFVCompanyCity.ErrorMessage = "Enter City."
            REVCompanyCity.ValidationExpression = "^[\s\S]{0,500}$" : REVCompanyCity.ErrorMessage = "City exceeded maximum size(max 500 character)."

            RFVState.ControlToValidate = "txtCompanyState" : RFVState.ErrorMessage = "Enter State."
            REVState.ValidationExpression = "^[\s\S]{0,500}$" : REVState.ErrorMessage = "State exceeded maximum size(max 500 character)."

            RFVCountry.ControlToValidate = "txtCompanyCountry" : RFVCountry.ErrorMessage = "Enter Country."
            REVCountry.ValidationExpression = "^[\s\S]{0,500}$" : REVCountry.ErrorMessage = "Country exceeded maximum size(max 500 character)."

            RFVCompanyPinCode.ControlToValidate = "txtCompanyPinCode" : RFVCompanyPinCode.ErrorMessage = "Enter PinCode."
            REVCompanyPinCode.ValidationExpression = "^[\s\S]{0,15}$" : REVCompanyPinCode.ErrorMessage = "PinCode exceeded maximum size(max 15 character)."

            RFVCompanyEmail.ControlToValidate = "txtCompanyEmail" : RFVCompanyEmail.ErrorMessage = "Enter E-Mail."
            REVCompanyEmail.ErrorMessage = "Enter valid E-Mail." : REVCompanyEmail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"

            RFVEstablishmentDate.ControlToValidate = "txtEstablishmentDate" : RFVEstablishmentDate.ErrorMessage = "Enter Establishment Date."

            RFVContactPerson.ControlToValidate = "txtContactPerson" : RFVContactPerson.ErrorMessage = "Enter Contact Person."
            REVContactPerson.ValidationExpression = "^[\s\S]{0,500}$" : REVContactPerson.ErrorMessage = "Contact Person exceeded maximum size(max 500 character)."

            RFVMoblieNo.ControlToValidate = "txtMoblieNo" : RFVMoblieNo.ErrorMessage = "Enter Moblie No."
            REVMoblieNo.ErrorMessage = "Enter valid 10 digit Mobile No." : REVMoblieNo.ValidationExpression = "^[0-9]{10}$"

            RFVContactMail.ControlToValidate = "txtContactMail" : RFVContactMail.ErrorMessage = "Enter Contact E-Mail."
            REVContactMail.ErrorMessage = "Enter valid Contact Mail E-Mail." : REVContactMail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"

            REVTelephoneNo.ErrorMessage = "Enter valid Telephone No." : REVTelephoneNo.ValidationExpression = "^[0-9]{0,15}$"

            RFVCompanyContactNo1.ControlToValidate = "txtCompanyContactNo1" : RFVCompanyContactNo1.ErrorMessage = "Enter Contact No 1."
            REVCompanyContactNo1.ErrorMessage = "Enter valid Contact No 1." : REVCompanyContactNo1.ValidationExpression = "^[0-9]{0,15}$"
            REVCompanyContactNo2.ErrorMessage = "Enter valid Contact No 2." : REVCompanyContactNo2.ValidationExpression = "^[0-9]{0,15}$"

            RFVExistingCompanyBranch.ErrorMessage = "Select Existing Company" : RFVExistingCompanyBranch.InitialValue = "Select Existing Company"
            RFVBranchName.ErrorMessage = "Enter Branch Office Name."
            RFVBranchContactPerson.ErrorMessage = "Enter Contact Person Name."
            RFVBranchAddress.ErrorMessage = "Enter Branch Office Address."
            REVBranchContactEmail.ErrorMessage = "Enter Valid E-mail." : REVBranchContactEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            REVBranchContactLandLineNo.ErrorMessage = "Enter Valid Telephone No." : REVBranchContactLandLineNo.ValidationExpression = "^[0-9]{0,15}$"
            REVBranchContactMobileNo.ErrorMessage = "Enter Valid 10 Digit Mobile No." : REVBranchContactMobileNo.ValidationExpression = "^[0-9]{10}$"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClientSideValidationCompanyDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Public Function SaveToBigDataPurchase(ByVal sPath As String) As Boolean
        Dim sExt As String = "", sFileName As String = "", ssql As String
        Dim iPosDot, iPosSlash As Integer
        Dim con As OleDb.OleDbConnection
        Dim com As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim ImgByte() As Byte
        Dim ImgLen As Integer
        Dim fs As IO.FileStream
        Dim iPSizeWiseReport As Integer
        Try
            If sPath <> "" Then
                iPosSlash = InStrRev(sPath, "\")
                iPosDot = InStrRev(sPath, ".")
                If iPosDot <> 0 Then
                    sFileName = Mid(sPath, iPosSlash + 1, iPosDot - (iPosSlash + 1))

                    sExt = Right(sPath, Len(sPath) - iPosDot)
                Else
                    If sPath = "" Then
                        Exit Function
                    End If
                    sFileName = Mid(sPath, iPosSlash, Len(sPath) - (iPosSlash + 1))
                    If sFileName = "" Then
                        Exit Function
                    End If
                    sExt = "unk"
                End If

                fs = New IO.FileStream(sPath, IO.FileMode.Open, IO.FileAccess.Read)
                ImgLen = fs.Length
                ReDim ImgByte(ImgLen)
                fs.Read(ImgByte, 0, System.Convert.ToInt32(fs.Length))
                fs.Close()
            End If

            Dim iMaxID As Integer
            Dim iExistingID As Integer
            ssql = "Select * from company_logo_settings where CLS_CompID =" & ddlExistingCompanyName.SelectedValue & ""
            dr = objDBL.SQLDataReader(sSession.AccessCode, ssql)
            If dr.HasRows = True Then
                'ssql = "" : ssql = "Delete from Print_Settings where PS_Status ='P'"
                'objDBL.SQLExecuteNonQuery(sSession.AccessCode, ssql)
                iExistingID = objDBL.SQLExecuteScalarInt(sSession.AccessCode, "Select CLS_ID From company_logo_settings Where CLS_CompID=" & ddlExistingCompanyName.SelectedValue & "")
                If sPath <> "" Then
                    ssql = "" : ssql = "Update company_logo_settings Set CLS_BIGDATA='?',CLS_SIZE=" & ImgLen & ",CLS_FileName='" & sFileName & "',CLS_Extn='" & sExt & "' Where CLS_ID=" & iExistingID & " And CLS_Status='A' And CLS_CompID=" & ddlExistingCompanyName.SelectedValue & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@CLS_BIGDATA", ImgByte)
                    com.Parameters.Add("@CLS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "Update company_logo_settings Set Where CLS_ID=" & iExistingID & " And CLS_Status='A' And CLS_CompID=" & ddlExistingCompanyName.SelectedValue & " "
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If
            Else
                iMaxID = objclsCompanyDetails.GetMaxID(sSession.AccessCode, ddlExistingCompanyName.SelectedValue, "company_logo_settings", "CLS_ID", "CLS_CompID")
                If sPath <> "" Then
                    ssql = "" : ssql = "Insert into company_logo_settings(CLS_ID,CLS_BIGDATA,CLS_SIZE,CLS_FileName,CLS_Extn,CLS_CompID,CLS_Status) values (1,?,?,'" & sFileName & "','" & sExt & "'," & ddlExistingCompanyName.SelectedValue & ", 'A')"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)

                    com.Parameters.Add("@CLS_BIGDATA", ImgByte)
                    com.Parameters.Add("@CLS_SIZE", ImgLen)
                Else
                    ssql = "" : ssql = "Insert into company_logo_settings(CLS_ID,CLS_Status,CLS_CompID) values (" & iMaxID & ",'A'," & iPSizeWiseReport & "," & ddlExistingCompanyName.SelectedValue & ")"
                    con = objDBL.SQLOpenDBConnection(sSession.AccessCode)
                    com = New OleDb.OleDbCommand(ssql, con)
                End If
            End If

            Dim myTrans As OleDb.OleDbTransaction  'Start a local transaction
            myTrans = con.BeginTransaction(IsolationLevel.ReadCommitted) 'Assign transaction object for a pending local transaction
            com.Connection = con
            com.Transaction = myTrans
            com.ExecuteNonQuery()
            myTrans.Commit()
            Return True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveToBigDataPurchase" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub BindExistingCompanyName()
        Try
            ddlExistingCompanyName.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingCompanyName.DataTextField = "Company_Name"
            ddlExistingCompanyName.DataValueField = "Company_ID"
            ddlExistingCompanyName.DataBind()
            ddlExistingCompanyName.Items.Insert(0, "Select Existing Company")

            ddlExistingCompanyBranch.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingCompanyBranch.DataTextField = "Company_Name"
            ddlExistingCompanyBranch.DataValueField = "Company_ID"
            ddlExistingCompanyBranch.DataBind()
            ddlExistingCompanyBranch.Items.Insert(0, "Select Existing Company")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingCompanyName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ddlExistingCompanyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingCompanyName.SelectedIndexChanged
        Try
            ClearAllCompanyDetails()
            If ddlExistingCompanyName.SelectedIndex > 0 Then
                BindCompanyDetails(ddlExistingCompanyName.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingCompanyName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Public Sub ClearAllCompanyDetails()
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            imgbtnAddBranch.Visible = False : imgbtnSaveUpdateBranch.Visible = False
            txtCompanyCode.Text = "" : txtCompanyName.Text = "" : txtCompanyAddress.Text = "" : txtCompanyCity.Text = ""
            txtCompanyState.Text = "" : txtCompanyCountry.Text = "" : txtCompanyPinCode.Text = "" : txtCompanyEmail.Text = ""
            txtEstablishmentDate.Text = "" : txtContactPerson.Text = "" : txtMoblieNo.Text = "" : txtContactMail.Text = ""
            txtTelephoneNo.Text = "" : txtWebSite.Text = "" : txtCompanyContactNo1.Text = "" : txtCompanyContactNo2.Text = ""
            txtAccountHolderName.Text = "" : txtAccountNo.Text = "" : txtBankName.Text = "" : txtBranch.Text = ""
            txtConditions.Text = "" : txtPaymentterms.Text = ""
            myLogoComp.ImageUrl = Nothing
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindCompanyDetails(ByVal iCompanyID As Integer)
        Dim dt As New DataTable
        Dim sFileName As String = ""
        Try
            ClearAllCompanyDetails()
            imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            imgbtnAddBranch.Visible = False : imgbtnSaveUpdateBranch.Visible = False
            dt = objclsCompanyDetails.GetCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, iCompanyID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0).Item("Company_Code")) = False Then
                    txtCompanyCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Code"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Name")) = False Then
                    txtCompanyName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Name"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Address")) = False Then
                    txtCompanyAddress.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Address"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_City")) = False Then
                    txtCompanyCity.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_City"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_State")) = False Then
                    txtCompanyState.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_State"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Country")) = False Then
                    txtCompanyCountry.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Country"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_PinCode")) = False Then
                    txtCompanyPinCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_PinCode"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_EmailID")) = False Then
                    txtCompanyEmail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_EmailID"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Establishment_Date")) = False Then
                    If dt.Rows(0).Item("Company_Establishment_Date") <> "" Then
                        txtEstablishmentDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("Company_Establishment_Date"), "D")
                    End If
                End If
                If IsDBNull(dt.Rows(0).Item("Company_ContactPerson")) = False Then
                    txtContactPerson.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_ContactPerson"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_MobileNo")) = False Then
                    txtMoblieNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_MobileNo"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_ContactEmailID")) = False Then
                    txtContactMail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_ContactEmailID"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_TelephoneNo")) = False Then
                    txtTelephoneNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_TelephoneNo"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_WebSite")) = False Then
                    txtWebSite.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_WebSite"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_ContactNo1")) = False Then
                    txtCompanyContactNo1.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_ContactNo1"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_ContactNo2")) = False Then
                    txtCompanyContactNo2.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_ContactNo2"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_HolderName")) = False Then
                    txtAccountHolderName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_HolderName"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_AccountNo")) = False Then
                    txtAccountNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_AccountNo"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Bankname")) = False Then
                    txtBankName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Bankname"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Branch")) = False Then
                    txtBranch.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Branch"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Conditions")) = False Then
                    txtConditions.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Conditions"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Paymentterms")) = False Then
                    txtPaymentterms.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Company_Paymentterms"))
                End If
                If IsDBNull(dt.Rows(0).Item("Company_Status")) = False Then
                    If dt.Rows(0).Item("Company_Status") = "Saved" Or dt.Rows(0).Item("Company_Status") = "Updated" Then
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    ElseIf dt.Rows(0).Item("Company_Status") = "" Then
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    End If
                End If
            End If
            sFileName = objclsCompanyDetails.getCompanyImageName(sSession.AccessCode, ddlExistingCompanyName.SelectedValue, "A")
            If sFileName <> "" And sFileName <> "." Then
                Dim imageDataURL As String = Server.MapPath("~/Images/" + sFileName)
                Dim bytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                Dim imageDataBase64 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                myLogoComp.ImageUrl = imageDataBase64
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCompanyDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Dim objCompanyDetails As New strCompanyDetails
        Dim dSDate As Date
        Dim fileName As String = ""
        Dim IID As Integer
        Try
            If txtCompanyCode.Text = "" Then
                lblError.Text = "Enter Firm’s registration number."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Code.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCode.Text.Trim.Length > 25 Then
                lblError.Text = "Firm’s registration number exceeded maximum size(max 25 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Code exceeded maximum size(max 25 character).','', 'error');", True)
                Exit Try
            End If
            If (objclsCompanyDetails.CheckCompanyNameCode(sSession.AccessCode, sSession.AccessCodeID, 0, "Company_Code", txtCompanyCode.Text) = True) Then
                lblError.Text = "Firm’s registration number already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Firm’s registration number already Exists.','', 'error');", True)
                Exit Try
            End If
            If txtCompanyName.Text = "" Then
                lblError.Text = "Enter Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Name.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyName.Text.Trim.Length > 500 Then
                lblError.Text = "Name exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Name exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If (objclsCompanyDetails.CheckCompanyNameCode(sSession.AccessCode, sSession.AccessCodeID, 0, "Company_Name", txtCompanyName.Text) = True) Then
                lblError.Text = "Company Name already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Company Name already Exists.','', 'error');", True)
                Exit Try
            End If
            If txtCompanyAddress.Text = "" Then
                lblError.Text = "Enter Head Office Address."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Head Office Address.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyAddress.Text.Trim.Length > 1000 Then
                lblError.Text = "Head Office Address exceeded maximum size(max 1000 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Head Office Address exceeded maximum size(max 1000 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyCity.Text = "" Then
                lblError.Text = "Enter City."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter City.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCity.Text.Trim.Length > 500 Then
                lblError.Text = "City exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('City exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyState.Text = "" Then
                lblError.Text = "Enter State."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter State.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyState.Text.Trim.Length > 500 Then
                lblError.Text = "State exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('State exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyCountry.Text = "" Then
                lblError.Text = "Enter Country."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Country.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCountry.Text.Trim.Length > 500 Then
                lblError.Text = "Country exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Country exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyPinCode.Text = "" Then
                lblError.Text = "Enter Pin Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Pin Code.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyPinCode.Text.Trim.Length > 15 Then
                lblError.Text = "Pin Code exceeded maximum size(max 15 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Pin Code exceeded maximum size(max 15 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyEmail.Text = "" Then
                lblError.Text = "Enter E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter E-Mail.','', 'warning');", True)
                Exit Try
            End If
            If txtEstablishmentDate.Text.Trim = "" Then
                lblError.Text = "Select Establishment Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Establishment Date.','', 'warning');", True)
                Exit Try
            Else
                Try
                    dSDate = DateTime.ParseExact(txtEstablishmentDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblError.Text = "Enter valid Establishment Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Establishment Date.','', 'warning');", True)
                    Exit Try
                End Try
            End If
            If txtContactPerson.Text = "" Then
                lblError.Text = "Enter Contact Person."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Contact Person.','', 'warning');", True)
                Exit Try
            End If
            If txtContactPerson.Text.Trim.Length > 500 Then
                lblError.Text = "Contact Person exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Contact Person Person exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtMoblieNo.Text = "" Then
                lblError.Text = "Enter Moblie No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Moblie No.','', 'warning');", True)
                Exit Try
            End If
            If txtMoblieNo.Text.Trim.Length > 10 Then
                lblError.Text = "Moblie No. exceeded maximum size(max 10 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Moblie No. exceeded maximum size(max 10 character).','', 'error');", True)
                Exit Try
            End If
            If txtContactMail.Text = "" Then
                lblError.Text = "Enter Contact E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Contact E-Mail.','', 'warning');", True)
                Exit Try
            End If
            If txtTelephoneNo.Text.Trim.Length > 15 Then
                lblError.Text = "Telephone No. Person exceeded maximum size(max 15 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Telephone No. exceeded maximum size(max 15 character).','', 'error');", True)
                Exit Try
            End If
            objCompanyDetails.iCompany_ID = 0
            If txtCompanyCode.Text <> "" Then
                objCompanyDetails.sCompany_Code = txtCompanyCode.Text
            Else
                objCompanyDetails.sCompany_Code = ""
            End If
            If txtCompanyName.Text <> "" Then
                objCompanyDetails.sCompany_Name = txtCompanyName.Text
            Else
                objCompanyDetails.sCompany_Name = ""
            End If
            If txtCompanyAddress.Text <> "" Then
                objCompanyDetails.sCompany_Address = txtCompanyAddress.Text
            Else
                objCompanyDetails.sCompany_Address = ""
            End If
            If txtCompanyCity.Text <> "" Then
                objCompanyDetails.sCompany_City = txtCompanyCity.Text
            Else
                objCompanyDetails.sCompany_City = ""
            End If
            If txtCompanyState.Text <> "" Then
                objCompanyDetails.sCompany_State = txtCompanyState.Text
            Else
                objCompanyDetails.sCompany_State = ""
            End If
            If txtCompanyCountry.Text <> "" Then
                objCompanyDetails.sCompany_Country = txtCompanyCountry.Text
            Else
                objCompanyDetails.sCompany_Country = ""
            End If
            If txtCompanyPinCode.Text <> "" Then
                objCompanyDetails.sCompany_PinCode = txtCompanyPinCode.Text
            Else
                objCompanyDetails.sCompany_PinCode = ""
            End If
            If txtCompanyEmail.Text <> "" Then
                objCompanyDetails.sCompany_EmailID = txtCompanyEmail.Text
            Else
                objCompanyDetails.sCompany_EmailID = ""
            End If
            objCompanyDetails.sCompany_Establishment_Date = dSDate
            If txtContactPerson.Text <> "" Then
                objCompanyDetails.sCompany_ContactPerson = txtContactPerson.Text
            Else
                objCompanyDetails.sCompany_ContactPerson = ""
            End If
            If txtMoblieNo.Text <> "" Then
                objCompanyDetails.sCompany_MobileNo = txtMoblieNo.Text
            Else
                objCompanyDetails.sCompany_MobileNo = ""
            End If
            If txtContactMail.Text <> "" Then
                objCompanyDetails.sCompany_ContactEmailID = txtContactMail.Text
            Else
                objCompanyDetails.sCompany_ContactEmailID = ""
            End If
            If txtTelephoneNo.Text <> "" Then
                objCompanyDetails.sCompany_TelephoneNo = txtTelephoneNo.Text
            Else
                objCompanyDetails.sCompany_TelephoneNo = ""
            End If
            If txtWebSite.Text <> "" Then
                objCompanyDetails.sCompany_WebSite = txtWebSite.Text
            Else
                objCompanyDetails.sCompany_WebSite = ""
            End If
            If txtCompanyContactNo1.Text <> "" Then
                objCompanyDetails.sCompany_ContactNo1 = txtCompanyContactNo1.Text
            Else
                objCompanyDetails.sCompany_ContactNo1 = ""
            End If
            If txtCompanyContactNo2.Text <> "" Then
                objCompanyDetails.sCompany_ContactNo2 = txtCompanyContactNo2.Text
            Else
                objCompanyDetails.sCompany_ContactNo2 = ""
            End If
            If txtAccountHolderName.Text <> "" Then
                objCompanyDetails.sCompany_HolderName = txtAccountHolderName.Text
            Else
                objCompanyDetails.sCompany_HolderName = ""
            End If
            If txtAccountNo.Text <> "" Then
                objCompanyDetails.sCompany_AccountNo = txtAccountNo.Text
            Else
                objCompanyDetails.sCompany_AccountNo = ""
            End If
            If txtBankName.Text <> "" Then
                objCompanyDetails.sCompany_Bankname = txtBankName.Text
            Else
                objCompanyDetails.sCompany_Bankname = ""
            End If
            If txtBranch.Text <> "" Then
                objCompanyDetails.sCompany_Branch = txtBranch.Text
            Else
                objCompanyDetails.sCompany_Branch = ""
            End If
            If txtConditions.Text <> "" Then
                objCompanyDetails.sCompany_Conditions = txtConditions.Text
            Else
                objCompanyDetails.sCompany_Conditions = ""
            End If
            If txtPaymentterms.Text <> "" Then
                objCompanyDetails.sCompany_Paymentterms = txtPaymentterms.Text
            Else
                objCompanyDetails.sCompany_Paymentterms = ""
            End If

            objCompanyDetails.iCompany_CrBy = sSession.UserID
            objCompanyDetails.iCompany_UpdatedBy = sSession.UserID
            objCompanyDetails.sCompany_IPAddress = sSession.IPAddress
            objCompanyDetails.iCompany_CompID = sSession.AccessCodeID
            Arr = objclsCompanyDetails.SaveCompanyDetails(sSession.AccessCode, objCompanyDetails)
            IID = Arr(1)

            BindExistingCompanyName()
            ddlExistingCompanyName.SelectedValue = IID

            If CompanyLogoUpload.FileName <> "" Then
                fileName = Server.MapPath("~/Images/" + CompanyLogoUpload.FileName)
                Dim folderPath As String = Server.MapPath("~/Images/")
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                CompanyLogoUpload.SaveAs(folderPath & Path.GetFileName(CompanyLogoUpload.FileName))
                SaveToBigDataPurchase(fileName)
                lblError.Text = Path.GetFileName(CompanyLogoUpload.FileName) + " has been uploaded."
            End If
            ddlExistingCompanyName_SelectedIndexChanged(sender, e)
            lblError.Text = "Successfully Saved."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Company Details", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved','', 'success');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message) 'changes
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim Arr As Array
        Dim objCompanyDetails As New strCompanyDetails
        Dim dSDate As Date
        Dim fileName As String = ""
        Dim IID As Integer
        Try
            If txtCompanyCode.Text = "" Then
                lblError.Text = "Enter Firm’s registration number."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Code.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCode.Text.Trim.Length > 25 Then
                lblError.Text = "Firm’s registration number exceeded maximum size(max 25 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Code exceeded maximum size(max 25 character).','', 'error');", True)
                Exit Try
            End If
            If (objclsCompanyDetails.CheckCompanyNameCode(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCompanyName.SelectedValue, "Company_Code", txtCompanyCode.Text) = True) Then
                lblError.Text = "Firm’s registration number already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Firm’s registration number already Exists.','', 'error');", True)
                Exit Try
            End If
            If txtCompanyName.Text = "" Then
                lblError.Text = "Enter Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Name.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyName.Text.Trim.Length > 500 Then
                lblError.Text = "Name exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Name exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If (objclsCompanyDetails.CheckCompanyNameCode(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCompanyName.SelectedValue, "Company_Name", txtCompanyName.Text) = True) Then
                lblError.Text = "Company Name already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Company Name already Exists.','', 'error');", True)
                Exit Try
            End If
            If txtCompanyAddress.Text = "" Then
                lblError.Text = "Enter Head Office Address ."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Head Office Address .','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyAddress.Text.Trim.Length > 1000 Then
                lblError.Text = "Head Office Address exceeded maximum size(max 1000 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Head Office Address exceeded maximum size(max 1000 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyCity.Text = "" Then
                lblError.Text = "Enter City."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter City.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCity.Text.Trim.Length > 500 Then
                lblError.Text = "City exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('City exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyState.Text = "" Then
                lblError.Text = "Enter State."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter State.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyState.Text.Trim.Length > 500 Then
                lblError.Text = "State exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('State exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyCountry.Text = "" Then
                lblError.Text = "Enter Country."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Country.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyCountry.Text.Trim.Length > 500 Then
                lblError.Text = "Country exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Country exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyPinCode.Text = "" Then
                lblError.Text = "Enter Pin Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Pin Code.','', 'warning');", True)
                Exit Try
            End If
            If txtCompanyPinCode.Text.Trim.Length > 15 Then
                lblError.Text = "Pin Code exceeded maximum size(max 15 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Pin Code exceeded maximum size(max 15 character).','', 'error');", True)
                Exit Try
            End If
            If txtCompanyEmail.Text = "" Then
                lblError.Text = "Enter E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter E-Mail.','', 'warning');", True)
                Exit Try
            End If
            If txtEstablishmentDate.Text.Trim = "" Then
                lblError.Text = "Select Establishment Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Establishment Date.','', 'warning');", True)
                Exit Try
            Else
                Try
                    dSDate = DateTime.ParseExact(txtEstablishmentDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblError.Text = "Enter valid Establishment Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Establishment Date.','', 'warning');", True)
                    Exit Try
                End Try
            End If
            If txtContactPerson.Text = "" Then
                lblError.Text = "Enter Contact Person."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Contact Person.','', 'warning');", True)
                Exit Try
            End If
            If txtContactPerson.Text.Trim.Length > 500 Then
                lblError.Text = "Contact Person exceeded maximum size(max 500 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Contact Person Person exceeded maximum size(max 500 character).','', 'error');", True)
                Exit Try
            End If
            If txtMoblieNo.Text = "" Then
                lblError.Text = "Enter Moblie No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Moblie No.','', 'warning');", True)
                Exit Try
            End If
            If txtMoblieNo.Text.Trim.Length > 10 Then
                lblError.Text = "Moblie No. exceeded maximum size(max 10 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Moblie No. exceeded maximum size(max 10 character).','', 'error');", True)
                Exit Try
            End If
            If txtContactMail.Text = "" Then
                lblError.Text = "Enter Contact E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Contact E-Mail.','', 'warning');", True)
                Exit Try
            End If
            If txtTelephoneNo.Text.Trim.Length > 15 Then
                lblError.Text = "Telephone No Person exceeded maximum size(max 15 character)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Telephone No. exceeded maximum size(max 15 character).','', 'error');", True)
                Exit Try
            End If
            If ddlExistingCompanyName.SelectedIndex > 0 Then
                objCompanyDetails.iCompany_ID = ddlExistingCompanyName.SelectedValue
            Else
                objCompanyDetails.iCompany_ID = 0
            End If
            objCompanyDetails.sCompany_Code = objclsGRACeGeneral.SafeSQL(txtCompanyCode.Text.Trim)
            objCompanyDetails.sCompany_Name = objclsGRACeGeneral.SafeSQL(txtCompanyName.Text.Trim)
            objCompanyDetails.sCompany_Address = objclsGRACeGeneral.SafeSQL(txtCompanyAddress.Text.Trim)
            objCompanyDetails.sCompany_City = objclsGRACeGeneral.SafeSQL(txtCompanyCity.Text.Trim)
            objCompanyDetails.sCompany_State = objclsGRACeGeneral.SafeSQL(txtCompanyState.Text.Trim)
            objCompanyDetails.sCompany_Country = objclsGRACeGeneral.SafeSQL(txtCompanyCountry.Text.Trim)
            objCompanyDetails.sCompany_PinCode = objclsGRACeGeneral.SafeSQL(txtCompanyPinCode.Text.Trim)
            objCompanyDetails.sCompany_EmailID = objclsGRACeGeneral.SafeSQL(txtCompanyEmail.Text.Trim)
            objCompanyDetails.sCompany_Establishment_Date = txtEstablishmentDate.Text
            objCompanyDetails.sCompany_ContactPerson = objclsGRACeGeneral.SafeSQL(txtContactPerson.Text.Trim)
            objCompanyDetails.sCompany_MobileNo = objclsGRACeGeneral.SafeSQL(txtMoblieNo.Text.Trim)
            objCompanyDetails.sCompany_ContactEmailID = objclsGRACeGeneral.SafeSQL(txtContactMail.Text.Trim)
            objCompanyDetails.sCompany_TelephoneNo = objclsGRACeGeneral.SafeSQL(txtTelephoneNo.Text.Trim)
            objCompanyDetails.sCompany_WebSite = objclsGRACeGeneral.SafeSQL(txtWebSite.Text.Trim)
            objCompanyDetails.sCompany_ContactNo1 = objclsGRACeGeneral.SafeSQL(txtCompanyContactNo1.Text.Trim)
            objCompanyDetails.sCompany_ContactNo2 = objclsGRACeGeneral.SafeSQL(txtCompanyContactNo2.Text.Trim)

            objCompanyDetails.sCompany_HolderName = objclsGRACeGeneral.SafeSQL(txtAccountHolderName.Text.Trim)
            objCompanyDetails.sCompany_AccountNo = objclsGRACeGeneral.SafeSQL(txtAccountNo.Text.Trim)
            objCompanyDetails.sCompany_Bankname = objclsGRACeGeneral.SafeSQL(txtBankName.Text.Trim)
            objCompanyDetails.sCompany_Branch = objclsGRACeGeneral.SafeSQL(txtBranch.Text.Trim)
            objCompanyDetails.sCompany_Conditions = objclsGRACeGeneral.SafeSQL(txtConditions.Text.Trim)
            objCompanyDetails.sCompany_Paymentterms = objclsGRACeGeneral.SafeSQL(txtPaymentterms.Text.Trim)

            objCompanyDetails.iCompany_CrBy = sSession.UserID
            objCompanyDetails.iCompany_UpdatedBy = sSession.UserID
            objCompanyDetails.sCompany_IPAddress = sSession.IPAddress
            objCompanyDetails.iCompany_CompID = sSession.AccessCodeID
            Arr = objclsCompanyDetails.SaveCompanyDetails(sSession.AccessCode, objCompanyDetails)
            IID = Arr(1)

            'BindExistingCompanyName()
            ddlExistingCompanyName.SelectedValue = IID

            If CompanyLogoUpload.FileName <> "" Then
                fileName = Server.MapPath("~/Images/" + CompanyLogoUpload.FileName)
                Dim folderPath As String = Server.MapPath("~/Images/")
                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If
                CompanyLogoUpload.SaveAs(folderPath & Path.GetFileName(CompanyLogoUpload.FileName))
                lblError.Text = Path.GetFileName(CompanyLogoUpload.FileName) + " has been uploaded."
                SaveToBigDataPurchase(fileName)
            End If
            ddlExistingCompanyName_SelectedIndexChanged(sender, e)
            lblError.Text = "Successfully Updated."

            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Company Details", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            ClearAllCompanyDetails() : ddlExistingCompanyName.SelectedIndex = 0
            imgbtnAddBranch_Click(sender, e) : lstboxBranch.Items.Clear() : ddlExistingCompanyBranch.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            If ddlExistingCompanyName.SelectedIndex > 0 Then
                dt = objclsCompanyDetails.LoadCompanyDetailsReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCompanyName.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                    Exit Sub
                End If
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CompanyDetails.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Company Details", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                Response.AddHeader("content-disposition", "attachment; filename=CompanyDetails" + ".pdf")
                Response.BinaryWrite(RptViewer)
                Response.Flush()
                Response.End()
            Else
                lblError.Text = "Select Existing Company."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Existing Company.','', 'error');", True)
                Exit Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            If ddlExistingCompanyName.SelectedIndex > 0 Then
                dt = objclsCompanyDetails.LoadCompanyDetailsReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingCompanyName.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                    Exit Sub
                End If
                ReportViewer1.Reset()
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CompanyDetails.rdlc")
                ReportViewer1.LocalReport.Refresh()
                Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Company Details", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                Response.AddHeader("content-disposition", "attachment; filename=CompanyDetails" + ".xls")
                Response.BinaryWrite(RptViewer)
                Response.Flush()
                Response.End()
            Else
                lblError.Text = "Select Existing Company."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Existing Company.','', 'error');", True)
                Exit Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Protected Sub lstboxBranch_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxBranch.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ClearAllBranchDetails()
            If ddlExistingCompanyBranch.SelectedIndex > 0 Then
                If lstboxBranch.SelectedIndex > -1 Then
                    txtBranchName.Text = objclsGRACeGeneral.SafeSQL(lstboxBranch.SelectedItem.Text)
                    dt = objclsCompanyDetails.LoadCompanyBranchDetails(sSession.AccessCode, sSession.AccessCodeID, lstboxBranch.SelectedValue, ddlExistingCompanyBranch.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("Company_Branch_Contact_Person")) = False Then
                            txtBranchContactPerson.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Contact_Person"))
                        End If
                        If IsDBNull(dt.Rows(0)("Company_Branch_Contact_MobileNo")) = False Then
                            txtBranchContactMobileNo.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Contact_MobileNo"))
                        End If
                        If IsDBNull(dt.Rows(0)("Company_Branch_Contact_LandLineNo")) = False Then
                            txtBranchContactLandLineNo.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Contact_LandLineNo"))
                        End If
                        If IsDBNull(dt.Rows(0)("Company_Branch_Contact_Email")) = False Then
                            txtBranchContactEmail.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Contact_Email"))
                        End If
                        If IsDBNull(dt.Rows(0)("Company_Branch_Designation")) = False Then
                            txtBranchDesignation.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Designation"))
                        End If
                        If IsDBNull(dt.Rows(0)("Company_Branch_Address")) = False Then
                            txtBranchAddress.Text = objclsGRACeGeneral.SafeSQL(dt.Rows(0)("Company_Branch_Address"))
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstboxBranch_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnAddBranch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddBranch.Click
        Try
            ClearAllBranchDetails()
            If lstboxBranch.Items.Count > 0 Then
                lstboxBranch.SelectedIndex = -1
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddBranch_Click")
        End Try
    End Sub
    Private Sub ddlExistingCompanyBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingCompanyBranch.SelectedIndexChanged
        Try
            ClearAllBranchDetails()
            lstboxBranch.Items.Clear()
            If ddlExistingCompanyBranch.SelectedIndex > 0 Then
                BindBranchs(ddlExistingCompanyBranch.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingCompanyBranch_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Public Sub ClearAllBranchDetails()
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnAddBranch.Visible = True : imgbtnSaveUpdateBranch.Visible = True
            txtBranchName.Text = "" : txtBranchContactPerson.Text = "" : txtBranchAddress.Text = ""
            txtBranchContactEmail.Text = "" : txtBranchContactMobileNo.Text = "" : txtBranchContactLandLineNo.Text = "" : txtBranchDesignation.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearBranch" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub imgbtnSaveUpdateBranch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveUpdateBranch.Click
        Dim Arr() As String
        Dim objsCompanyBranch As New strCompanyBranch
        Try
            lblError.Text = ""
            If ddlExistingCompanyBranch.SelectedIndex > 0 Then
                If txtBranchName.Text = "" Then
                    lblError.Text = "Branch Enter Office Name."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Loaction Name.','', 'warning');", True)
                    txtBranchName.Focus()
                    Exit Sub
                End If
                If txtBranchContactPerson.Text = "" Then
                    lblError.Text = "Enter Contact Person Name."
                    txtBranchContactPerson.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Contact Person Name.','', 'warning');", True)
                    Exit Sub
                End If
                If txtBranchAddress.Text = "" Then
                    lblError.Text = "Enter Branch Office Address."
                    txtBranchAddress.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Branch Office Address.','', 'warning');", True)
                    Exit Sub
                End If
                If lstboxBranch.SelectedIndex < 0 Then
                    If objclsCompanyDetails.CheckCompanyBranch(sSession.AccessCode, sSession.AccessCodeID, Trim(txtBranchName.Text), ddlExistingCompanyBranch.SelectedValue) <> 0 Then
                        lblError.Text = "This Branch already exist."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('This Branch already exist.','', 'error');", True)
                        Exit Sub
                    End If
                End If
                If txtBranchContactPerson.Text.Trim.Length > 50 Then
                    lblError.Text = "Contact Person exceeded maximum size(max 50 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Contact Person exceeded maximum size(max 50 characters).','', 'error');", True)
                    txtBranchContactPerson.Focus()
                    Exit Sub
                End If
                If txtBranchName.Text.Trim.Length > 100 Then
                    lblError.Text = "Branch Office Name exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Branch Name exceeded maximum size(max 100 characters).','', 'error');", True)
                    txtBranchName.Focus()
                    Exit Sub
                End If
                If txtBranchContactMobileNo.Text.Trim.Length > 15 Then
                    lblError.Text = "Contact Mobile number exceeded maximum size(max 15 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Contact Mobile number exceeded maximum size(max 15 characters).','', 'error');", True)
                    txtBranchContactMobileNo.Focus()
                    Exit Sub
                End If
                If txtBranchDesignation.Text.Trim.Length > 500 Then
                    lblError.Text = "Designation exceeded maximum size(max 500 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Designation exceeded maximum size(max 500 characters).','', 'error');", True)
                    txtBranchDesignation.Focus()
                    Exit Sub
                End If
                If txtBranchContactLandLineNo.Text.Trim.Length > 50 Then
                    lblError.Text = "Contact LandLine number exceeded maximum size(max 50 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Contact LandLine number exceeded maximum size(max 50 characters).','', 'error');", True)
                    txtBranchContactLandLineNo.Focus()
                    Exit Sub
                End If
                If txtBranchContactEmail.Text.Trim.Length > 100 Then
                    lblError.Text = "E-Mail exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('E-Mail exceeded maximum size(max 100 characters).','', 'error');", True)
                    txtBranchContactEmail.Focus()
                    Exit Sub
                End If
                If txtBranchAddress.Text.Trim.Length > 500 Then
                    lblError.Text = "Branch Office Address exceeded maximum size(max 500 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Branch Office Address exceeded maximum size(max 500 characters).','', 'error');", True)
                    txtBranchAddress.Focus()
                    Exit Sub
                End If
                If lstboxBranch.SelectedIndex > -1 Then
                    objsCompanyBranch.iCompany_Branch_Id = lstboxBranch.SelectedValue
                Else
                    objsCompanyBranch.iCompany_Branch_Id = 0
                End If
                objsCompanyBranch.iCompany_Branch_CompanyID = ddlExistingCompanyBranch.SelectedValue
                objsCompanyBranch.sCompany_Branch_Name = objclsGRACeGeneral.SafeSQL(txtBranchName.Text.Trim())
                objsCompanyBranch.sCompany_Branch_Address = txtBranchAddress.Text
                objsCompanyBranch.sCompany_Branch_DelFlag = "A"
                objsCompanyBranch.sCompany_Branch_Contact_Person = objclsGRACeGeneral.SafeSQL(txtBranchContactPerson.Text.Trim())
                objsCompanyBranch.sCompany_Branch_Contact_MobileNo = objclsGRACeGeneral.SafeSQL(txtBranchContactMobileNo.Text.Trim())
                objsCompanyBranch.sCompany_Branch_Contact_LandLineNo = objclsGRACeGeneral.SafeSQL(txtBranchContactLandLineNo.Text.Trim())
                objsCompanyBranch.sCompany_Branch_Contact_Email = objclsGRACeGeneral.SafeSQL(txtBranchContactEmail.Text.Trim())
                objsCompanyBranch.sCompany_Branch_Designation = objclsGRACeGeneral.SafeSQL(txtBranchDesignation.Text.Trim())
                objsCompanyBranch.iCompany_Branch_CRBY = sSession.UserID
                objsCompanyBranch.iCompany_Branch_UpdatedBy = sSession.UserID
                objsCompanyBranch.sCompany_Branch_STATUS = "A"
                objsCompanyBranch.sCompany_Branch_IPAddress = sSession.IPAddress
                objsCompanyBranch.iCompany_Branch_CompID = sSession.AccessCodeID

                Arr = objclsCompanyDetails.SaveCompanyBranch(sSession.AccessCode, objsCompanyBranch)
                If Arr(0) = 3 Then
                    lblError.Text = "Successfully Saved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved.','', 'success');", True)
                End If
                If Arr(0) = 2 Then
                    lblError.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                End If
                BindBranchs(ddlExistingCompanyBranch.SelectedValue)
                lstboxBranch.SelectedValue = Arr(1)
                lstboxBranch_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Company."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Company.','', 'error');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveUpdateBranch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindBranchs(ByVal iCompanyID As Integer)
        Try
            lstboxBranch.DataSource = objclsCompanyDetails.GetCompanyBranchDetails(sSession.AccessCode, sSession.AccessCodeID, iCompanyID)
            lstboxBranch.DataTextField = "Company_Branch_Name"
            lstboxBranch.DataValueField = "Company_Branch_Id"
            lstboxBranch.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBranchs" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
