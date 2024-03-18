Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsReportTemplate
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    Private iTEM_Id As Integer
    Private iTEM_FunctionId As Integer
    Private sTEM_Module As String
    Private iTEM_ReportTitle As String
    Private sTEM_ContentId As String
    Private sTEM_SortOrder As String
    Private sTEM_Delflag As String
    Private sTEM_Status As String
    Private iTEM_CompID As Integer
    Private iTEM_Yearid As Integer
    Private iTEM_CrBy As Integer
    Private dTEM_CrOn As DateTime
    Private iTEM_UpdatedBy As Integer
    Private dTEM_UpdatedOn As DateTime
    Private sTEM_Operation As String
    Private sTEM_IPAddress As String

    Public Property TEM_Id() As Integer
        Get
            Return (iTEM_Id)
        End Get
        Set(ByVal Value As Integer)
            iTEM_Id = Value
        End Set
    End Property
    Public Property TEM_FunctionId() As Integer
        Get
            Return (iTEM_FunctionId)
        End Get
        Set(ByVal Value As Integer)
            iTEM_FunctionId = Value
        End Set
    End Property
    Public Property TEM_Module() As String
        Get
            Return (sTEM_Module)
        End Get
        Set(ByVal Value As String)
            sTEM_Module = Value
        End Set
    End Property
    Public Property TEM_ReportTitle() As Integer
        Get
            Return (iTEM_ReportTitle)
        End Get
        Set(ByVal Value As Integer)
            iTEM_ReportTitle = Value
        End Set
    End Property
    Public Property TEM_ContentId() As String
        Get
            Return (sTEM_ContentId)
        End Get
        Set(ByVal Value As String)
            sTEM_ContentId = Value
        End Set
    End Property
    Public Property TEM_SortOrder() As String
        Get
            Return (sTEM_SortOrder)
        End Get
        Set(ByVal Value As String)
            sTEM_SortOrder = Value
        End Set
    End Property
    Public Property TEM_Delflag() As String
        Get
            Return (sTEM_Delflag)
        End Get
        Set(ByVal Value As String)
            sTEM_Delflag = Value
        End Set
    End Property
    Public Property TEM_Status() As String
        Get
            Return (sTEM_Status)
        End Get
        Set(ByVal Value As String)
            sTEM_Status = Value
        End Set
    End Property
    Public Property TEM_CompID() As Integer
        Get
            Return (iTEM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iTEM_CompID = Value
        End Set
    End Property
    Public Property TEM_Yearid() As Integer
        Get
            Return (iTEM_Yearid)
        End Get
        Set(ByVal Value As Integer)
            iTEM_Yearid = Value
        End Set
    End Property
    Public Property TEM_CrBy() As Integer
        Get
            Return (iTEM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iTEM_CrBy = Value
        End Set
    End Property
    Public Property TEM_CrOn() As DateTime
        Get
            Return (dTEM_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            dTEM_CrOn = Value
        End Set
    End Property
    Public Property TEM_UpdatedBy() As Integer
        Get
            Return (iTEM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iTEM_UpdatedBy = Value
        End Set
    End Property
    Public Property TEM_UpdatedOn() As DateTime
        Get
            Return (dTEM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dTEM_UpdatedOn = Value
        End Set
    End Property
    Public Property TEM_Operation() As String
        Get
            Return (sTEM_Operation)
        End Get
        Set(ByVal Value As String)
            sTEM_Operation = Value
        End Set
    End Property
    Public Property TEM_IPAddress() As String
        Get
            Return (sTEM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sTEM_IPAddress = Value
        End Set
    End Property
    Public Function GetReportTemplateID(ByVal sAC As String, ByVal iCompID As Integer, ByVal iFunctionID As Integer, ByVal sModule As String, ByVal iReportTitle As Integer) As String
        Dim sSql As String
        Dim sContentID As String = ""
        Try
            sSql = "Select TEM_ContentId From SAD_Finalisation_Report_Template Where TEM_FunctionId = " & iFunctionID & " And TEM_Module = '" & sModule & "' And TEM_ReportTitle = " & iReportTitle & " And TEM_Delflag  = 'W'"
            sContentID = objDBL.SQLGetDescription(sAC, sSql)
            Return sContentID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportContentData(ByVal sAC As String, ByVal iCompID As Integer, ByVal iFPTID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "select RCM_Heading,RCM_Description from SAD_ReportContentMaster Where RCM_Id  = " & iFPTID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadReortTemplateToGrid(ByVal sAC As String, ByVal iCompID As Integer, ByVal iFunId As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select RCM_Id,RCM_Heading from SAD_ReportContentMaster Where RCM_CompID=" & iCompID & " And (RCM_ReportId=" & iFunId & " Or RCM_ReportId=0) And RCM_Delflag<>'D' Order by RCM_Heading"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveReportTemplate(ByVal sAC As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal objReportTemplate As clsReportTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_FunctionId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_Module", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_Module
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_ReportTitle", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_ReportTitle
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_ContentId", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_ContentId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_SortOrder", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_SortOrder
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iYearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@TEM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objReportTemplate.TEM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_Finalisation_Report_Template", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetReportTemplateForLOE(ByVal sAC As String, ByVal iCompID As Integer, ByVal iReportId As Integer, ByVal sHeading As String) As String
        Dim sSql As String = ""
        Try
            sSql = "Select RCM_Description from SAD_ReportContentMaster Where RCM_CompID=" & iCompID & " And RCM_ReportId=" & iReportId & " And RCM_Heading='" & sHeading & "'"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
