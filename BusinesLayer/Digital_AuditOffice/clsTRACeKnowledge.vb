Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Structure strTRACe_CabinetDetails
    Private TC_PKID As Integer
    Private TC_Name As String
    Private TC_Remarks As String
    Private TC_Status As String
    Private TC_CrBy As Integer
    Private TC_IPAddress As String
    Private TC_CompID As Integer
    Public Property iTC_PKID() As Integer
        Get
            Return (TC_PKID)
        End Get
        Set(ByVal Value As Integer)
            TC_PKID = Value
        End Set
    End Property
    Public Property sTC_Name() As String
        Get
            Return (TC_Name)
        End Get
        Set(ByVal Value As String)
            TC_Name = Value
        End Set
    End Property
    Public Property sTC_Remarks() As String
        Get
            Return (TC_Remarks)
        End Get
        Set(ByVal Value As String)
            TC_Remarks = Value
        End Set
    End Property
    Public Property sTC_Status() As String
        Get
            Return (TC_Status)
        End Get
        Set(ByVal Value As String)
            TC_Status = Value
        End Set
    End Property
    Public Property iTC_CrBy() As Integer
        Get
            Return (TC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TC_CrBy = Value
        End Set
    End Property
    Public Property sTC_IPAddress() As String
        Get
            Return (TC_IPAddress)
        End Get
        Set(ByVal Value As String)
            TC_IPAddress = Value
        End Set
    End Property
    Public Property iTC_CompID() As Integer
        Get
            Return (TC_CompID)
        End Get
        Set(ByVal Value As Integer)
            TC_CompID = Value
        End Set
    End Property
End Structure
Public Structure strTRACe_SubCabinet
    Private TSC_PKID As Integer
    Private TSC_CabinetID As Integer
    Private TSC_Name As String
    Private TSC_Remarks As String
    Private TSC_Decs As String
    Private TSC_Status As String
    Private TSC_CrBy As Integer
    Private TSC_IPAddress As String
    Private TSC_CompID As Integer

    Public Property iTSC_PKID() As Integer
        Get
            Return (TSC_PKID)
        End Get
        Set(ByVal Value As Integer)
            TSC_PKID = Value
        End Set
    End Property
    Public Property iTSC_CabinetID() As Integer
        Get
            Return (TSC_CabinetID)
        End Get
        Set(ByVal Value As Integer)
            TSC_CabinetID = Value
        End Set
    End Property
    Public Property sTSC_Name() As String
        Get
            Return (TSC_Name)
        End Get
        Set(ByVal Value As String)
            TSC_Name = Value
        End Set
    End Property
    Public Property sTSC_Remarks() As String
        Get
            Return (TSC_Remarks)
        End Get
        Set(ByVal Value As String)
            TSC_Remarks = Value
        End Set
    End Property
    Public Property sTSC_Decs() As String
        Get
            Return (TSC_Decs)
        End Get
        Set(ByVal Value As String)
            TSC_Decs = Value
        End Set
    End Property
    Public Property sTSC_Status() As String
        Get
            Return (TSC_Status)
        End Get
        Set(ByVal Value As String)
            TSC_Status = Value
        End Set
    End Property
    Public Property iTSC_CrBy() As Integer
        Get
            Return (TSC_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TSC_CrBy = Value
        End Set
    End Property
    Public Property sTSC_IPAddress() As String
        Get
            Return (TSC_IPAddress)
        End Get
        Set(ByVal Value As String)
            TSC_IPAddress = Value
        End Set
    End Property
    Public Property iTSC_CompID() As Integer
        Get
            Return (TSC_CompID)
        End Get
        Set(ByVal Value As Integer)
            TSC_CompID = Value
        End Set
    End Property
End Structure
Public Structure strTRACe_Folder
    Private TF_PKID As Integer
    Private TF_CabinetID As Integer
    Private TF_SubCabinetID As Integer
    Private TF_Name As String
    Private TF_Remarks As String
    Private TF_Status As String
    Private TF_CrBy As Integer
    Private TF_UpdatedBy As Integer
    Private TF_IPAddress As String
    Private TF_CompID As String

    Public Property iTF_PKID() As Integer
        Get
            Return (TF_PKID)
        End Get
        Set(ByVal Value As Integer)
            TF_PKID = Value
        End Set
    End Property
    Public Property iTF_CabinetID() As Integer
        Get
            Return (TF_CabinetID)
        End Get
        Set(ByVal Value As Integer)
            TF_CabinetID = Value
        End Set
    End Property
    Public Property iTF_SubCabinetID() As Integer
        Get
            Return (TF_SubCabinetID)
        End Get
        Set(ByVal Value As Integer)
            TF_SubCabinetID = Value
        End Set
    End Property
    Public Property sTF_Name() As String
        Get
            Return (TF_Name)
        End Get
        Set(ByVal Value As String)
            TF_Name = Value
        End Set
    End Property
    Public Property sTF_Remarks() As String
        Get
            Return (TF_Remarks)
        End Get
        Set(ByVal Value As String)
            TF_Remarks = Value
        End Set
    End Property
    Public Property sTF_Status() As String
        Get
            Return (TF_Status)
        End Get
        Set(ByVal Value As String)
            TF_Status = Value
        End Set
    End Property
    Public Property iTF_CrBy() As Integer
        Get
            Return (TF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TF_CrBy = Value
        End Set
    End Property
    Public Property iTF_UpdatedBy() As Integer
        Get
            Return (TF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            TF_UpdatedBy = Value
        End Set
    End Property
    Public Property sTF_IPAddress() As String
        Get
            Return (TF_IPAddress)
        End Get
        Set(ByVal Value As String)
            TF_IPAddress = Value
        End Set
    End Property
    Public Property iTF_CompID() As Integer
        Get
            Return (TF_CompID)
        End Get
        Set(ByVal Value As Integer)
            TF_CompID = Value
        End Set
    End Property
End Structure
Public Structure strTRACe_Documents
    Private TF_PKID As Integer
    Private TF_CabinetID As Integer
    Private TF_SubCabinetID As Integer
    Private TF_FolderID As Integer
    Private TF_OLE As String
    Private TF_FilePath As String
    Private TF_Name As String
    Private TF_Desc As String
    Private TF_Remarks As String
    Private TF_Status As String
    Private TF_CrBy As Integer
    Private TF_UpdatedBy As Integer
    Private TF_Attch_CrBy As Integer
    Private TF_Attch_CrOn As Date
    Private TF_IPAddress As String
    Private TF_CompID As Integer

    Public Property iTF_PKID() As Integer
        Get
            Return (TF_PKID)
        End Get
        Set(ByVal Value As Integer)
            TF_PKID = Value
        End Set
    End Property
    Public Property iTF_CabinetID() As Integer
        Get
            Return (TF_CabinetID)
        End Get
        Set(ByVal Value As Integer)
            TF_CabinetID = Value
        End Set
    End Property
    Public Property iTF_SubCabinetID() As Integer
        Get
            Return (TF_SubCabinetID)
        End Get
        Set(ByVal Value As Integer)
            TF_SubCabinetID = Value
        End Set
    End Property
    Public Property iTF_FolderID() As Integer
        Get
            Return (TF_FolderID)
        End Get
        Set(ByVal Value As Integer)
            TF_FolderID = Value
        End Set
    End Property
    Public Property sTF_OLE() As String
        Get
            Return (TF_OLE)
        End Get
        Set(ByVal Value As String)
            TF_OLE = Value
        End Set
    End Property
    Public Property sTF_FilePath() As String
        Get
            Return (TF_FilePath)
        End Get
        Set(ByVal Value As String)
            TF_FilePath = Value
        End Set
    End Property
    Public Property sTF_Name() As String
        Get
            Return (TF_Name)
        End Get
        Set(ByVal Value As String)
            TF_Name = Value
        End Set
    End Property
    Public Property sTF_Remarks() As String
        Get
            Return (TF_Remarks)
        End Get
        Set(ByVal Value As String)
            TF_Remarks = Value
        End Set
    End Property
    Public Property sTF_Desc() As String
        Get
            Return (TF_Desc)
        End Get
        Set(ByVal Value As String)
            TF_Desc = Value
        End Set
    End Property
    Public Property sTF_Status() As String
        Get
            Return (TF_Status)
        End Get
        Set(ByVal Value As String)
            TF_Status = Value
        End Set
    End Property
    Public Property iTF_CrBy() As Integer
        Get
            Return (TF_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TF_CrBy = Value
        End Set
    End Property
    Public Property iTF_UpdatedBy() As Integer
        Get
            Return (TF_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            TF_UpdatedBy = Value
        End Set
    End Property
    Public Property iTF_Attch_CrBy() As Integer
        Get
            Return (TF_Attch_CrBy)
        End Get
        Set(ByVal Value As Integer)
            TF_Attch_CrBy = Value
        End Set
    End Property
    Public Property dTF_Attch_CrOn() As Date
        Get
            Return (TF_Attch_CrOn)
        End Get
        Set(ByVal Value As Date)
            TF_Attch_CrOn = Value
        End Set
    End Property
    Public Property sTF_IPAddress() As String
        Get
            Return (TF_IPAddress)
        End Get
        Set(ByVal Value As String)
            TF_IPAddress = Value
        End Set
    End Property
    Public Property iTF_CompID() As Integer
        Get
            Return (TF_CompID)
        End Get
        Set(ByVal Value As Integer)
            TF_CompID = Value
        End Set
    End Property
End Structure
Public Class clsTRACeKnowledge
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function LoadCabinetSubCabinetFolder(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String, ByVal iSubcabinetID As Integer) As DataTable
        Dim sSql As String = "" : Dim dt As New DataTable
        Try
            If sType = "Cabinet" Then
                sSql = "Select CBN_ID,CBN_NAME from edt_cabinet WHERE CBN_PARENT=-1 and cbn_DelStatus='A'  And  CBN_PERMISSION = '1' order by cbn_name"
            End If
            If sType = "SubCabinet" Then
                sSql = "Select CBN_ID,CBN_NAME from edt_cabinet WHERE CBN_PARENT <> -1 and  cbn_DelStatus='A'  And  CBN_PERMISSION = '1' order by cbn_name"
            End If
            If sType = "Folder" Then
                sSql = "Select FOL_FOLID,FOL_NAME From edt_folder  Where FOL_CABINET=" & iSubcabinetID & " order by FOL_NAME"
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCabinetDetails(ByVal sAC As String, ByVal objstrTRACe_CabinetDetails As strTRACe_CabinetDetails) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(8) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.iTC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.sTC_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.sTC_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.sTC_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.iTC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.sTC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrTRACe_CabinetDetails.iTC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_Cabinet", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubCabinetDetails(ByVal sAC As String, ByVal objclsSubCabinetDetails As strTRACe_SubCabinet) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.iTSC_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_CabinetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.iTSC_CabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.sTSC_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.sTSC_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_Decs", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.sTSC_Decs
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.sTSC_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.iTSC_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.sTSC_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TSC_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsSubCabinetDetails.iTSC_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_SubCabinet", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFolderDetails(ByVal sAC As String, ByVal objclsFolderDetails As strTRACe_Folder) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CabinetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_CabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_SubCabinetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_SubCabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsFolderDetails.sTF_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsFolderDetails.sTF_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsFolderDetails.sTF_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsFolderDetails.sTF_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsFolderDetails.iTF_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_Folder", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDocumentsDetails(ByVal sAC As String, ByVal objclsTRACeDocuments As strTRACe_Documents) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CabinetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_CabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_SubCabinetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_SubCabinetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_FolderID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_FolderID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_FilePath", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_FilePath
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Name", OleDb.OleDbType.VarChar, 1000)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_Name
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Status", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Attch_CrBy", OleDb.OleDbType.Integer, 5)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_Attch_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_Attch_CrOn", OleDb.OleDbType.Date, 30)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.dTF_Attch_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.sTF_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TF_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsTRACeDocuments.iTF_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spTRACe_Documents", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocumentDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabinetID As Integer, ByVal iSubCabinetID As Integer, ByVal iFolderID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim i As Integer
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("FileID")
            dt.Columns.Add("Cabinet")
            dt.Columns.Add("SubCabinet")
            dt.Columns.Add("FolderName")
            dt.Columns.Add("FileName")
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileDesc")
            dt.Columns.Add("FileCrBy")
            dt.Columns.Add("FileCrOn")

            sSql = " Select a.CBN_NAME As Cabinet,b.CBN_NAME As SubCabinet,f.FOL_NAME As FolderName,d.TF_PKID,d.TF_Name As FileName,d.TF_Desc,Usr_FullName,Convert(Varchar(10),d.TF_Attch_CrOn,103)TF_Attch_CrOn,"
            sSql = sSql & " TF_FilePath As FilePath from TRACe_Documents d "
            sSql = sSql & " Left Join sad_Userdetails On TF_Attch_CrBy=usr_id And Usr_CompID=" & iACID & " "
            sSql = sSql & " Left Join edt_cabinet a On a.CBN_ID=TF_CabinetID And  a.CBN_PERMISSION = 1  And a.CBN_PARENT=-1  "
            sSql = sSql & " Left Join edt_cabinet b On b.CBN_ID=TF_SubCabinetID  And  b.CBN_PERMISSION = 1  "
            sSql = sSql & " Left Join edt_folder f On f.FOL_FOLID=d.TF_FolderID  "
            sSql = sSql & " Where d.TF_CompID=1 "
            If iCabinetID > 0 Then
                sSql = sSql & " And d.TF_CabinetID=" & iCabinetID & ""
            End If
            If iSubCabinetID > 0 Then
                sSql = sSql & " And d.TF_SubCabinetID=" & iSubCabinetID & ""
            End If
            If iFolderID > 0 Then
                sSql = sSql & " And d.TF_FolderID=" & iFolderID & ""
            End If
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtTab.Rows.Count > 0 Then
                For i = 0 To dtTab.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtTab.Rows(i)("TF_PKID")) = False Then
                        dRow("FileID") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("TF_PKID"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("Cabinet")) = False Then
                        dRow("Cabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("Cabinet"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("SubCabinet")) = False Then
                        dRow("SubCabinet") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("SubCabinet"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("FolderName")) = False Then
                        dRow("FolderName") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("FolderName"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("FileName")) = False Then
                        dRow("FileName") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("FileName"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("FilePath")) = False Then
                        dRow("FilePath") = dtTab.Rows(i)("FilePath")
                    End If
                    If IsDBNull(dtTab.Rows(i)("TF_Desc")) = False Then
                        dRow("FileDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("TF_Desc"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("Usr_FullName")) = False Then
                        dRow("FileCrBy") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("Usr_FullName"))
                    End If
                    If IsDBNull(dtTab.Rows(i)("TF_Attch_CrOn")) = False Then
                        dRow("FileCrOn") = objclsGRACeGeneral.FormatDtForRDBMS(dtTab.Rows(i)("TF_Attch_CrOn"), "F")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachments(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Try
            sSql = "Select ATCH_ID, ATCH_DOCID,ATCH_EXT,(ATCH_FName + '.' + ATCH_EXT) as ATCH_FName,ATCH_CreatedBy,ATCH_CREATEDON From edt_attachments where ATCH_CompID=" & iACID & " And "
            sSql = sSql & " ATCH_ID=" & iAttachID & " AND ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetCabinateID(ByVal sAC As String, ByVal iACID As Integer, ByVal sCabinateName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select CBN_ID From edt_cabinet Where CBN_Name='" & sCabinateName & "' And CBN_PARENT=-1"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubCabinateID(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubCabinateName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select CBN_ID From edt_cabinet Where CBN_Name='" & sSubCabinateName & "' "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFolderID(ByVal sAC As String, ByVal iACID As Integer, ByVal sFolderName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select FOL_FOLID From edt_folder Where FOL_Name='" & sFolderName & "' "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function InsertPageDetails(ByVal sAC As String, iACID As Integer, ByVal iCBN_NODE As Integer, ByVal iSCBN_NODE As Integer, ByVal iFol_ID As Integer, ByVal iPGE_DOCUMENT_TYPE As Integer, ByVal iUserID As Integer, ByVal sTitle As String, ByVal sPathName As String)
        Dim aSql As String
        Dim iMax As Integer = 0
        Try
            iMax = objclsGeneralFunctions.GetMaxID(sAC, iACID, "edt_page", "PGE_BASENAME", "PGE_Compid")
            If sPathName = "" Then
                aSql = "Insert Into edt_page (PGE_BASENAME,PGE_CABINET,PGE_SubCabinet,PGE_FOLDER,PGE_Details_ID,PGE_Compid,pge_Delflag,PGE_Date,PGE_CrBy,PGE_DOCUMENT_TYPE,PGE_Status,PGE_TITLE,PGE_ext,pge_size) 
            Values (" & iMax & "," & iCBN_NODE & "," & iSCBN_NODE & "," & iFol_ID & "," & iMax & "," & iACID & ",'A'," & Today.Date & "," & iUserID & "," & iPGE_DOCUMENT_TYPE & ",'A','" & sTitle & "','.xlsx',0)"
            Else
                aSql = "Insert Into edt_page (PGE_BASENAME,PGE_CABINET,PGE_SubCabinet,PGE_FOLDER,PGE_Details_ID,PGE_Compid,pge_Delflag,PGE_Date,PGE_CrBy,PGE_DOCUMENT_TYPE,PGE_Status,PGE_TITLE,PGE_ext,pge_size) 
            Values (" & iMax & "," & iCBN_NODE & "," & iSCBN_NODE & "," & iFol_ID & "," & iMax & "," & iACID & ",'A'," & Today.Date & "," & iUserID & "," & iPGE_DOCUMENT_TYPE & ",'A','" & sTitle & "','" & sPathName & "',0)"
            End If
            objDBL.SQLExecuteNonQuery(sAC, aSql)
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
