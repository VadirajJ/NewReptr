Imports DatabaseLayer
Imports System.Data.SqlClient
Imports BusinesLayer
Public Class clsReports
    Private objDBL As New DatabaseLayer.DBHelper
    Dim iRG_Id As Integer
    Dim iRG_CustomerId As Integer
    Dim iRG_Signedby As Integer
    Dim iRG_YearId As Integer
    Dim iRG_ReportType As Integer
    Dim iRG_Module As Integer
    Dim iRG_Report As Integer
    Dim iRG_Heading As Integer
    Dim sRG_Description As String
    Dim iRG_CrBy As Integer
    Dim dRG_CrOn As DateTime
    Dim iRG_UpdatedBy As Integer
    Dim dRG_UpdatedOn As DateTime
    Dim sRG_IPAddress As String
    Dim iRG_FinancialYear As Integer
    Dim iRG_Compid As Integer
    Dim iRG_AuditId As Integer
    Dim sRG_UDIN As String
    Dim dRG_UDINdate As DateTime
    Public Property RG_Id() As Integer
        Get
            Return (iRG_Id)
        End Get
        Set(ByVal Value As Integer)
            iRG_Id = Value
        End Set
    End Property
    Public Property RG_CustomerId() As Integer
        Get
            Return (iRG_CustomerId)
        End Get
        Set(ByVal Value As Integer)
            iRG_CustomerId = Value
        End Set
    End Property
    Public Property RG_Signedby() As Integer
        Get
            Return (iRG_Signedby)
        End Get
        Set(ByVal Value As Integer)
            iRG_Signedby = Value
        End Set
    End Property
    Public Property RG_YearId() As Integer
        Get
            Return (iRG_YearId)
        End Get
        Set(ByVal Value As Integer)
            iRG_YearId = Value
        End Set
    End Property
    Public Property RG_ReportType() As Integer
        Get
            Return (iRG_ReportType)
        End Get
        Set(ByVal Value As Integer)
            iRG_ReportType = Value
        End Set
    End Property
    Public Property RG_Module() As Integer
        Get
            Return (iRG_Module)
        End Get
        Set(ByVal Value As Integer)
            iRG_Module = Value
        End Set
    End Property
    Public Property RG_Report() As Integer
        Get
            Return (iRG_Report)
        End Get
        Set(ByVal Value As Integer)
            iRG_Report = Value
        End Set
    End Property
    Public Property RG_Heading() As Integer
        Get
            Return (iRG_Heading)
        End Get
        Set(ByVal Value As Integer)
            iRG_Heading = Value
        End Set
    End Property
    Public Property RG_Description() As String
        Get
            Return (sRG_Description)
        End Get
        Set(ByVal Value As String)
            sRG_Description = Value
        End Set
    End Property
    Public Property RG_CrBy() As Integer
        Get
            Return (iRG_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iRG_CrBy = Value
        End Set
    End Property
    Public Property RG_CrOn() As DateTime
        Get
            Return (dRG_CrOn)
        End Get
        Set(ByVal Value As DateTime)
            dRG_CrOn = Value
        End Set
    End Property
    Public Property RG_UpdatedBy() As Integer
        Get
            Return (iRG_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iRG_UpdatedBy = Value
        End Set
    End Property
    Public Property RG_UpdatedOn() As DateTime
        Get
            Return (dRG_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dRG_UpdatedOn = Value
        End Set
    End Property
    Public Property RG_IPAddress() As String
        Get
            Return (sRG_IPAddress)
        End Get
        Set(ByVal Value As String)
            sRG_IPAddress = Value
        End Set
    End Property
    Public Property RG_FinancialYear() As Integer
        Get
            Return (iRG_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iRG_FinancialYear = Value
        End Set
    End Property
    Public Property RG_Compid() As Integer
        Get
            Return (iRG_Compid)
        End Get
        Set(ByVal Value As Integer)
            iRG_Compid = Value
        End Set
    End Property
    Public Property RG_AuditId() As Integer
        Get
            Return (iRG_AuditId)
        End Get
        Set(ByVal Value As Integer)
            iRG_AuditId = Value
        End Set
    End Property
    Public Property RG_UDIN() As String
        Get
            Return (sRG_UDIN)
        End Get
        Set(ByVal Value As String)
            sRG_UDIN = Value
        End Set
    End Property
    Public Property RG_UDINdate() As DateTime
        Get
            Return (dRG_UDINdate)
        End Get
        Set(ByVal Value As DateTime)
            dRG_UDINdate = Value
        End Set
    End Property
    Public Function SaveReportGeneration(ByVal sAC As String, ByVal iACID As Integer, ByVal objclsReports As clsReports) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_CustomerId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_CustomerId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Signedby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Signedby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_YearId ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_ReportType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_ReportType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Module", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Module
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Report", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Report
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Heading", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Heading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Description", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objclsReports.sRG_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_CrOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsReports.dRG_CrOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsReports.dRG_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsReports.sRG_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_FinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_Compid", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_Compid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_AuditId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsReports.iRG_AuditId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_UDIN", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsReports.sRG_UDIN
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RG_UDINdate ", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsReports.dRG_UDINdate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spSAD_ReportGeneration", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHeading(ByVal sAC As String, ByVal iCompID As Integer, ByVal iReport As Integer, ByVal iReportType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select  TEM_Id,TEM_ContentId from SAD_Finalisation_Report_Template where TEM_ReportTitle =" & iReport & " and TEM_FunctionId=" & iReportType & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHeading1(ByVal sAC As String, ByVal iCompID As Integer, ByVal sReport As String, ByVal id As String, ByVal iReportType As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select  RCM_Id,RCM_Heading from SAD_ReportContentMaster where RCM_Id in(" & id & ") and RCM_ReportId=" & iReportType & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescription(ByVal sAC As String, ByVal iCompID As Integer, ByVal iReport As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select  RCM_Description from SAD_ReportContentMaster where RCM_Id=" & iReport & " "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescriptionfromRepot(ByVal sAC As String, ByVal iCompID As Integer, ByVal iheading As Integer, ByVal iReport As Integer, ByVal icustid As Integer, ByVal iAuditNo As Integer, ByVal ireporttype As Integer, ByVal iyearid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select RG_Description,ISNULL(RG_Signedby,0) As RG_Signedby,ISNULL(RG_UDIN,'') As RG_UDIN,ISNULL(Convert(Varchar(10),RG_UDINdate,103),'') As RG_UDINdate from SAD_ReportGeneration where RG_Heading=" & iheading & " and RG_Report=" & iReport & " and RG_CustomerId=" & icustid & " and RG_AuditId=" & iAuditNo & " and RG_ReportType=" & ireporttype & " and RG_YearId=" & iyearid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImgPath(ByVal sAC As String, ByVal sKey As String) As String
        Dim sSql As String
        Try
            sSql = "Select sad_Config_Value from sad_config_settings where sad_Config_Key = '" & sKey & "'"
            GetImgPath = objDBL.SQLGetDescription(sAC, sSql)
            Return GetImgPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCompanyNameCity(ByVal sAC As String, ByVal icompid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Company_Code,Company_Name,Company_Address,Company_City,Company_PinCode,Company_EmailID,Company_MobileNo,Company_TelephoneNo,Company_WebSite From Trace_CompanyDetails where Company_ID =" & icompid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetBranchDetails(ByVal sAC As String, ByVal icompid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select Company_Branch_Name,Company_Branch_Address,Company_Branch_Contact_Person,Company_Branch_Contact_MobileNo,Company_Branch_Contact_LandLineNo,Company_Branch_Contact_Email,Company_Branch_Designation From TRACe_CompanyBranchDetails where Company_Branch_CompanyID =" & icompid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Image(ByVal sAC As String, ByVal icompid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select CLS_FileName  From company_logo_settings where CLS_CompID =" & icompid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetEmployeedetails(ByVal sAC As String, ByVal icompid As Integer, ByVal ipartnerid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select usr_FullName,usr_OfficePhone,usr_PhoneNo From Sad_UserDetails where usr_Id =" & ipartnerid & " and Usr_CompId=" & icompid & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDateId(ByVal sAC As String, ByVal iACID As Integer) As String
        Dim sSql As String = ""
        Try
            sSql = "Select Sad_Config_Value from sad_config_settings Where Sad_Config_Key='DateFormat' And SAD_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BinALLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iCustId As Integer, ByVal iAuditId As Integer, ByVal iReporttypeId As Integer, ByVal iReportId As Integer) As DataTable
    '    Try
    '        Dim sSql As String
    '        Dim dtDetails As New DataTable
    '        Dim dt As New DataTable
    '        Dim dsDetails As New DataSet
    '        Dim drow As DataRow
    '        Try
    '            dtDetails.Columns.Add("Customer")
    '            dtDetails.Columns.Add("PKID")
    '            dtDetails.Columns.Add("Headingid")
    '            dtDetails.Columns.Add("ReportID")
    '            dtDetails.Columns.Add("ReportType")
    '            dtDetails.Columns.Add("Report")
    '            dtDetails.Columns.Add("Heading")
    '            dtDetails.Columns.Add("Description")
    '            dtDetails.Columns.Add("ReportTypeID")
    '            dtDetails.Columns.Add("Signedby")
    '            dtDetails.Columns.Add("ModuleID")
    '            dtDetails.Columns.Add("CustomerID")
    '            dtDetails.Columns.Add("AuditId")

    '            sSql = "Select RG_Id,RG_CustomerId,RG_Heading,RG_ReportType,RG_Report,RG_Description,RG_Signedby,RG_Module,RG_CustomerId,RG_AuditId from SAD_ReportGeneration "
    '            sSql = sSql & " Left Join SAD_Finalisation_Report_Template On TEM_FunctionId=RG_ReportType And TEM_ReportTitle=0 "
    '            sSql = sSql & " Where RG_Compid=" & iCompID & " And RG_YearId=" & iYearid & ""
    '            If iCustId > 0 Then
    '                sSql = sSql & " and RG_CustomerId =" & iCustId & ""
    '            End If
    '            If iReporttypeId > 0 Then
    '                sSql = sSql & " and RG_ReportType =" & iReporttypeId & ""
    '            End If
    '            If iReportId <> 0 Then
    '                sSql = sSql & " and RG_Report =" & iReportId & ""
    '            End If
    '            If iAuditId > 0 Then
    '                sSql = sSql & " and RG_AuditId =" & iAuditId & ""
    '            End If
    '            sSql = sSql & " Order by TEM_ContentId"
    '            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

    '            For i = 0 To dt.Rows.Count - 1
    '                drow = dtDetails.NewRow
    '                drow("PKID") = dt.Rows(i)("RG_Id")
    '                drow("Customer") = objDBL.SQLGetDescription(sNameSpace, "Select CUST_NAME From SAD_CUSTOMER_MASTER Where CUST_ID=" & dt.Rows(i)("RG_CustomerId") & " and CUST_CompID=" & iCompID & "")
    '                drow("Headingid") = dt.Rows(i)("RG_Heading")
    '                drow("ReportID") = dt.Rows(i)("RG_Report")
    '                drow("ReportType") = objDBL.SQLGetDescription(sNameSpace, "Select RCM_ReportName From SAD_ReportContentMaster Where RCM_ReportId=" & dt.Rows(i)("RG_ReportType") & " and RCM_CompID=" & iCompID & "")
    '                drow("Report") = dt.Rows(i)("RG_Report")

    '                If dt.Rows(i)("RG_Report") = 1 Then
    '                    drow("Report") = "Draft Audit Report"
    '                ElseIf dt.Rows(i)("RG_Report") = 2 Then
    '                    drow("Report") = "Executive Summary"
    '                ElseIf dt.Rows(i)("RG_Report") = 3 Then
    '                    drow("Report") = "Final Audit Report"
    '                ElseIf dt.Rows(i)("RG_Report") = 4 Then
    '                    drow("Report") = "Proposal"
    '                ElseIf dt.Rows(i)("RG_Report") = 5 Then
    '                    drow("Report") = "Proposal For Renewal"
    '                ElseIf dt.Rows(i)("RG_Report") = 6 Then
    '                    drow("Report") = "Acceptence Letter"
    '                ElseIf dt.Rows(i)("RG_Report") = 7 Then
    '                    drow("Report") = "Covering Letter"
    '                ElseIf dt.Rows(i)("RG_Report") = 8 Then
    '                    drow("Report") = "Management Representation"
    '                End If

    '                drow("Heading") = objDBL.SQLGetDescription(sNameSpace, "Select RCM_Heading From SAD_ReportContentMaster Where RCM_Id=" & dt.Rows(i)("RG_Heading") & " and RCM_CompID=" & iCompID & "")
    '                drow("Description") = dt.Rows(i)("RG_Description")
    '                drow("ReportTypeID") = dt.Rows(i)("RG_ReportType")
    '                drow("Signedby") = dt.Rows(i)("RG_Signedby")
    '                drow("ModuleID") = dt.Rows(i)("RG_Module")
    '                drow("CustomerID") = dt.Rows(i)("RG_CustomerId")
    '                drow("AuditId") = dt.Rows(i)("RG_AuditId")
    '                dtDetails.Rows.Add(drow)
    '            Next

    '            Return dtDetails
    '        Catch ex As Exception
    '            MsgBox(ex.Message, MsgBoxStyle.Information)
    '            Throw
    '        End Try
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function LoadDetails(ByVal sAC As String, ByVal iFunCtionId As Integer, ByVal sModule As String, ByVal sReport As String, ByVal sCustomerNAme As String, ByVal sYear As String, ByVal Total As Double, ByVal iProId As Integer)
    '    Dim dt As New DataTable, dtTab As New DataTable
    '    Dim dRow As DataRow
    '    Dim sStatus As String = "", sContentIDs As String = ""
    '    Dim ds As New DataSet
    '    Dim aArray As Array
    '    Dim i As Integer
    '    Try
    '        dt.Columns.Add("Heading")
    '        dt.Columns.Add("Details")
    '        If iProId = 0 Then
    '            sContentIDs = objDBL.SQLGetDescription(sAC, "Select TEM_ContentId From SAD_Finalisation_Report_Template Where  TEM_FunctionId = " & iFunCtionId & " And TEM_Module = '" & sModule & "' And TEM_ReportTitle = " & sReport & " And TEM_Delflag  = 'A'")
    '        Else
    '            sContentIDs = objDBL.SQLGetDescription(sAC, "Select TEM_ContentId From SAD_Finalisation_Report_Template Where  TEM_Module = '" & sModule & "' And TEM_ReportTitle = " & sReport & " And TEM_Delflag  = 'W'")
    '        End If
    '        If sContentIDs <> "" Then
    '            aArray = sContentIDs.Split(",")
    '            For i = 0 To UBound(aArray)
    '                If aArray(i) <> "" Or aArray(i) <> String.Empty Then
    '                    dRow = dt.NewRow()
    '                    dRow("Heading") = objDBL.SQLGetDescription(sAC, "Select RCM_Heading from SAD_ReportContentMaster where RCM_Id = " & aArray(i) & " And RCM_Delflag<>'D'")
    '                    dRow("Details") = objDBL.SQLGetDescription(sAC, "select RCM_Description from SAD_ReportContentMaster Where RCM_Id =  " & aArray(i) & " And RCM_Delflag<>'D'")
    '                    dt.Rows.Add(dRow)
    '                End If
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function LoadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iCustId As Integer, ByVal iAuditId As Integer, ByVal iReporttypeId As Integer, ByVal iReportId As Integer) As DataTable
    '    Dim dRow As DataRow
    '    Dim sStatus As String = "", sContentIDs As String = ""
    '    Dim ds As New DataSet
    '    Dim aArray As Array
    '    Dim i As Integer
    '    Dim dt, dt1 As New DataTable
    '    Dim sSql As String = ""
    '    Try
    '        dt1.Columns.Add("Heading")
    '        dt1.Columns.Add("Details")

    '        sSql = "Select RG_Heading,RG_Description from SAD_ReportGeneration "
    '        sSql = sSql & "Left Join SAD_Finalisation_Report_Template On TEM_FunctionId=RG_ReportType And TEM_ReportTitle=0"
    '        sSql = sSql & " Where RG_Compid=" & iCompID & " And RG_YearId=" & iYearid & ""
    '        If iCustId > 0 Then
    '            sSql = sSql & " and RG_CustomerId =" & iCustId & ""
    '        End If
    '        If iReporttypeId > 0 Then
    '            sSql = sSql & " and RG_ReportType =" & iReporttypeId & ""
    '        End If
    '        If iReportId > 0 Then
    '            sSql = sSql & " and RG_Report =" & iReportId & ""
    '        End If
    '        If iAuditId > 0 Then
    '            sSql = sSql & " and RG_AuditId =" & iAuditId & ""
    '        End If
    '        sSql = sSql & " Order by TEM_ContentId"
    '        dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
    '        For i = 0 To dt.Rows.Count - 1
    '            dRow = dt1.NewRow
    '            dRow("Heading") = objDBL.SQLGetDescription(sNameSpace, "Select RCM_Heading from SAD_ReportContentMaster where RCM_Id = " & dt.Rows(i)("RG_Heading") & " And RCM_Delflag<>'D'")
    '            dRow("Details") = dt.Rows(i)("RG_Description")
    '            dt1.Rows.Add(dRow)
    '        Next
    '        Return dt1
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Information)
    '        Throw
    '    End Try

    'End Function
    Public Function BinALLDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iCustId As Integer, ByVal iAuditId As Integer, ByVal iReporttypeId As Integer, ByVal iReportId As Integer) As DataTable
        Dim dtE As New DataTable
        Dim dtN As New DataTable
        Dim sSql As String = ""
        Try
            dtE.Columns.Add("ID")
            dtE.Columns.Add("Customer")
            dtE.Columns.Add("PKID")
            dtE.Columns.Add("Headingid")
            dtE.Columns.Add("ReportID")
            dtE.Columns.Add("ReportType")
            dtE.Columns.Add("Report")
            dtE.Columns.Add("Heading")
            dtE.Columns.Add("Description")
            dtE.Columns.Add("ReportTypeID")
            dtE.Columns.Add("Signedby")
            dtE.Columns.Add("ModuleID")
            dtE.Columns.Add("CustomerID")
            dtE.Columns.Add("AuditId")

            sSql = "SELECT RG_Id,CUST_NAME,RG_Heading,RG_Report,RCM_ReportName,RG_Report,RCM_Heading,RG_Description,RG_ReportType,RG_Signedby,RG_Module,RG_CustomerId,RG_AuditId FROM SAD_ReportGeneration "
            sSql &= " LEFT JOIN SAD_CUSTOMER_MASTER ON CUST_ID = RG_CustomerId And CUST_CompID=" & iCompID & ""
            sSql &= " LEFT JOIN SAD_ReportContentMaster ON RCM_ReportId=RG_ReportType and RCM_Id=RG_Heading and RCM_CompID=" & iCompID & ""
            sSql &= " WHERE RG_Compid=" & iCompID & " AND RG_YearId=" & iYearid & ""
            If iCustId > 0 Then sSql &= " AND RG_CustomerId=" & iCustId
            If iReporttypeId > 0 Then sSql &= " AND RG_ReportType=" & iReporttypeId
            If iReportId > 0 Then sSql &= " AND RG_Report=" & iReportId
            If iAuditId > 0 Then sSql &= " AND RG_AuditId=" & iAuditId
            Dim dt As DataTable = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            For Each row As DataRow In dt.Rows
                Dim drow As DataRow = dtE.NewRow
                With row
                    drow("ID") = .Item("RG_Heading")
                    drow("PKID") = .Item("RG_Id")
                    drow("Customer") = .Item("CUST_NAME")
                    drow("Headingid") = .Item("RG_Heading")
                    drow("ReportID") = .Item("RG_Report")
                    drow("ReportType") = .Item("RCM_ReportName")
                    drow("Report") = GetReportTypeName(.Item("RG_Report"))
                    drow("Heading") = .Item("RCM_Heading")
                    drow("Description") = .Item("RG_Description")
                    drow("ReportTypeID") = .Item("RG_ReportType")
                    drow("Signedby") = .Item("RG_Signedby")
                    drow("ModuleID") = .Item("RG_Module")
                    drow("CustomerID") = .Item("RG_CustomerId")
                    drow("AuditId") = .Item("RG_AuditId")
                End With
                dtE.Rows.Add(drow)
            Next

            dtN.Columns.Add("Customer")
            dtN.Columns.Add("PKID")
            dtN.Columns.Add("Headingid")
            dtN.Columns.Add("ReportID")
            dtN.Columns.Add("ReportType")
            dtN.Columns.Add("Report")
            dtN.Columns.Add("Heading")
            dtN.Columns.Add("Description")
            dtN.Columns.Add("ReportTypeID")
            dtN.Columns.Add("Signedby")
            dtN.Columns.Add("ModuleID")
            dtN.Columns.Add("CustomerID")
            dtN.Columns.Add("AuditId")

            Dim sContentIDs = objDBL.SQLGetDescription(sNameSpace, "SELECT TEM_ContentId FROM SAD_Finalisation_Report_Template WHERE TEM_FunctionId = " & iReporttypeId & " AND TEM_ReportTitle = " & iReportId & "")
            If sContentIDs <> "" Then
                Dim ids As String() = sContentIDs.Split(","c)
                For Each id In ids
                    Dim foundRow As DataRow = GetDataRowIfExists(dtE, Convert.ToInt32(id))
                    If foundRow IsNot Nothing Then
                        Dim newRow As DataRow = dtN.NewRow()
                        newRow("Customer") = foundRow("Customer")
                        newRow("PKID") = foundRow("PKID")
                        newRow("Headingid") = foundRow("Headingid")
                        newRow("ReportID") = foundRow("ReportID")
                        newRow("ReportType") = foundRow("ReportType")
                        newRow("Report") = foundRow("Report")
                        newRow("Heading") = foundRow("Heading")
                        newRow("Description") = foundRow("Description")
                        newRow("ReportTypeID") = foundRow("ReportTypeID")
                        newRow("Signedby") = foundRow("Signedby")
                        newRow("ModuleID") = foundRow("ModuleID")
                        newRow("CustomerID") = foundRow("CustomerID")
                        newRow("AuditId") = foundRow("AuditId")
                        dtN.Rows.Add(newRow)
                    End If
                Next
            End If
            Return dtN
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetReportTypeName(reportId As Integer) As String
        Select Case reportId
            Case 1
                Return "Draft Audit Report"
            Case 2
                Return "Executive Summary"
            Case 3
                Return "Final Audit Report"
            Case 4
                Return "Proposal"
            Case 5
                Return "Proposal For Renewal"
            Case 6
                Return "Acceptance Letter"
            Case 7
                Return "Covering Letter"
            Case 8
                Return "Management Representation"
            Case Else
                Return ""
        End Select
    End Function
    Public Function LoadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iCustId As Integer, ByVal iAuditId As Integer, ByVal iReporttypeId As Integer, ByVal iReportId As Integer) As DataTable
        Dim dtE As New DataTable
        Dim dtN As New DataTable
        Dim sSql As String = ""
        Try
            dtE.Columns.Add("ID", GetType(Integer))
            dtE.Columns.Add("Heading", GetType(String))
            dtE.Columns.Add("Details", GetType(String))

            sSql = "SELECT RG_Heading As ID,RCM_Heading As Heading,RG_Description As Details FROM SAD_ReportGeneration "
            sSql &= " LEFT JOIN SAD_ReportContentMaster ON RCM_ReportId=RG_ReportType and RCM_Id=RG_Heading and RCM_CompID=" & iCompID & ""
            sSql &= " WHERE RG_Compid = " & iCompID & " AND RG_YearId = " & iYearid & ""
            If iCustId > 0 Then sSql &= " AND RG_CustomerId = " & iCustId
            If iReporttypeId > 0 Then sSql &= " AND RG_ReportType = " & iReporttypeId
            If iReportId > 0 Then sSql &= " AND RG_Report = " & iReportId
            If iAuditId > 0 Then sSql &= " AND RG_AuditId = " & iAuditId
            dtE = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            dtN.Columns.Add("Heading", GetType(String))
            dtN.Columns.Add("Details", GetType(String))

            Dim sContentIDs = objDBL.SQLGetDescription(sNameSpace, "SELECT TEM_ContentId FROM SAD_Finalisation_Report_Template WHERE TEM_FunctionId = " & iReporttypeId & " AND TEM_ReportTitle = " & iReportId & "")
            If sContentIDs <> "" Then
                Dim ids As String() = sContentIDs.Split(","c)
                For Each id In ids
                    Dim foundRow As DataRow = GetDataRowIfExists(dtE, Convert.ToInt32(id))
                    If foundRow IsNot Nothing Then
                        Dim newRow As DataRow = dtN.NewRow()
                        newRow("Heading") = foundRow("Heading")
                        newRow("Details") = foundRow("Details")
                        dtN.Rows.Add(newRow)
                    End If
                Next
            End If
            Return dtN
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetDataRowIfExists(dt As DataTable, searchID As Integer) As DataRow
        Dim rows As DataRow() = dt.Select($"ID = {searchID}")
        Return If(rows.Length > 0, rows(0), Nothing)
    End Function
    Public Function LoadAuditReportInAuditCompletionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer, ByVal iCustId As Integer, ByVal iAuditId As Integer) As DataTable
        Dim dt As New DataTable, dtN As New DataTable
        Dim sSql As String = ""
        Dim iReportId As Integer = 0
        Try

            dtN.Columns.Add("Heading", GetType(String))
            dtN.Columns.Add("Details", GetType(String))

            sSql = "Select Distinct(RG_ReportType) As ReporttypeId From SAD_ReportGeneration WHERE RG_Compid = " & iCompID & " AND RG_YearId = " & iYearid & " And RG_AuditId = " & iAuditId & " AND RG_CustomerId = " & iCustId & " Order by RG_ReportType"
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dt.Rows.Count - 1
                Dim iReporttypeId As Integer = dt.Rows(i)("ReporttypeId")
                Dim newRow As DataRow = dtN.NewRow()
                Select Case iReporttypeId
                    Case 1
                        newRow("Heading") = "Report on the standalone Financial Statements"
                    Case 2
                        newRow("Heading") = "Independent Auditor's Report"
                    Case 3
                        newRow("Heading") = "Annexure A to the Independent Auditor's Report"
                    Case 4
                        newRow("Heading") = "Annexure B to the Independent Auditor's Report"
                    Case 5
                        newRow("Heading") = "LOE and Information about the Auditee Report"
                    Case 6
                        newRow("Heading") = "Management Representation Letter2"
                    Case 7
                        newRow("Heading") = "Management Representation Letter2"
                    Case Else
                        newRow("Heading") = ""
                End Select
                newRow("Details") = ""
                dtN.Rows.Add(newRow)

                Dim dtE As New DataTable
                dtE.Columns.Add("ID", GetType(Integer))
                dtE.Columns.Add("Heading", GetType(String))
                dtE.Columns.Add("Details", GetType(String))

                sSql = "SELECT RG_Heading As ID,RCM_Heading As Heading,RG_Description As Details FROM SAD_ReportGeneration "
                sSql &= " LEFT JOIN SAD_ReportContentMaster ON RCM_ReportId=RG_ReportType and RCM_Id=RG_Heading and RCM_CompID=" & iCompID & ""
                sSql &= " WHERE RG_Compid = " & iCompID & " AND RG_YearId = " & iYearid & ""
                If iCustId > 0 Then sSql &= " AND RG_CustomerId = " & iCustId
                If iReporttypeId > 0 Then sSql &= " AND RG_ReportType = " & iReporttypeId
                If iReportId > 0 Then sSql &= " AND RG_Report = " & iReportId
                If iAuditId > 0 Then sSql &= " AND RG_AuditId = " & iAuditId
                dtE = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

                Dim sContentIDs = objDBL.SQLGetDescription(sNameSpace, "SELECT TEM_ContentId FROM SAD_Finalisation_Report_Template WHERE TEM_FunctionId = " & iReporttypeId & " AND TEM_ReportTitle = " & iReportId & "")
                If sContentIDs <> "" Then
                    Dim ids As String() = sContentIDs.Split(","c)
                    For Each id In ids
                        Dim foundRow As DataRow = GetDataRowIfExists(dtE, Convert.ToInt32(id))
                        If foundRow IsNot Nothing Then
                            newRow = dtN.NewRow()
                            newRow("Heading") = foundRow("Heading")
                            newRow("Details") = foundRow("Details")
                            dtN.Rows.Add(newRow)
                        End If
                    Next
                End If
            Next
            Return dtN
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
