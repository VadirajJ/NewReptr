Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data

Public Class clsRiskMaster
    Private objDBL As New DatabaseLayer.DBHelper

    Public iRAM_PKID As Integer
    Public sRAM_Code As String
    Public sRAM_Category As String
    Public sRAM_Name As String
    Public sRAM_Remarks As String
    Public iRAM_CrBy As Integer
    Public iRAM_UpdatedBy As Integer
    Public sRAM_IPAddressas As String
    Public iRAM_CompID As Integer
    Public iRAM_Score As Integer
    Public dRAM_StartValue As Double
    Public dRAM_EndValue As Double
    Public sRAM_Color As String
    Public iRAM_YearID As Integer
    Public Property iRAMYearID() As Integer
        Get
            Return (iRAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iRAM_YearID = Value
        End Set
    End Property
    Public Property dRAMStartValue() As Double
        Get
            Return (dRAM_StartValue)
        End Get
        Set(ByVal Value As Double)
            dRAM_StartValue = Value
        End Set
    End Property
    Public Property dRAMEndValue() As Double
        Get
            Return (dRAM_EndValue)
        End Get
        Set(ByVal Value As Double)
            dRAM_EndValue = Value
        End Set
    End Property
    Public Property sRAMColor() As String
        Get
            Return (sRAM_Color)
        End Get
        Set(ByVal Value As String)
            sRAM_Color = Value
        End Set
    End Property
    Public Property iRAMPKID() As Integer
        Get
            Return (iRAM_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRAM_PKID = Value
        End Set
    End Property
    Public Property iRAMScore() As Integer
        Get
            Return (iRAM_Score)
        End Get
        Set(ByVal Value As Integer)
            iRAM_Score = Value
        End Set
    End Property
    Public Property sRAMCategory() As String
        Get
            Return (sRAM_Category)
        End Get
        Set(ByVal Value As String)
            sRAM_Category = Value
        End Set
    End Property
    Public Property sRAMCode() As String
        Get
            Return (sRAM_Code)
        End Get
        Set(ByVal Value As String)
            sRAM_Code = Value
        End Set
    End Property
    Public Property sRAMName() As String
        Get
            Return (sRAM_Name)
        End Get
        Set(ByVal Value As String)
            sRAM_Name = Value
        End Set
    End Property
    Public Property sRAMRemarks() As String
        Get
            Return (sRAM_Remarks)
        End Get
        Set(ByVal Value As String)
            sRAM_Remarks = Value
        End Set
    End Property
    Public Property iRAMCrBy() As Integer
        Get
            Return (iRAM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iRAM_CrBy = Value
        End Set
    End Property
    Public Property iRAMUpdatedBy() As Integer
        Get
            Return (iRAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iRAM_UpdatedBy = Value
        End Set
    End Property
    Public Property sRAMIPAddressas() As String
        Get
            Return (sRAM_IPAddressas)
        End Get
        Set(ByVal Value As String)
            sRAM_IPAddressas = Value
        End Set
    End Property
    Public Property iRAMCompID() As Integer
        Get
            Return (iRAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iRAM_CompID = Value
        End Set
    End Property
    Public Function LoadMasterTypeScoreWise(ByVal sAc As String, ByVal iAcID As Integer, ByVal SMasterID As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name From Risk_GeneralMaster Where RAM_Category='" & SMasterID & "' And RAM_YearID=" & iYearID & " and RAM_CompID=" & iAcID & " Order By RAM_Name"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMasterTypeNameWise(ByVal sAc As String, ByVal iAcID As Integer, ByVal SMasterID As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name From Risk_GeneralMaster Where RAM_Category='" & SMasterID & "' And RAM_YearID=" & iYearID & " and RAM_CompID=" & iAcID & " Order By RAM_Name"
            Return objDBL.SQLExecuteDataTable(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sAc As String, ByVal iAcID As Integer, ByVal iRAMID As Integer) As DataSet
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name,RAM_Remarks,RAM_Code,RAM_DelFlag,RAM_Score,RAM_StartValue,RAM_EndValue,RAM_Color From Risk_GeneralMaster Where RAM_PKID=" & iRAMID & " And RAM_CompID=" & iAcID & " Order By RAM_Name ASC "
            Return objDBL.SQLExecuteDataSet(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskTypeNameExist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal iID As Integer, ByVal sCategory As String, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID from Risk_GeneralMaster where RAM_Name='" & sName & "' And RAM_YearID=" & iYearID & " and RAm_Category='" & sCategory & "' And RAM_CompID =" & iAcID & ""
            If iID > 0 Then
                sSql = sSql & " And RAM_PKID <>" & iID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskScoreExist(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal iID As Integer, ByVal sCategory As String, ByVal iScore As Integer, ByVal sName As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select RAM_Score from Risk_GeneralMaster where RAm_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " and RAM_CompID =" & iAcID & ""
            If iScore > 0 Then
                sSql = sSql & " And RAM_Score =" & iScore & " "
            End If
            If iID > 0 Then
                sSql = sSql & " And RAM_PKID <>" & iID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskScoreCount(ByVal sAc As String, ByVal iAcID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal iID As Integer) As Boolean
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "Select count(RAM_PKID) from Risk_GeneralMaster where  RAM_YearID=" & iYearID & " and RAM_Category='" & sCategory & "' And RAM_CompID =" & iAcID & " and RAM_PKID<>" & iID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAc, sSql)
            If iCount >= 15 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ActivateRiskMaster(ByVal sAc As String, ByVal iAcID As Integer, ByVal iID As Integer, ByVal iUserID As Integer, ByVal sStatus As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Risk_GeneralMaster Set "
            If sStatus = "W" Then
                sSql = sSql & "RAM_DelFlag='A',RAM_STATUS='A',RAM_ApprovedBy=" & iUserID & ",RAM_ApprovedOn=Getdate(),"
            ElseIf sStatus = "D" Then
                sSql = sSql & "RAM_DelFlag='D',RAM_STATUS='AD',RAM_DeletedBy=" & iUserID & ",RAM_DeletedOn=Getdate(),"
            ElseIf sStatus = "A" Then
                sSql = sSql & "RAM_DelFlag='A',RAM_STATUS='AR',RAM_RecallBY=" & iUserID & ",RAM_RecallON=Getdate(),"
            End If
            sSql = sSql & " RAM_IPAddress='" & sIPAddress & "' where RAM_CompID=" & iAcID & " And RAM_PKID = " & iID & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadAuditUniverseMasterGrid(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SlNo")
            dtTab.Columns.Add("PKID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Status")

            sSql = "Select RAM_PKID,RAM_Name,RAM_Code,RAM_DelFlag from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " And RAM_CompID= " & iAcID & " And RAM_Category='" & sType & "' order by RAM_Name"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SlNo") = i + 1
                dr("PKID") = dt.Rows(i)("RAM_PKID")
                dr("Name") = dt.Rows(i)("RAM_Name")
                dr("Code") = dt.Rows(i)("RAM_Code")
                If dt.Rows(i)("RAM_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Risk Type and Checks Category
    Public Function LoadRiskTypeGridRTCC(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("PKID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Status")

            sSql = "Select RAM_PKID,RAM_Name,RAM_Code,RAM_DelFlag,RAM_Score,RAM_StartValue,RAM_EndValue,RAM_Color from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " And RAM_CompID= " & iAcID & ""
            If sType <> "" Then
                sSql = sSql & " And RAM_Category='" & sType & "'"
            End If
            If iStatus = 0 Then
                sSql = sSql & " And RAM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And RAM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And RAM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " order by RAM_Name"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("PKID") = dt.Rows(i)("RAM_PKID")
                dr("Name") = dt.Rows(i)("RAM_Name")
                dr("Code") = dt.Rows(i)("RAM_Code")
                If dt.Rows(i)("RAM_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Risk Impact,Risk Likelihood,Design Effectiveness Scores and Operational Effectiveness scores
    Public Function LoadRiskTypeGridRILIDESOES(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("PKID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Score")
            dtTab.Columns.Add("Status")

            sSql = "Select RAM_PKID,RAM_Name,RAM_Code,RAM_DelFlag,RAM_Score,RAM_StartValue,RAM_EndValue,RAM_Color from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " And RAM_CompID= " & iAcID & ""
            If sType <> "" Then
                sSql = sSql & " And RAM_Category='" & sType & "'"
            End If
            If iStatus = 0 Then
                sSql = sSql & " And RAM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And RAM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And RAM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " order by RAM_Score"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("PKID") = dt.Rows(i)("RAM_PKID")
                dr("Name") = dt.Rows(i)("RAM_Name")
                dr("Code") = dt.Rows(i)("RAM_Code")
                If IsDBNull(dt.Rows(i)("RAM_Score")) = False Then
                    dr("Score") = dt.Rows(i)("RAM_Score")
                    If dr("Score") = 0 Then
                        dr("Score") = ""
                    End If
                Else
                    dr("Score") = ""
                End If
                If dt.Rows(i)("RAM_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Gross Risk Score,Gross Control Score and Residual Risk Score
    Public Function LoadRiskTypeGridGRSGCSRRS(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iYearID As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("PKID")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Score")
            dtTab.Columns.Add("Startvalue")
            dtTab.Columns.Add("Endvalue")
            dtTab.Columns.Add("Color")
            dtTab.Columns.Add("Status")

            sSql = "Select RAM_PKID,RAM_Name,RAM_Code,RAM_DelFlag,RAM_Score,RAM_StartValue,RAM_EndValue,RAM_Color from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " And RAM_CompID= " & iAcID & ""
            If sType <> "" Then
                sSql = sSql & " And RAM_Category='" & sType & "'"
            End If
            If iStatus = 0 Then
                sSql = sSql & " And RAM_DelFlag ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " And RAM_DelFlag='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " And RAM_DelFlag='W'" 'Waiting for approval
            End If
            sSql = sSql & " order by RAM_Score"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("PKID") = dt.Rows(i)("RAM_PKID")
                dr("Name") = dt.Rows(i)("RAM_Name")
                dr("Code") = dt.Rows(i)("RAM_Code")
                dr("Color") = dt.Rows(i)("RAM_Color")
                dr("Startvalue") = dt.Rows(i)("RAM_StartValue")
                dr("Endvalue") = dt.Rows(i)("RAM_EndValue")
                dr("Score") = dt.Rows(i)("RAM_Score")
                If dt.Rows(i)("RAM_DelFlag") = "A" Then
                    dr("Status") = "Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                    dr("Status") = "De-Activated"
                ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                    dr("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditUniverseMaster(ByVal sAc As String, ByVal iAcID As Integer, ByVal objclsRiskMaster As clsRiskMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Category", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Name", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMIPAddressas
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAc, "spAuditUniverse_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveRiskMaster(ByVal sAc As String, ByVal iAcID As Integer, ByVal objclsRiskMaster As clsRiskMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Code", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Category", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Name", OleDb.OleDbType.VarChar, 200)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Score", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAM_Score
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_StartValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsRiskMaster.dRAMStartValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_EndValue", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsRiskMaster.dRAMEndValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Color", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAM_Color
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsRiskMaster.sRAMIPAddressas
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsRiskMaster.iRAMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAc, "spRisk_GeneralMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditUniverseMasterReport(ByVal sAc As String, ByVal iAcID As Integer, ByVal sType As String, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("Master")
            dtTab.Columns.Add("Name")
            dtTab.Columns.Add("Code")
            dtTab.Columns.Add("Status")

            sSql = "Select * from Risk_GeneralMaster Where RAM_YearID=" & iYearID & " And RAM_CompID= " & iAcID & " "
            If sType <> "" Then
                sSql = sSql & "  And RAM_Category='" & sType & "'"
            Else
                sSql = sSql & "  And RAM_Category in ('RT','CC')"
            End If
            sSql = sSql & " order by RAM_Name"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            If sType = "" Or sType = "RT" Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    If dt.Rows(i)("RAM_Category") = "RT" Then
                        dr("Master") = "Risk Type"
                        dr("Name") = dt.Rows(i)("RAM_Name")
                        dr("Code") = dt.Rows(i)("RAM_Code")
                        If dt.Rows(i)("RAM_DelFlag") = "A" Then
                            dr("Status") = "Activated"
                        ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                            dr("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                            dr("Status") = "Waiting for Approval"
                        End If
                        dtTab.Rows.Add(dr)
                    End If
                Next
                dr = dtTab.NewRow()
                dtTab.Rows.Add(dr)
            End If

            If sType = "" Or sType = "CC" Then
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    If dt.Rows(i)("RAM_Category") = "CC" Then
                        dr("Master") = "Checks Category"
                        dr("Name") = dt.Rows(i)("RAM_Name")
                        dr("Code") = dt.Rows(i)("RAM_Code")
                        If dt.Rows(i)("RAM_DelFlag") = "A" Then
                            dr("Status") = "Activated"
                        ElseIf dt.Rows(i)("RAM_DelFlag") = "D" Then
                            dr("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("RAM_DelFlag") = "W" Then
                            dr("Status") = "Waiting for Approval"
                        End If
                        dtTab.Rows.Add(dr)
                    End If
                Next
                dr = dtTab.NewRow()
                dtTab.Rows.Add(dr)
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class


