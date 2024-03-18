Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAccountpolicies
	Private objDBL As New DatabaseLayer.DBHelper
	Dim objGen As New clsGRACeGeneral
	Private objclsGRACeGeneral As New clsGRACeGeneral
	Private objclsGeneralFunctions As New clsGeneralFunctions

	Private iRCM_Id As Integer
	Private iRCM_ReportId As Integer
	Private sRCM_ReportName As String
	Private sRCM_Heading As String
	Private sRCM_Description As String
	Private sRCM_Delflag As String
	Private sRCM_Status As String
	Private iRCM_CrBy As Integer
	Private dRCM_CrOn As DateTime
	Private iRCM_UpdatedBy As Integer
	Private dRCM_UpdatedOn As DateTime
	Private iRCM_DeletedBy As Integer
	Private dRCM_DeletedOn As DateTime
	Private iRCM_AppBy As Integer
	Private dRCM_AppOn As DateTime
	Private sRCM_IPAddress As String
	Private iRCM_CompID As Integer
	Private iRCM_Yearid As Integer
	Public Property RCM_Id() As Integer
		Get
			Return (iRCM_Id)
		End Get
		Set(ByVal Value As Integer)
			iRCM_Id = Value
		End Set
	End Property
	Public Property RCM_ReportId() As Integer
		Get
			Return (iRCM_ReportId)
		End Get
		Set(ByVal Value As Integer)
			iRCM_ReportId = Value
		End Set
	End Property
	Public Property RCM_ReportName() As String
		Get
			Return (sRCM_ReportName)
		End Get
		Set(ByVal Value As String)
			sRCM_ReportName = Value
		End Set
	End Property
	Public Property RCM_Heading() As String
		Get
			Return (sRCM_Heading)
		End Get
		Set(ByVal Value As String)
			sRCM_Heading = Value
		End Set
	End Property
	Public Property RCM_Description() As String
		Get
			Return (sRCM_Description)
		End Get
		Set(ByVal Value As String)
			sRCM_Description = Value
		End Set
	End Property
	Public Property RCM_Delflag() As String
		Get
			Return (sRCM_Delflag)
		End Get
		Set(ByVal Value As String)
			sRCM_Delflag = Value
		End Set
	End Property
	Public Property RCM_Status() As String
		Get
			Return (sRCM_Status)
		End Get
		Set(ByVal Value As String)
			sRCM_Status = Value
		End Set
	End Property
	Public Property RCM_CrBy() As Integer
		Get
			Return (iRCM_CrBy)
		End Get
		Set(ByVal Value As Integer)
			iRCM_CrBy = Value
		End Set
	End Property
	Public Property RCM_CrOn() As Date
		Get
			Return (dRCM_CrOn)
		End Get
		Set(ByVal Value As Date)
			dRCM_CrOn = Value
		End Set
	End Property
	Public Property RCM_UpdatedBy() As Integer
		Get
			Return (iRCM_UpdatedBy)
		End Get
		Set(ByVal Value As Integer)
			iRCM_UpdatedBy = Value
		End Set
	End Property
	Public Property RCM_UpdatedOn() As Date
		Get
			Return (dRCM_UpdatedOn)
		End Get
		Set(ByVal Value As Date)
			dRCM_UpdatedOn = Value
		End Set
	End Property
	Public Property RCM_DeletedBy() As Integer
		Get
			Return (iRCM_DeletedBy)
		End Get
		Set(ByVal Value As Integer)
			iRCM_DeletedBy = Value
		End Set
	End Property
	Public Property RCM_DeletedOn() As Date
		Get
			Return (dRCM_DeletedOn)
		End Get
		Set(ByVal Value As Date)
			dRCM_DeletedOn = Value
		End Set
	End Property
	Public Property RCM_AppBy() As Integer
		Get
			Return (iRCM_AppBy)
		End Get
		Set(ByVal Value As Integer)
			iRCM_AppBy = Value
		End Set
	End Property
	Public Property RCM_AppOn() As Date
		Get
			Return (dRCM_AppOn)
		End Get
		Set(ByVal Value As Date)
			dRCM_AppOn = Value
		End Set
	End Property
	Public Property RCM_IPAddress() As String
		Get
			Return (sRCM_IPAddress)
		End Get
		Set(ByVal Value As String)
			sRCM_IPAddress = Value
		End Set
	End Property
	Public Property RCM_CompID() As Integer
		Get
			Return (iRCM_CompID)
		End Get
		Set(ByVal Value As Integer)
			iRCM_CompID = Value
		End Set
	End Property
	Public Property RCM_Yearid() As Integer
		Get
			Return (iRCM_Yearid)
		End Get
		Set(ByVal Value As Integer)
			iRCM_Yearid = Value
		End Set
	End Property
	Public Function SaveReportContentMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal objAccountpolicies As clsAccountpolicies) As Array
		Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
		Dim iParamCount As Integer
		Dim Arr(1) As String
		Try
			iParamCount = 0
			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Id", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_Id
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_ReportId", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_ReportId
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_ReportName", OleDb.OleDbType.VarChar, 500)
			ObjParam(iParamCount).Value = objAccountpolicies.sRCM_ReportName
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Heading", OleDb.OleDbType.VarChar, 2000)
			ObjParam(iParamCount).Value = objAccountpolicies.RCM_Heading
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Description", OleDb.OleDbType.VarChar, 5000)
			ObjParam(iParamCount).Value = objAccountpolicies.sRCM_Description
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Delflag", OleDb.OleDbType.VarChar, 1)
			ObjParam(iParamCount).Value = objAccountpolicies.sRCM_Delflag
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Status", OleDb.OleDbType.VarChar, 1)
			ObjParam(iParamCount).Value = objAccountpolicies.sRCM_Status
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CrBy", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_CrBy
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CrOn", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAccountpolicies.dRCM_CrOn
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_UpdatedBy", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_UpdatedBy
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_UpdatedOn", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAccountpolicies.dRCM_UpdatedOn
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			'ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_DeletedBy", OleDb.OleDbType.Integer, 4)
			'ObjParam(iParamCount).Value = objAccountpolicies.iRCM_DeletedBy
			'ObjParam(iParamCount).Direction = ParameterDirection.Input
			'iParamCount += 1

			'ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_DeletedOn", OleDb.OleDbType.Date)
			'ObjParam(iParamCount).Value = objAccountpolicies.dRCM_DeletedOn
			'ObjParam(iParamCount).Direction = ParameterDirection.Input
			'iParamCount += 1

			'ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_AppBy", OleDb.OleDbType.Integer, 4)
			'ObjParam(iParamCount).Value = objAccountpolicies.iRCM_AppBy
			'ObjParam(iParamCount).Direction = ParameterDirection.Input
			'iParamCount += 1

			'ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_AppOn", OleDb.OleDbType.Date)
			'ObjParam(iParamCount).Value = objAccountpolicies.dRCM_AppOn
			'ObjParam(iParamCount).Direction = ParameterDirection.Input
			'iParamCount += 1

			'ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_AppOn", OleDb.OleDbType.Date)
			'ObjParam(iParamCount).Value = objAccountpolicies.dRCM_AppOn
			'ObjParam(iParamCount).Direction = ParameterDirection.Input
			'iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_IPAddress", OleDb.OleDbType.VarChar, 25)
			ObjParam(iParamCount).Value = objAccountpolicies.sRCM_IPAddress
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CompID", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_CompID
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Yearid", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAccountpolicies.iRCM_Yearid
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
			ObjParam(iParamCount).Direction = ParameterDirection.Output
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
			ObjParam(iParamCount).Direction = ParameterDirection.Output
			Arr(0) = "@iUpdateOrSave"
			Arr(1) = "@iOper"

			Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_ReportContentMaster", 1, Arr, ObjParam)
			Return Arr
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function CheckReportHeadingExisting(ByVal sAc As String, ByVal iAcID As Integer, ByVal iReportType As Integer, iPKID As Integer, ByVal sDesc As String) As Boolean
		Dim sSql As String
		Try
			sSql = "Select * from SAD_ReportContentMaster where RCM_ReportId=" & iReportType & " And RCM_Heading='" & sDesc & "' And RCM_CompID=" & iAcID & ""
			If iPKID > 0 Then
				sSql = sSql & " And RCM_Id <> " & iPKID & ""
			End If
			CheckReportHeadingExisting = objDBL.SQLCheckForRecord(sAc, sSql)
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function BinALLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Reportid As Integer) As DataTable
		Try
			Dim sSql As String
			Dim dtDetails As New DataTable
			Dim dt As New DataTable
			Dim dsDetails As New DataSet
			Dim drow As DataRow
			Try
				dtDetails.Columns.Add("SrNo")
				dtDetails.Columns.Add("PKID")
				dtDetails.Columns.Add("ReportType")
				dtDetails.Columns.Add("Heading")
				dtDetails.Columns.Add("ReportID")
				dtDetails.Columns.Add("Description")

				sSql = "select * from SAD_ReportContentMaster Where RCM_ReportId=" & Reportid & " And RCM_CompID=" & iCompID & ""
				dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

				For i = 0 To dt.Rows.Count - 1
					drow = dtDetails.NewRow
					drow("SrNo") = i + 1
					drow("PKID") = dt.Rows(i)("RCM_Id")
					drow("ReportType") = dt.Rows(i)("RCM_ReportName")
					drow("Heading") = dt.Rows(i)("RCM_Heading")
					drow("ReportID") = dt.Rows(i)("RCM_ReportId")
					drow("Description") = dt.Rows(i)("RCM_Description")

					dtDetails.Rows.Add(drow)
				Next

				Return dtDetails
			Catch ex As Exception
				MsgBox(ex.Message, MsgBoxStyle.Information)
				Throw
			End Try
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function BinALLDetailsExisting(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal Reportid As Integer, ByVal iCustid As Integer, ByVal iBranchid As Integer, ByVal iYearid As Integer) As DataTable
		Try
			Dim sSql As String
			Dim dtDetails As New DataTable
			Dim dt As New DataTable
			Dim dsDetails As New DataSet
			Dim drow As DataRow
			Try
				dtDetails.Columns.Add("SrNo")
				dtDetails.Columns.Add("PKID")
				dtDetails.Columns.Add("ReportType")
				dtDetails.Columns.Add("Heading")
				dtDetails.Columns.Add("ReportID")
				dtDetails.Columns.Add("Description")

				sSql = "select * from Acc_Account_policies Where ACP_Rpttype=" & Reportid & " And ACP_Compid=" & iCompID & " and ACP_Custid=" & iCustid & " And ACP_Branchid=" & iBranchid & "  And ACP_Yearid= " & iYearid & ""
				dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

				For i = 0 To dt.Rows.Count - 1
					drow = dtDetails.NewRow
					drow("SrNo") = i + 1
					drow("PKID") = dt.Rows(i)("ACF_pkid")
					drow("ReportType") = dt.Rows(i)("ACP_Rpttypename")
					drow("Heading") = dt.Rows(i)("")
					drow("ReportID") = dt.Rows(i)("RCM_ReportId")
					drow("Description") = dt.Rows(i)("RCM_Description")
					dtDetails.Rows.Add(drow)
				Next

				Return dtDetails
			Catch ex As Exception
				MsgBox(ex.Message, MsgBoxStyle.Information)
				Throw
			End Try
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function LoadReportContentToGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iId As Integer) As DataTable
		Dim dtRes As New DataTable, dt As New DataTable
		Dim dRow As DataRow
		Dim sSql As String
		Dim i As Integer
		Try

			dtRes.Columns.Add("Description")
			sSql = "Select RCM_Id,RCM_ReportId,RCM_ReportName,RCM_Heading,RCM_Description From SAD_ReportContentMaster Where RCM_CompID=" & iACID & " And RCM_ReportId=0"
			If iId > 0 Then
				sSql = sSql & " Or RCM_ReportId = " & iId & ""
			End If
			sSql = sSql & " Order By RCM_ReportName  "
			dt = objDBL.SQLExecuteDataTable(sAC, sSql)
			If dt.Rows.Count > 0 Then
				For i = 0 To dt.Rows.Count - 1
					dRow = dtRes.NewRow
					dRow("Description") = "<b>" & objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RCM_Heading")) & "</b>"
					dtRes.Rows.Add(dRow)
					dRow = dtRes.NewRow
					dRow("Description") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RCM_Description"))
					dtRes.Rows.Add(dRow)
					dRow = dtRes.NewRow
					dtRes.Rows.Add(dRow)
				Next
			End If
			Return dtRes
		Catch ex As Exception
			Throw
		End Try
	End Function
End Class
