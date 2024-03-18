Imports System
Imports System.Data
Imports DatabaseLayer
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Security.Cryptography
Public Class ClsAssetDeletion
	Private objDBL As New DatabaseLayer.DBHelper
	Dim objGen As New clsGRACeGeneral
	Dim objGenFun As New clsGeneralFunctions

	Dim AFAD_ID As Integer
	Dim AFAD_CustomerName As Integer
	Dim AFAD_TransNo As String
	Dim AFAD_Location As Integer
	Dim AFAD_Division As Integer
	Dim AFAD_Department As Integer
	Dim AFAD_Bay As Integer
	Dim AFAD_AssetClass As Integer
	Dim AFAD_Asset As Integer
	Dim AFAD_AssetDeletion As Integer
	Dim AFAD_AssetDeletionType As Integer
	Dim AFAD_DeletionDate As DateTime
	Dim AFAD_Amount As Double
	Dim AFAD_Quantity As Integer
	Dim AFAD_Paymenttype As Integer
	Dim AFAD_CostofTransport As Double
	Dim AFAD_InstallationCost As Double
	Dim AFAD_DateofInitiate As DateTime
	Dim AFAD_DateofReceived As DateTime
	Dim AFAD_ToLocation As Integer
	Dim AFAD_ToDivision As Integer
	Dim AFAD_ToDepartment As Integer
	Dim AFAD_ToBay As Integer
	Dim AFAD_AssetDelDesc As String
	Dim AFAD_PorLStatus As String
	Dim AFAD_PorLAmount As Double
	Dim AFAD_CreatedBy As Integer
	Dim AFAD_CreatedOn As DateTime
	Dim AFAD_ApprovedBy As Integer
	Dim AFAD_ApprovedOn As DateTime
	Dim AFAD_Status As String
	Dim AFAD_Delflag As String
	Dim AFAD_YearID As Integer
	Dim AFAD_CompID As Integer
	Dim AFAD_Deletedby As Integer
	Dim AFAD_DeletedOn As DateTime
	Dim AFAD_IPAddress As String
	Dim AFAD_SalesPrice As Double
	Dim AFAD_DelDeprec As Double
	Dim AFAD_WDVValue As Double

	Dim AFAD_ContAssetValue As Double
	Dim AFAD_ContDep As Double
	Dim AFAD_ContWDV As Double

	Dim AFAD_InsClaimedNo As String
	Dim AFAD_InsAmtClaimed As Double
	Dim AFAD_InsClaimedDate As DateTime
	Dim AFAD_InsAmtRecvd As Double
	Dim AFAD_InsRefNo As String
	Dim AFAD_InsRefDate As DateTime
	Dim AFAD_Remarks As String
	Public Property iAFAD_ID() As Integer
		Get
			Return (AFAD_ID)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ID = Value
		End Set
	End Property
	Public Property iAFAD_CustomerName() As Integer
		Get
			Return (AFAD_CustomerName)
		End Get
		Set(ByVal Value As Integer)
			AFAD_CustomerName = Value
		End Set
	End Property
	Public Property sAFAD_TransNo() As String
		Get
			Return (AFAD_TransNo)
		End Get
		Set(ByVal Value As String)
			AFAD_TransNo = Value
		End Set
	End Property
	Public Property iAFAD_Location() As Integer
		Get
			Return (AFAD_Location)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Location = Value
		End Set
	End Property
	Public Property iAFAD_Division() As Integer
		Get
			Return (AFAD_Division)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Division = Value
		End Set
	End Property
	Public Property iAFAD_Department() As Integer
		Get
			Return (AFAD_Department)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Department = Value
		End Set
	End Property
	Public Property iAFAD_Bay() As Integer
		Get
			Return (AFAD_Bay)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Bay = Value
		End Set
	End Property
	Public Property iAFAD_AssetClass() As Integer
		Get
			Return (AFAD_AssetClass)
		End Get
		Set(ByVal Value As Integer)
			AFAD_AssetClass = Value
		End Set
	End Property
	Public Property iAFAD_Asset() As Integer
		Get
			Return (AFAD_Asset)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Asset = Value
		End Set
	End Property
	Public Property iAFAD_AssetDeletion() As Integer
		Get
			Return (AFAD_AssetDeletion)
		End Get
		Set(ByVal Value As Integer)
			AFAD_AssetDeletion = Value
		End Set
	End Property
	Public Property iAFAD_AssetDeletionType() As Integer
		Get
			Return (AFAD_AssetDeletionType)
		End Get
		Set(ByVal Value As Integer)
			AFAD_AssetDeletionType = Value
		End Set
	End Property
	Public Property dAFAD_DeletionDate() As Date
		Get
			Return (AFAD_DeletionDate)
		End Get
		Set(ByVal Value As Date)
			AFAD_DeletionDate = Value
		End Set
	End Property
	Public Property dAFAD_Amount() As Double
		Get
			Return (AFAD_Amount)
		End Get
		Set(ByVal Value As Double)
			AFAD_Amount = Value
		End Set
	End Property
	Public Property iAFAD_Quantity() As Integer
		Get
			Return (AFAD_Quantity)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Quantity = Value
		End Set
	End Property
	Public Property iAFAD_Paymenttype() As Integer
		Get
			Return (AFAD_Paymenttype)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Paymenttype = Value
		End Set
	End Property
	Public Property dAFAD_CostofTransport() As Double
		Get
			Return (AFAD_CostofTransport)
		End Get
		Set(ByVal Value As Double)
			AFAD_CostofTransport = Value
		End Set
	End Property
	Public Property dAFAD_InstallationCost() As Double
		Get
			Return (AFAD_InstallationCost)
		End Get
		Set(ByVal Value As Double)
			AFAD_InstallationCost = Value
		End Set
	End Property
	Public Property dAFAD_DateofInitiate() As Date
		Get
			Return (AFAD_DateofInitiate)
		End Get
		Set(ByVal Value As Date)
			AFAD_DateofInitiate = Value
		End Set
	End Property
	Public Property dAFAD_DateofReceived() As Date
		Get
			Return (AFAD_DateofReceived)
		End Get
		Set(ByVal Value As Date)
			AFAD_DateofReceived = Value
		End Set
	End Property
	Public Property iAFAD_ToLocation() As Integer
		Get
			Return (AFAD_ToLocation)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ToLocation = Value
		End Set
	End Property
	Public Property iAFAD_ToDivision() As Integer
		Get
			Return (AFAD_ToDivision)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ToDivision = Value
		End Set
	End Property
	Public Property iAFAD_ToDepartment() As Integer
		Get
			Return (AFAD_ToDepartment)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ToDepartment = Value
		End Set
	End Property
	Public Property iAFAD_ToBay() As Integer
		Get
			Return (AFAD_ToBay)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ToBay = Value
		End Set
	End Property
	Public Property sAFAD_AssetDelDesc() As String
		Get
			Return (AFAD_AssetDelDesc)
		End Get
		Set(ByVal Value As String)
			AFAD_AssetDelDesc = Value
		End Set
	End Property
	Public Property sAFAD_PorLStatus() As String
		Get
			Return (AFAD_PorLStatus)
		End Get
		Set(ByVal Value As String)
			AFAD_PorLStatus = Value
		End Set
	End Property
	Public Property dAFAD_PorLAmount() As Double
		Get
			Return (AFAD_PorLAmount)
		End Get
		Set(ByVal Value As Double)
			AFAD_PorLAmount = Value
		End Set
	End Property
	Public Property dAFAD_SalesPrice() As Double
		Get
			Return (AFAD_SalesPrice)
		End Get
		Set(ByVal Value As Double)
			AFAD_SalesPrice = Value
		End Set
	End Property
	Public Property dAFAD_DelDeprec() As Double
		Get
			Return (AFAD_DelDeprec)
		End Get
		Set(ByVal Value As Double)
			AFAD_DelDeprec = Value
		End Set
	End Property
	Public Property dAFAD_WDVValue() As Double
		Get
			Return (AFAD_WDVValue)
		End Get
		Set(ByVal Value As Double)
			AFAD_WDVValue = Value
		End Set
	End Property
	Public Property dAFAD_ContAssetValue() As Double
		Get
			Return (AFAD_ContAssetValue)
		End Get
		Set(ByVal Value As Double)
			AFAD_ContAssetValue = Value
		End Set
	End Property
	Public Property dAFAD_ContDep() As Double
		Get
			Return (AFAD_ContDep)
		End Get
		Set(ByVal Value As Double)
			AFAD_ContDep = Value
		End Set
	End Property
	Public Property dAFAD_ContWDV() As Double
		Get
			Return (AFAD_ContWDV)
		End Get
		Set(ByVal Value As Double)
			AFAD_ContWDV = Value
		End Set
	End Property
	Public Property sAFAD_InsClaimedNo() As String
		Get
			Return (AFAD_InsClaimedNo)
		End Get
		Set(ByVal Value As String)
			AFAD_InsClaimedNo = Value
		End Set
	End Property
	Public Property dAFAD_InsAmtClaimed() As Double
		Get
			Return (AFAD_InsAmtClaimed)
		End Get
		Set(ByVal Value As Double)
			AFAD_InsAmtClaimed = Value
		End Set
	End Property
	Public Property dAFAD_InsClaimedDate() As Date
		Get
			Return (AFAD_InsClaimedDate)
		End Get
		Set(ByVal Value As Date)
			AFAD_InsClaimedDate = Value
		End Set
	End Property
	Public Property dAFAD_InsAmtRecvd() As Double
		Get
			Return (AFAD_InsAmtRecvd)
		End Get
		Set(ByVal Value As Double)
			AFAD_InsAmtRecvd = Value
		End Set
	End Property
	Public Property sAFAD_InsRefNo() As String
		Get
			Return (AFAD_InsRefNo)
		End Get
		Set(ByVal Value As String)
			AFAD_InsRefNo = Value
		End Set
	End Property
	Public Property dAFAD_InsRefDate() As Date
		Get
			Return (AFAD_InsRefDate)
		End Get
		Set(ByVal Value As Date)
			AFAD_InsRefDate = Value
		End Set
	End Property
	Public Property sAFAD_Remarks() As String
		Get
			Return (AFAD_Remarks)
		End Get
		Set(ByVal Value As String)
			AFAD_Remarks = Value
		End Set
	End Property

	Public Property iAFAD_CreatedBy() As Integer
		Get
			Return (AFAD_CreatedBy)
		End Get
		Set(ByVal Value As Integer)
			AFAD_CreatedBy = Value
		End Set
	End Property
	Public Property dAFAD_CreatedOn() As Date
		Get
			Return (AFAD_CreatedOn)
		End Get
		Set(ByVal Value As Date)
			AFAD_CreatedOn = Value
		End Set
	End Property
	Public Property iAFAD_ApprovedBy() As Integer
		Get
			Return (AFAD_ApprovedBy)
		End Get
		Set(ByVal Value As Integer)
			AFAD_ApprovedBy = Value
		End Set
	End Property
	Public Property dAFAD_ApprovedOn() As Date
		Get
			Return (AFAD_ApprovedOn)
		End Get
		Set(ByVal Value As Date)
			AFAD_ApprovedOn = Value
		End Set
	End Property
	Public Property sAFAD_Status() As String
		Get
			Return (AFAD_Status)
		End Get
		Set(ByVal Value As String)
			AFAD_Status = Value
		End Set
	End Property
	Public Property sAFAD_Delflag() As String
		Get
			Return (AFAD_Delflag)
		End Get
		Set(ByVal Value As String)
			AFAD_Delflag = Value
		End Set
	End Property
	Public Property iAFAD_YearID() As Integer
		Get
			Return (AFAD_YearID)
		End Get
		Set(ByVal Value As Integer)
			AFAD_YearID = Value
		End Set
	End Property
	Public Property iAFAD_CompID() As Integer
		Get
			Return (AFAD_CompID)
		End Get
		Set(ByVal Value As Integer)
			AFAD_CompID = Value
		End Set
	End Property
	Public Property iAFAD_Deletedby() As Integer
		Get
			Return (AFAD_Deletedby)
		End Get
		Set(ByVal Value As Integer)
			AFAD_Deletedby = Value
		End Set
	End Property
	Public Property dAFAD_DeletedOn() As Date
		Get
			Return (AFAD_DeletedOn)
		End Get
		Set(ByVal Value As Date)
			AFAD_DeletedOn = Value
		End Set
	End Property
	Public Property sAFAD_IPAddress() As String
		Get
			Return (AFAD_IPAddress)
		End Get
		Set(ByVal Value As String)
			AFAD_IPAddress = Value
		End Set
	End Property
	Public Function LoadAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal assettype As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal idivisionId As Integer, ByVal idepartmentId As Integer, ByVal ibayId As Integer) As DataTable
		Dim sSql As String = ""
		Try
			sSql = "" : sSql = "Select AFAM_ID,AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_CompID=" & iCompID & " and AFAM_AssetType=" & assettype & " And AFAM_CustId = " & iCustId & ""
			If iLocationId <> 0 Then
				sSql = sSql & " And AFAM_Location = " & iLocationId & ""
			End If
			If idivisionId <> 0 Then
				sSql = sSql & " And AFAM_Division = " & idivisionId & ""
			End If
			If idepartmentId <> 0 Then
				sSql = sSql & " And AFAM_Department = " & idepartmentId & ""
			End If
			If ibayId <> 0 Then
				sSql = sSql & " And AFAM_Bay = " & ibayId & ""
			End If
			Return objDBL.SQLExecuteDataTable(sNameSpace, sSql)
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function showDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAFAA_ID As Integer) As DataTable
		Dim dt As New DataTable
		Dim sSql As String = ""
		Try
			sSql = "select * from Acc_FixedAssetDeletion Where AFAD_ID=" & iAFAA_ID & " And AFAD_CompID=" & iCompID & ""
			dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
			Return dt
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function SaveFixedAssetDeletion(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objAsstDelTrn As ClsAssetDeletion) As Array
		Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(51) {}
		Dim iParamCount As Integer
		Dim Arr(1) As String
		Try
			iParamCount = 0
			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ID", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ID
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_CustomerName", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_CustomerName
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1
			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_TransNo", OleDb.OleDbType.VarChar, 500)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_TransNo
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Location", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Location
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Division", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Division
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Department", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Department
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Bay", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Bay
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_AssetClass", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_AssetClass
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Asset", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Asset
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_AssetDeletion", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_AssetDeletion
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_AssetDeletionType", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_AssetDeletionType
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_DeletionDate", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_DeletionDate
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Amount", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Amount
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Quantity", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Quantity
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Paymenttype", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Paymenttype
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_CostofTransport", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_CostofTransport
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InstallationCost", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InstallationCost
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_DateofInitiate", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_DateofInitiate
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_DateofReceived", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_DateofReceived
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ToLocation", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ToLocation
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ToDivision", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ToDivision
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ToDepartment", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ToDepartment
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ToBay", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ToBay
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_AssetDelDesc", OleDb.OleDbType.VarChar, 500)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_AssetDelDesc
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_PorLStatus", OleDb.OleDbType.VarChar, 50)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_PorLStatus
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_PorLAmount", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_PorLAmount
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_SalesPrice", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_SalesPrice
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_DelDeprec", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_DelDeprec
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_WDVValue", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_WDVValue
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ContAssetValue", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ContAssetValue
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ContDep", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ContDep
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ContWDV", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ContWDV
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsClaimedNo", OleDb.OleDbType.VarChar)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsClaimedNo
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsAmtClaimed", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsAmtClaimed
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsClaimedDate", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsClaimedDate
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsAmtRecvd", OleDb.OleDbType.Double)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsAmtRecvd
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsRefNo", OleDb.OleDbType.VarChar)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsRefNo
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_InsRefDate", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_InsRefDate
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Remarks", OleDb.OleDbType.VarChar, 200)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Remarks
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_CreatedBy", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_CreatedBy
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_CreatedOn", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_CreatedOn
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ApprovedBy", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ApprovedBy
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_ApprovedOn", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_ApprovedOn
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Status", OleDb.OleDbType.VarChar, 25)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Status
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Delflag", OleDb.OleDbType.VarChar, 5)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Delflag
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_YearID", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_YearID
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_CompID", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_CompID
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_Deletedby", OleDb.OleDbType.Integer, 4)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_Deletedby
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_DeletedOn", OleDb.OleDbType.Date)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_DeletedOn
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@AFAD_IPAddress", OleDb.OleDbType.VarChar, 100)
			ObjParam(iParamCount).Value = objAsstDelTrn.AFAD_IPAddress
			ObjParam(iParamCount).Direction = ParameterDirection.Input
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
			ObjParam(iParamCount).Direction = ParameterDirection.Output
			iParamCount += 1

			ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
			ObjParam(iParamCount).Direction = ParameterDirection.Output
			Arr(0) = "@iUpdateOrSave"
			Arr(1) = "@iOper"

			Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_FixedAssetDeletion", 1, Arr, ObjParam)
			Return Arr
		Catch ex As Exception
			Throw
		End Try
	End Function
	'Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsstId As Integer, ByVal iCustId As Integer) As String
	'	Dim dt As New DataTable
	'	Dim sSql As String = ""
	'	Try
	'		sSql = "select AFAA_Delflag from  Acc_FixedAssetAdditionDel"
	'		sSql = sSql & " Where AFAA_ID=" & iAsstId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iyearId & " and AFAA_CustId=" & iCustId & ""
	'		GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
	'		Return GetStatus
	'	Catch ex As Exception
	'		Throw
	'	End Try
	'End Function
	Public Function GenerateTransactionNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer) As String
		Dim sSql As String = "", sPrefix As String = ""
		Dim iMax As Integer = 0
		Dim ds As New DataSet
		Try

			iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AFAD_ID)+1,1) from Acc_FixedAssetDeletion where AFAD_YearID=" & iyearId & " and AFAD_CompID=" & iCompID & "")
			sPrefix = "DEL000" & iMax
			Return sPrefix
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal sTransNo As String, ByVal iCustId As Integer) As String
		Dim dt As New DataTable
		Dim sSql As String = ""
		Try
			sSql = "select AFAD_Delflag from Acc_FixedAssetDeletion"
			sSql = sSql & " Where AFAD_TransNo='" & sTransNo & "' and AFAD_CompID=" & iCompID & " and AFAD_YearID=" & iyearId & " and AFAD_CustomerName=" & iCustId & ""
			GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
			Return GetStatus
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Sub StatusCheck(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iTransno As String, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer, ByVal iCustId As Integer)
		Dim sSql As String = ""
		Try
			sSql = "Update Acc_FixedAssetDeletion Set AFAD_Delflag='" & Sdelflag & "',AFAD_ApprovedBy=" & iApedby & ",AFAD_ApprovedOn=getdate(),AFAD_Deletedby=" & iApedby & ",AFAD_DeletedOn=getdate(),AFAD_Status='" & sStatus & "'"
			sSql = sSql & " Where AFAD_TransNo='" & iTransno & "' and AFAD_CompID=" & iCompID & " and AFAD_YearID=" & iyearId & " and AFAD_CustomerName=" & iCustId & ""
			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
		Catch ex As Exception
			Throw
		End Try
	End Sub
	Public Sub UpdateStatusAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAssetId As Integer, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer, ByVal iCustId As Integer)
		Dim sSql As String = ""
		Try
			sSql = "Update Acc_FixedAssetMaster Set AFAM_Delflag='" & Sdelflag & "',AFAM_Status='" & sStatus & "'"
			sSql = sSql & " Where AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_YearID=" & iyearId & " and AFAM_CustId=" & iCustId & ""
			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

			sSql = "Update Acc_FixedAssetAdditionDel Set AFAA_Status='" & sStatus & "'"
			sSql = sSql & " Where AFAA_ItemType=" & iAssetId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iyearId & " and AFAA_CustId=" & iCustId & ""
			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

		Catch ex As Exception
			Throw
		End Try
	End Sub
	Public Sub UpdateStatusTransfer(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAssetId As Integer, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer, ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal iDivisionId As Integer, ByVal iDepartmentId As Integer, ByVal iBayId As Integer)
		Dim sSql As String = ""
		Try
			sSql = "Update Acc_FixedAssetMaster Set AFAM_Delflag='" & Sdelflag & "',AFAM_Status='" & sStatus & "'"
			sSql = sSql & " Where AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_YearID=" & iyearId & " and AFAM_CustId=" & iCustId & "  and AFAM_Location=" & iLocationId & "  and AFAM_Division=" & iDivisionId & " and AFAM_Department=" & iDepartmentId & " and AFAM_Bay=" & iBayId & ""
			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

			sSql = "Update Acc_FixedAssetAdditionDel Set AFAA_Delflag='" & Sdelflag & "',AFAA_Status='" & sStatus & "'"
			sSql = sSql & " Where AFAA_ItemType=" & iAssetId & " and AFAA_CompID=" & iCompID & " and AFAA_YearID=" & iyearId & " and AFAA_CustId=" & iCustId & "  and AFAA_Location=" & iLocationId & "  and AFAA_Division=" & iDivisionId & " and AFAA_Department=" & iDepartmentId & " and AFAA_Bay=" & iBayId & ""
			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

			'sSql = "Update Acc_AssetDepreciation Set ADep_DelFlag='" & Sdelflag & "',ADep_Status='" & sStatus & "'"
			'sSql = sSql & " Where ADep_Item=" & iAssetId & " and ADep_CompID=" & iCompID & " and ADep_YearID=" & iyearId & " and ADep_CustId=" & iCustId & "  and ADep_Location=" & iLocationId & "  and ADep_Division=" & iDivisionId & " and ADep_Department=" & iDepartmentId & " and ADep_Bay=" & iBayId & ""
			'objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

		Catch ex As Exception
			Throw
		End Try
	End Sub
	Public Sub InsertintoAssetMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAssetId As Integer, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer, ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal iDivisionId As Integer, ByVal iDepartmentId As Integer, ByVal iBayId As Integer, ByVal dDate As Date)
		Dim sSql As String = ""
		Dim iID As Integer
		Dim dt As New DataTable
		Dim sdate As String
		'Dim ddate As Date
		Try
			'sSql = "Update Acc_FixedAssetMaster Set AFAM_Delflag='" & Sdelflag & "',AFAM_Status='" & sStatus & "',AFAM_Location=" & iLocationId & ",AFAM_Division=" & iDivisionId & ",AFAM_Department=" & iDepartmentId & ",AFAM_Bay=" & iBayId & ""
			'sSql = sSql & " Where AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_YearID=" & iyearId & " and AFAM_CustId=" & iCustId & ""
			'objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

			sSql = "select * from Acc_FixedAssetMaster"
			sSql = sSql & " Where AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & "  and AFAM_CustId=" & iCustId & ""
			dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

			'ddate = dt.Rows(0).Item("AFAM_CommissionDate")
			sdate = Format(dDate, "yyyy-MM-dd")

			sSql = "Select isnull(max(AFAM_ID)+1,1) from Acc_FixedAssetMaster where AFAM_CompID=" & iCompID & ""
			iID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

			sSql = "" : sSql = "Insert into Acc_FixedAssetMaster(AFAM_ID,AFAM_AssetType,AFAM_AssetCode,AFAM_Description,AFAM_ItemCode,AFAM_ItemDescription,AFAM_CommissionDate,AFAM_Quantity,AFAM_Unit,AFAM_AssetAge,"
			sSql = sSql & "AFAM_Location,AFAM_Division,AFAM_Department,AFAM_Bay,AFAM_DelFlag,AFAM_Status,AFAM_YearID,AFAM_CompID,AFAM_Opeartion,AFAM_IPAddress,AFAM_CustId,AFAM_CreatedBy,AFAM_CreatedBy)"

			sSql = sSql & " Values(" & iID & "," & dt.Rows(0).Item("AFAM_AssetType") & ",'" & dt.Rows(0).Item("AFAM_AssetCode") & "','" & dt.Rows(0).Item("AFAM_Description") & "','" & dt.Rows(0).Item("AFAM_ItemCode") & "',"
			sSql = sSql & " '" & dt.Rows(0).Item("AFAM_ItemDescription") & "','" & sdate & "','" & dt.Rows(0).Item("AFAM_Quantity") & "' ," & dt.Rows(0).Item("AFAM_Unit") & ","
			sSql = sSql & " " & dt.Rows(0).Item("AFAM_AssetAge") & "," & iLocationId & "," & iDivisionId & "," & iDepartmentId & ","
			sSql = sSql & " " & iBayId & ",'A','A'," & iyearId & ","
			sSql = sSql & " " & dt.Rows(0).Item("AFAM_CompID") & ",'C','" & dt.Rows(0).Item("AFAM_IPAddress") & "'," & dt.Rows(0).Item("AFAM_CustId") & "," & iApedby & ",GetDate())"

			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
			'Return dt.Rows(0)("ADD_ID")

		Catch ex As Exception
			Throw
		End Try
	End Sub
	Public Sub InsertintoAssetMasterAdd(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAssetId As Integer, ByVal iAssetClassId As Integer, ByVal sStatus As String, ByVal Sdelflag As String, ByVal iApedby As Integer,
										ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal iDivisionId As Integer, ByVal iDepartmentId As Integer, ByVal iBayId As Integer, ByVal dAssetValue As Double, ByVal dDepAmount As Double, ByVal dWDVValue As Double, ByVal iuserid As Integer)
		Dim sSql As String = ""
		Dim iID As Integer
		Dim iMasterID As Integer
		Dim dt As New DataTable
		Dim sdate As String
		Dim ddate As Date

		Dim SCreatedOn As String
		Dim dCreatedOn As Date

		Dim SApprovedOn As String
		Dim dApprovedOn As Date
		Try

			sSql = "select * from Acc_FixedAssetAdditionDel"
			sSql = sSql & " Where AFAA_AssetType=" & iAssetClassId & " and AFAA_ItemType=" & iAssetId & " and AFAA_CompID=" & iCompID & "  and AFAA_CustId=" & iCustId & ""
			dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

			ddate = dt.Rows(0).Item("AFAA_PurchaseDate")
			sdate = Format(ddate, "yyyy-MM-dd")

			dCreatedOn = dt.Rows(0).Item("AFAA_PurchaseDate")
			SCreatedOn = Format(ddate, "yyyy-MM-dd")

			dApprovedOn = dt.Rows(0).Item("AFAA_PurchaseDate")
			SApprovedOn = Format(ddate, "yyyy-MM-dd")

			sSql = "Select isnull(max(AFAA_ID)+1,1) from Acc_FixedAssetAdditionDel where AFAA_CompID=" & iCompID & ""
			iID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

			sSql = "Select isnull(max(AFAM_ID),1) from Acc_FixedAssetMaster where AFAM_CompID=" & iCompID & ""
			iMasterID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

			sSql = "" : sSql = "Insert into Acc_FixedAssetAdditionDel(AFAA_ID,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay,AFAA_TrType,AFAA_AssetType,AFAA_AssetNo,AFAA_PurchaseDate,AFAA_AssetAmount,AFAA_FYAmount,AFAA_DepreAmount,"
			sSql = sSql & " AFAA_CreatedBy,AFAA_CreatedOn,AFAA_Status,AFAA_Delflag,AFAA_YearID,AFAA_CompID,AFAA_Operation,AFAA_IPAddress,AFAA_ApprovedBy,AFAA_ApprovedOn,AFAA_ItemType,AFAA_CustId)"

			sSql = sSql & " Values(" & iID & "," & iLocationId & "," & iDivisionId & "," & iDepartmentId & "," & iBayId & ","
			sSql = sSql & " " & dt.Rows(0).Item("AFAA_TrType") & "," & dt.Rows(0).Item("AFAA_AssetType") & ",'" & dt.Rows(0).Item("AFAA_AssetNo") & "' ,'" & sdate & "',"
			sSql = sSql & " " & dAssetValue & ", " & dWDVValue & ", " & dDepAmount & ", " & dt.Rows(0).Item("AFAA_CreatedBy") & ","
			sSql = sSql & " '" & SCreatedOn & "','A','A'," & iyearId & "," & dt.Rows(0).Item("AFAA_CompID") & ",'C','" & dt.Rows(0).Item("AFAA_IPAddress") & "',"
			sSql = sSql & " " & iuserid & ",'" & SApprovedOn & "'," & iMasterID & "," & dt.Rows(0).Item("AFAA_CustId") & ")"

			objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
			'Return dt.Rows(0)("ADD_ID")

		Catch ex As Exception
			Throw
		End Try
	End Sub
	Public Function GetAssetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsstClassId As Integer, ByVal iAssetId As Integer, ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal iDivisionId As Integer, ByVal iDepartmentId As Integer, ByVal iBayId As Integer) As String
		Dim dt As New DataTable
		Dim sSql As String = ""
		Try
			sSql = "select AFAM_DelFlag from  Acc_FixedAssetMaster"
			sSql = sSql & " Where AFAM_AssetType=" & iAsstClassId & " and AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustId & " and AFAM_Location=" & iLocationId & " and AFAM_Division=" & iDivisionId & " and AFAM_Department=" & iDepartmentId & " and AFAM_Bay=" & iBayId & ""
			GetAssetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
			Return GetAssetStatus
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function GetMastersDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearId As Integer, ByVal iAsstClassId As Integer, ByVal iAssetId As Integer, ByVal iCustId As Integer, ByVal iLocationId As Integer, ByVal iDivisionId As Integer, ByVal iDepartmentId As Integer, ByVal iBayId As Integer) As DataTable
		Dim sSql As String
		Dim dt As New DataTable
		Dim TransType As Integer = 0
		Try

			TransType = objDBL.SQLExecuteScalar(sNameSpace, "Select AFAA_TrType from Acc_FixedAssetAdditionDel where AFAA_AssetType=" & iAsstClassId & " and AFAA_ItemType=" & iAssetId & " and AFAA_CompID=" & iCompID & "  and AFAA_CustId=" & iCustId & " and AFAA_Location=" & iLocationId & " and AFAA_Division=" & iDivisionId & " and AFAA_Department=" & iDepartmentId & " and AFAA_Bay=" & iBayId & "")

			If TransType = 1 Then
				sSql = "select a.AFAM_Quantity as Quantity,b.AFAA_AssetAmount as OriginalCost from Acc_FixedAssetMaster a "
				sSql = sSql & " left join Acc_FixedAssetAdditionDel b on"
				sSql = sSql & " a.AFAM_AssetType=b.AFAA_AssetType and a.AFAM_ID=b.AFAA_ItemType"
				sSql = sSql & " Where AFAM_AssetType=" & iAsstClassId & " and AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustId & " and AFAM_Location=" & iLocationId & " and AFAM_Division=" & iDivisionId & " and AFAM_Department=" & iDepartmentId & " and AFAM_Bay=" & iBayId & ""
				dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
			ElseIf TransType = 2 Then

				sSql = "select a.AFAM_Quantity as Quantity,isnull(sum(b.FAAD_AssetValue),0) as OriginalCost from Acc_FixedAssetMaster a "
				sSql = sSql & " left join Acc_FixedAssetAdditionDetails b on"
				sSql = sSql & " a.AFAM_AssetType=b.FAAD_AssetType and a.AFAM_ID=b.FAAD_ItemType"
				sSql = sSql & " Where AFAM_AssetType=" & iAsstClassId & " and AFAM_ID=" & iAssetId & " and AFAM_CompID=" & iCompID & " and AFAM_CustId=" & iCustId & " and AFAM_Location=" & iLocationId & " and AFAM_Division=" & iDivisionId & " and AFAM_Department=" & iDepartmentId & " and AFAM_Bay=" & iBayId & " group by  a.AFAM_Quantity"
				dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
			ElseIf TransType = 0 Then
			End If

			Return dt
		Catch ex As Exception
			Throw
		End Try
	End Function
	Public Function GetFYAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As DataTable

		Dim sSql As String = ""
		Dim Amount As New DataTable
		Try
			sSql = "select isnull(ADep_OPBForYR,0) as DepreciationforFY from Acc_AssetDepreciation "
			sSql = sSql & " where ADep_AssetID=" & iAssetClassId & " and ADep_CompID=" & iCompID & " and ADep_Item=" & iAssetId & " and ADep_CustId=" & iCustID & " and ADep_Location=" & iLocationID & " and ADep_Division=" & iDivisionID & " and ADep_Department=" & iDepartmentID & " and ADep_Bay=" & iBayID & " and ADep_YearID=" & iYearId & ""
			Amount = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
			If Amount.Rows.Count = 0 Then
				sSql = "select isnull(AFAA_DepreAmount,0) as DepreciationforFY from Acc_FixedAssetAdditionDel  "
				sSql = sSql & " where AFAA_AssetType=" & iAssetClassId & " and AFAA_CompID=" & iCompID & " and AFAA_ItemCode=" & iAssetId & " and AFAA_CustId=" & iCustID & " and AFAA_Location=" & iLocationID & " and AFAA_Division=" & iDivisionID & " and AFAA_Department=" & iDepartmentID & " and AFAA_Bay=" & iBayID & ""
				Amount = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
			End If
			Return Amount
		Catch ex As Exception
			Throw
		End Try
	End Function

	Public Function GetReate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As DataTable

		Dim sSql As String = ""
		Dim Rate As New DataTable
		Try
			sSql = "select ADep_RateofDep from Acc_AssetDepreciation "
			sSql = sSql & " where ADep_AssetID=" & iAssetClassId & " and ADep_CompID=" & iCompID & " and ADep_Item=" & iAssetId & " and ADep_CustId=" & iCustID & " and ADep_Location=" & iLocationID & " and ADep_Division=" & iDivisionID & " and ADep_Department=" & iDepartmentID & " and ADep_Bay=" & iBayID & " and ADep_YearID=" & iYearId & ""
			Rate = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
			Return Rate
		Catch ex As Exception
			Throw
		End Try
	End Function
End Class
