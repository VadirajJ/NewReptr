Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Net.Mail
Imports System.Data
Public Class CustLogin
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Private objclsServerConnection As New clsServerDB
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        Me.Form.DefaultButton = Me.imgbtnLogin.UniqueID
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Dim sPath As String = "", sPath2 As String = "", sPath3 As String = ""
        Dim sConnectionstring As String = ""
        Dim objclsLogin As New CustclsLogin
        Try
            sPath = Server.MapPath("~\Scripts\Trace\Script1.txt")
            sPath2 = Server.MapPath("~\Scripts\Trace\Script2.txt")
            sPath3 = Server.MapPath("~\Scripts\Trace\Script3.txt")

            If objclsLogin.CheckDatabaseExists("MMCSPLCR", txtDatabase.Text) = True Then
                lblValidationMsg.Text = "Database Name Already Exist."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            objclsServerConnection.createDatabase(txtServerName.Text, txtLogin.Text, txtsPassword.Text, txtDatabase.Text)
            objclsServerConnection.createTables(txtServerName.Text, txtLogin.Text, txtsPassword.Text, txtDatabase.Text, sPath)
            objclsServerConnection.createTablesForSP(txtServerName.Text, txtLogin.Text, txtsPassword.Text, txtDatabase.Text, sPath2)
            objclsServerConnection.createTables(txtServerName.Text, txtLogin.Text, txtsPassword.Text, txtDatabase.Text, sPath3)
            ' objclsServerConnection.InsertTraCompanyDetails(txtServerName.Text, txtLogin.Text, txtsPassword.Text, txtDatabase.Text)

            lblValidationMsg.Text = "Successfully,Created Database"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            txtLogin.Text = "" : txtsPassword.Text = ""
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnLogin_Click(sender As Object, e As EventArgs) Handles imgbtnLogin.Click
        Dim sPassword As String
        Dim objclsLogin As New CustclsLogin
        Dim iValidUserID As Integer, iUserID As Integer
        Try
            iValidUserID = objclsLogin.CheckValidLoginUserName(txtUserName.Text.Trim)

            If iValidUserID > 0 Then
                Dim sIPAddress As String = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
                sPassword = objclsLogin.EncryptPassword(txtPassword.Text.Trim)
                iUserID = objclsLogin.GetLoginUserID(txtUserName.Text.Trim, sPassword)
                If iUserID > 0 Then
                    objclsLogin.UpdateLoginDetails(iUserID, sIPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Else
                    objclsLogin.UnSuccusfullAttemptUpdate(txtUserName.Text)
                    lblValidationMsg.Text = "Invalid Login Details"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                    txtUserName.Text = ""
                End If
            ElseIf iUserID = 0 Then
                lblValidationMsg.Text = "Invalid Login Details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                txtUserName.Text = ""
            End If
        Catch
            If txtUserName.Text = "admin" Or txtUserName.Text = "Admin" Then

            Else
                lblValidationMsg.Text = "Invalid Login Details"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
            End If
        End Try
    End Sub
End Class

