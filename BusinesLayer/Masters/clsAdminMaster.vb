Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAdminMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Public iID As Integer
    Public sCode As String
    Public sDesc As String
    Public sCategory As String
    Public sRemarks As String
    Public iKeyComponent As Integer
    Public iAuditAssignment As Integer
    Public iBillingType As Integer
    Public sModule As String
    Public iRiskCategory As Integer
    Public sStatus As String
    Public dcmmRate As Double
    Public sCMMAct As String
    Public sCMMHSNSAC As String
    Public sDelflag As String
    Public iCrBy As Integer
    Public iUpdatedBy As Integer
    Public sIpAddress As String
    Public iCompId As Integer
    Public iYearID As Integer
    Public dStartValue As String
    Public dEndValue As String
    Public sName As String
    Public sColor As String
    Public sFLAG As String
    Public Function LoadAdminMasterDesgRoleDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Mas_ID AS PKID,Mas_Description AS Name From " & sTableName & " where Mas_CompID=" & iAcID & " Order By Mas_Description ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllAdminMasterOtherDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And cmm_delflag in ('A','W') Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAct(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select distinct(CMM_Act) From Content_Management_Master Where CMM_CompID=" & iCompID & " and CMM_Act<>'' and CMM_Act<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActselected(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal id As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CMM_Act From Content_Management_Master Where CMM_CompID=" & iCompID & " and cmm_id = " & id & " and CMM_Act<>'' and CMM_Act<>'NULL'"
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAdminMasterOtherDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & " And cmm_delflag='A' Order By CMM_Desc ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadExistingRequestDocument(ByVal sAc As String, ByVal iAcID As Integer, ByVal iExistingRequestDocumentType As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select DRL_DRLID,DRL_Name From Audit_Doc_Request_List Where DRL_DocTypeID=" & iExistingRequestDocumentType & " And DRL_CompID=" & iAcID & " Order By DRL_Name ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAdminMasterDesgRoleDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iDescID As Integer, ByVal sTableName As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From " & sTableName & " Where Mas_id=" & iDescID & " and Mas_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetExistingRequestDocument(ByVal sAc As String, ByVal iAcID As Integer, ByVal iExistingRequestDocumentListID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select DRL_DRLID,DRL_Name,DRL_Description,DRL_DocumentType From Audit_Doc_Request_List Where DRL_DRLID=" & iExistingRequestDocumentListID & " And DRL_CompID=" & iAcID & " Order By DRL_Name ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRequestDocumentList(ByVal sAc As String, ByVal iAcID As Integer, ByVal iExistingRequestDocumentType As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("RequestDocumentType")
            dtTab.Columns.Add("DocumentRequestList")
            dtTab.Columns.Add("Description")
            sSql = "Select DRL_DRLID,DRL_Name,DRL_Description,DRL_DocumentType,CMM_Desc From Audit_Doc_Request_List "
            sSql = sSql & " Left Join Content_Management_Master On CMM_ID=DRL_DocTypeID And CMM_Category='DRL'"
            sSql = sSql & " Where DRL_DocTypeID=" & iExistingRequestDocumentType & " And DRL_CompID=" & iAcID & " Order By DRL_Name ASC"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow()
                dr("SrNo") = i + 1
                dr("DocumentRequestList") = dt.Rows(i)("CMM_Desc")
                dr("RequestDocumentType") = dt.Rows(i)("DRL_Name")
                dr("Description") = dt.Rows(i)("DRL_Description")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAdminMasterOtherDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iDescID As Integer, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select cmm_ID,CMM_Code,CMM_Desc,CMS_Remarks,CMM_DelFlag,CMM_Rate,CMS_KeyComponent,CMM_HSNSAC,CMM_Act From Content_Management_Master Where CMM_ID=" & iDescID & " And CMM_Category='" & sType & "' And CMM_CompID=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDeleteorNot(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal sTableName As String, ByVal sCoulmnName As String,
                                     ByVal iMasID As Integer, ByVal sType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from " & sTableName & " where " & sCoulmnName & "='" & sDesc & "'"
            If sType = "DESGROLE" Then
                If iMasID > 0 Then
                    sSql = sSql & " And Mas_ID=" & iMasID & " and Mas_Delflag='D'"
                End If
            Else
                If iMasID > 0 Then
                    sSql = sSql & " And CMM_ID=" & iMasID & " and CMM_DelFlag='D'"
                End If
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal sTableName As String, ByVal sCoulmnName As String,
                                         ByVal iMasID As Integer, ByVal sType As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from " & sTableName & " where " & sCoulmnName & "='" & sDesc & "'"
            If sType = "DESGROLE" Then
                If iMasID > 0 Then
                    sSql = sSql & " And Mas_ID <> " & iMasID & ""
                End If
            Else
                sSql = sSql & " And cmm_Category='" & sType & "'"
                If iMasID > 0 Then
                    sSql = sSql & " And CMM_ID <> " & iMasID & ""
                End If
            End If
            CheckExistingDetails = objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveOrUpdateDtls(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasID As Integer, ByVal sMasCode As String, ByVal sMasDesc As String, ByVal sMasNotes As String, ByVal sTableName As String, ByVal iUserID As Integer, ByVal sIPAddress As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(9) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iMasID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Code", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = sMasCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Description", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sMasDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_Notes", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sMasNotes
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@mas_Createdby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@mas_Updatedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@Mas_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iAcID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            If UCase(sTableName) = "SAD_GRPDESGN_GENERAL_MASTER" Then
                Arr = objDBL.ExecuteSPForInsertARR(sAc, "spSAD_GRPDESGN_General_Master", 1, Arr, ObjParam)
            ElseIf UCase(sTableName) = "SAD_GRPORLVL_GENERAL_MASTER" Then
                Arr = objDBL.ExecuteSPForInsertARR(sAc, "spSAD_GrpOrLvl_General_Master", 1, Arr, ObjParam)
            End If
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateGeneralMasterStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal sTableName As String, ByVal iUserId As Integer,
                                         ByVal sIPAddress As String, ByVal sStatus As String, ByVal sType As String)
        Dim sSql As String = ""
        Try
            If sType = "DESGROLE" Then
                sSql = "Update " & sTableName & " Set Mas_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " Mas_delflag='A',Mas_Status='A',mas_Approvedby=" & iUserId & ",mas_Approvedon=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " Mas_delflag='D',Mas_Status='AD',Mas_DeletedBy=" & iUserId & ",Mas_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " Mas_delflag='A',Mas_Status='AR',Mas_RecalledBy=" & iUserId & ",Mas_RecalledOn=GetDate()"
                End If
                sSql = sSql & " Where Mas_Id=" & iMasId & ""
            ElseIf sType = "OTHERS" Then
                sSql = "Update Content_Management_Master Set CMM_IPAddress='" & sIPAddress & "',"
                If sStatus = "W" Then
                    sSql = sSql & " CMM_DelFlag='A',CMM_Status='A',CMM_ApprovedBy=" & iUserId & ",CMM_ApprovedOn=GetDate()"
                ElseIf sStatus = "D" Then
                    sSql = sSql & " CMM_DelFlag='D',CMM_Status='AD',CMM_DeletedBy=" & iUserId & ",CMM_DeletedOn=GetDate()"
                ElseIf sStatus = "A" Then
                    sSql = sSql & " CMM_DelFlag='A',CMM_Status='AR',CMM_RecallBy=" & iUserId & ",CMM_RecallOn=GetDate()"
                End If
                sSql = sSql & " Where CMM_ID=" & iMasId & ""
            End If
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadGeneralMasterDESGROLEGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String, ByVal iStatus As Integer, ByVal sSearchText As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Notes")
            dt.Columns.Add("Status")
            sSql = "Select * From " & sTableName & " Where Mas_CompID=" & iAcID & " "
            If iStatus = 0 Then
                sSql = sSql & " And Mas_delflag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And Mas_delflag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And Mas_delflag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And Mas_Description like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By Mas_Description ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("Mas_id")
                dr("DescName") = ds.Tables(0).Rows(i)("Mas_Description")
                If IsDBNull(ds.Tables(0).Rows(i)("Mas_Notes")) = False Then
                    If ds.Tables(0).Rows(i)("Mas_Notes") <> "NULL" Then
                        dr("Notes") = ds.Tables(0).Rows(i)("Mas_Notes")
                    End If
                End If
                dr("Description") = ds.Tables(0).Rows(i)("Mas_Description")
                If IsDBNull(ds.Tables(0).Rows(i)("Mas_delflag")) = False Then
                    If ds.Tables(0).Rows(i)("Mas_delflag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("Mas_delflag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("Mas_delflag") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralMasterOTHERGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sTableName As String, ByVal iStatus As Integer, ByVal sSearchText As String, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Notes")
            dt.Columns.Add("Act")
            dt.Columns.Add("IsCompliance")
            dt.Columns.Add("Status")
            sSql = "Select * From Content_Management_Master Where CMM_CompID=" & iAcID & " And cmm_Category='" & sType & "' "
            If iStatus = 0 Then
                sSql = sSql & " And CMM_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CMM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CMM_DelFlag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And CMM_Desc like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By CMM_Desc ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("CMM_ID")
                dr("DescName") = ds.Tables(0).Rows(i)("CMM_Desc")
                dr("Description") = ds.Tables(0).Rows(i)("CMM_Desc")
                If IsDBNull(ds.Tables(0).Rows(i)("CMS_Remarks")) = False Then
                    If ds.Tables(0).Rows(i)("CMS_Remarks") <> "NULL" Then
                        dr("Notes") = ds.Tables(0).Rows(i)("CMS_Remarks")
                    End If
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("CMM_Act")) = False Then
                    If ds.Tables(0).Rows(i)("CMM_Act") <> "NULL" Then
                        dr("Act") = ds.Tables(0).Rows(i)("CMM_Act")
                    End If
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("CMS_KeyComponent")) = False Then
                    If ds.Tables(0).Rows(i)("CMS_KeyComponent").ToString() = "1" Then
                        dr("IsCompliance") = "Yes"
                    Else
                        dr("IsCompliance") = "No"
                    End If
                Else
                    dr("IsCompliance") = "No"
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("CMM_DelFlag")) = False Then
                    If ds.Tables(0).Rows(i)("CMM_DelFlag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("CMM_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("CMM_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveMasterDetails(ByVal sAC As String, ByVal objclsMaster As clsAdminMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Code", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsMaster.sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Desc", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Category", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objclsMaster.sCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_KeyComponent", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iKeyComponent
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cms_Module", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objclsMaster.sModule
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_RiskCategory", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iRiskCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_Rate", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objclsMaster.dcmmRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_Act", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsMaster.sCMMAct
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_HSNSAC", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsMaster.sCMMHSNSAC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@cmm_delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_IpAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CMM_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spContent_Management_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDocumentTypeDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iDRLID As Integer, ByVal iDocTypeID As Integer, ByVal sName As String, ByVal sDescription As String,
                                            ByVal iUserID As Integer, ByVal sIPAddress As String) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_DRLID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDRLID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_DocTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iDocTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = sName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_Description", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = sDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_DocumentType", OleDb.OleDbType.Integer, 8)
            ObjParam(iParamCount).Value = 1
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iUserID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = sIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DRL_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iACID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_Doc_Request_List", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckExistingDTDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As String, ByVal iDocTypeID As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from Audit_Doc_Request_List where DRL_DocTypeID=" & iDocTypeID & " And DRL_Name='" & sDesc & "'"
            CheckExistingDTDetails = objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadGeneralMasterDRLDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, ByVal sSearchText As String, ByVal sType As String) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Dim sDesc As String = "", sDescription As String = ""
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescName")
            dt.Columns.Add("RequestDocumentType")
            dt.Columns.Add("Status")
            sSql = "Select CMM_Desc,DRL_Name,CMM_DelFlag From Content_Management_Master"
            sSql = sSql & " Left Join Audit_Doc_Request_List On CMM_ID=DRL_DocTypeID And DRL_CompID=" & iAcID & ""
            sSql = sSql & " Where CMM_Category='DRL' And CMM_CompID=" & iAcID & ""
            If iStatus = 0 Then
                sSql = sSql & " And CMM_DelFlag='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And CMM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And CMM_DelFlag='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And CMM_Desc like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By CMM_Desc ASC"
            dtTab = objDBL.SQLExecuteDataTable(sAc, sSql)
            For i = 0 To dtTab.Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                If IsDBNull(dtTab.Rows(i)("CMM_Desc")) = False Then
                    sDesc = dtTab.Rows(i)("CMM_Desc")
                    If sDescription <> sDesc Then
                        dr("DescName") = sDesc
                        sDescription = sDesc
                    End If
                End If
                If IsDBNull(dtTab.Rows(i)("DRL_Name")) = False Then
                    dr("RequestDocumentType") = dtTab.Rows(i)("DRL_Name")
                End If
                If IsDBNull(dtTab.Rows(i)("CMM_DelFlag")) = False Then
                    If dtTab.Rows(i)("CMM_DelFlag") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf dtTab.Rows(i)("CMM_DelFlag") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf dtTab.Rows(i)("CMM_DelFlag") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditAssignmentSTMasterReportDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, iAuditAssignmentId As Integer, ByVal sAuditAssignmentText As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("Status")
            sSql = "Select * From AuditAssignmentSubTask_Master Where AM_CompId=" & iAcID & " And AM_AuditAssignmentID=" & iAuditAssignmentId & " "
            If iStatus = 0 Then
                sSql = sSql & " And AM_DELFLG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And AM_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And AM_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By AM_Name ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("AM_ID")
                If ds.Tables(0).Rows(i)("AM_BillingTypeID") = 0 Then
                    dr("Description") = sAuditAssignmentText & " - " & ds.Tables(0).Rows(i)("AM_Name") + " - Billable"
                ElseIf ds.Tables(0).Rows(i)("AM_BillingTypeID") = 1 Then
                    dr("Description") = sAuditAssignmentText & " - " & ds.Tables(0).Rows(i)("AM_Name") + " - Non Billable"
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("AM_DELFLG")) = False Then
                    If ds.Tables(0).Rows(i)("AM_DELFLG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("AM_DELFLG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("AM_DELFLG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadAuditAssignmentSTMasterGridDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iStatus As Integer, iAuditAssignmentId As Integer, ByVal sSearchText As String) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("DescID")
            dt.Columns.Add("cmm_desc")
            dt.Columns.Add("DescName")
            dt.Columns.Add("Description")
            dt.Columns.Add("BillingType")
            dt.Columns.Add("Status")
            'sSql = "Select * From AuditAssignmentSubTask_Master Where AM_AuditAssignmentID=" & iAuditAssignmentId & " And AM_CompId=" & iAcID & " "
            sSql = "Select * From AuditAssignmentSubTask_Master Left Join Content_Management_Master on AM_AuditAssignmentID=cmm_id Where AM_AuditAssignmentID=" & iAuditAssignmentId & " And AM_CompId=" & iAcID & ""
            If iStatus = 0 Then
                sSql = sSql & " And AM_DELFLG='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And AM_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And AM_DELFLG='W'" 'Waiting for approval
            End If
            If sSearchText <> "" Then
                sSql = sSql & " And AM_Name like '" & sSearchText & "%' " '
            End If
            sSql = sSql & " Order By AM_Name ASC"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                dr("DescID") = ds.Tables(0).Rows(i)("AM_ID")
                dr("cmm_desc") = ds.Tables(0).Rows(i)("cmm_desc")
                dr("DescName") = ds.Tables(0).Rows(i)("AM_Name")
                dr("Description") = ds.Tables(0).Rows(i)("AM_Desc")
                If ds.Tables(0).Rows(i)("AM_BillingTypeID") = 0 Then
                    dr("BillingType") = "Billable"
                ElseIf ds.Tables(0).Rows(i)("AM_BillingTypeID") = 1 Then
                    dr("BillingType") = "Non Billable"
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("AM_DELFLG")) = False Then
                    If ds.Tables(0).Rows(i)("AM_DELFLG") = "W" Then
                        dr("Status") = "Waiting for Approval"
                    ElseIf ds.Tables(0).Rows(i)("AM_DELFLG") = "D" Then
                        dr("Status") = "De-Activated"
                    ElseIf ds.Tables(0).Rows(i)("AM_DELFLG") = "A" Then
                        dr("Status") = "Activated"
                    End If
                End If
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditAssignmentName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditAssignmentID As Integer) As String
        Dim sSql As String
        Try
            sSql = "select CMM_Desc from Content_Management_Master where CMM_ID=" & iAuditAssignmentID & " And CMM_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllAuditAssignmentSTDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iAuditAssignmentID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AM_ID,AM_Name From AuditAssignmentSubTask_Master Where AM_AuditAssignmentID=" & iAuditAssignmentID & " And AM_CompId=" & iAcID & " And AM_DELFLG in ('A','W') Order By AM_Name ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditAssignmentSTDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iDescID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * From AuditAssignmentSubTask_Master Where AM_ID=" & iDescID & " and AM_CompId=" & iAcID & ""
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditAssignmentSTDeleteorNot(ByVal sAc As String, ByVal iAcID As Integer, ByVal sDesc As Object, ByVal iMasID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from AuditAssignmentSubTask_Master where AM_CompId=" & iAcID & " And AM_Name='" & sDesc & "'"
            If iMasID > 0 Then
                sSql = sSql & " And AM_ID=" & iMasID & " and AM_DELFLG='D'"
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAuditAssignmentSTExistingDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal iTaskID As Integer, ByVal sDesc As Object, ByVal sCoulmnName As String, ByVal iMasID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select * from AuditAssignmentSubTask_Master where AM_AuditAssignmentID=" & iTaskID & " And AM_CompId=" & iAcID & " And " & sCoulmnName & "='" & sDesc & "'"
            If iMasID > 0 Then
                sSql = sSql & " And AM_ID <> " & iMasID & ""
            End If
            CheckAuditAssignmentSTExistingDetails = objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAuditAssignmentSTStatus(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasId As Integer, ByVal iUserId As Integer, ByVal sIPAddress As String, ByVal sStatus As String)
        Dim sSql As String = ""
        Try
            sSql = "Update AuditAssignmentSubTask_Master Set AM_IPAddress='" & sIPAddress & "',"
            If sStatus = "W" Then
                sSql = sSql & " AM_DELFLG='A',AM_STATUS='A',AM_APPROVEDBY=" & iUserId & ",AM_APPROVEDON=GetDate()"
            ElseIf sStatus = "D" Then
                sSql = sSql & " AM_DELFLG='D',AM_STATUS='AD',AM_DELETEDBY=" & iUserId & ",AM_DELETEDON=GetDate()"
            ElseIf sStatus = "A" Then
                sSql = sSql & " AM_DELFLG='A',AM_STATUS='AR',AM_RECALLBY=" & iUserId & ",AM_RECALLON=GetDate()"
            End If
            sSql = sSql & " Where AM_ID=" & iMasId & " And AM_CompId=" & iAcID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveAssignmentSubTaskMasterDetails(ByVal sAC As String, ByVal objclsMaster As clsAdminMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CODE", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsMaster.sCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Name", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsMaster.sDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_AuditAssignmentID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iAuditAssignment
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsMaster.sRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_BillingTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iBillingType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsMaster.sStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_DELFLG", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsMaster.sDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsMaster.sIpAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsMaster.iCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAuditAssignmentSubTask_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditAssignmentSubTask(ByVal sAc As String, ByVal iAcID As Integer, ByVal iTaskID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AM_ID,AM_Name From AuditAssignmentSubTask_Master Where AM_CompId=" & iAcID & " And AM_AuditAssignmentID=" & iTaskID & " And AM_DELFLG='A' Order By AM_Name ASC"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadInvoiceTaxTypesDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal sTaxPercentage As String) As DataTable
        Dim sSql As String = ""
        Try
            If sType.Contains("IGST") = True Then
                sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='TM' And CMM_Desc not like '%IGST%' And CMM_Desc not like '%CGST%' And CMM_Desc not like '%SGST%' And CMM_Rate='" & sTaxPercentage & "' And CMM_CompID=" & iAcID & " And cmm_delflag='A' Order By CMM_Desc ASC"
            ElseIf sType.Contains("CGST") = True Then
                sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='TM' And CMM_Desc not like '%IGST%' And CMM_Desc not like '%CGST%' And CMM_Rate='" & sTaxPercentage & "' And CMM_CompID=" & iAcID & " And cmm_delflag='A' Order By CMM_Desc ASC"
            ElseIf sType.Contains("SGST") = True Then
                sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='TM' And CMM_Desc not like '%IGST%' And CMM_Desc not like '%SGST%' And CMM_Rate='" & sTaxPercentage & "' And CMM_CompID=" & iAcID & " And cmm_delflag='A' Order By CMM_Desc ASC"
            Else
                sSql = "Select CMM_ID AS PKID,CMM_Desc AS Name From Content_Management_Master Where CMM_Category='TM' And CMM_CompID=" & iAcID & " And cmm_delflag='A' Order By CMM_Desc ASC"
            End If
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

