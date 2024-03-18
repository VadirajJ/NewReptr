Imports DatabaseLayer
Public Structure str_KRI
    Private KRI_PKID As Integer
    Private KRI_CategoryID As Integer
    Private KRI_RiskID As Integer
    Private KRI_SubCategoryID As Integer
    Private KRI_RiskDescription As String
    Private KRI_PeriodID As Integer
    Private KRI_MeasureID As Integer
    Private KRI_AttachID As Integer
    Private KRI_DelFlag As String
    Private KRI_STATUS As String
    Private KRI_CrBy As Integer
    Private KRI_CrOn As Date
    Private KRI_IPAddress As String
    Private KRI_CompId As Integer
    Private KRI_YearID As Integer
    Public Property iKRI_PKID() As Integer
        Get
            Return (KRI_PKID)
        End Get
        Set(ByVal Value As Integer)
            KRI_PKID = Value
        End Set
    End Property
    Public Property iKRI_CategoryID() As Integer
        Get
            Return (KRI_CategoryID)
        End Get
        Set(ByVal Value As Integer)
            KRI_CategoryID = Value
        End Set
    End Property
    Public Property iKRI_RiskID() As Integer
        Get
            Return (KRI_RiskID)
        End Get
        Set(ByVal Value As Integer)
            KRI_RiskID = Value
        End Set
    End Property
    Public Property iKRI_SubCategoryID() As Integer
        Get
            Return (KRI_SubCategoryID)
        End Get
        Set(ByVal Value As Integer)
            KRI_SubCategoryID = Value
        End Set
    End Property
    Public Property sKRI_RiskDescription() As String
        Get
            Return (KRI_RiskDescription)
        End Get
        Set(ByVal Value As String)
            KRI_RiskDescription = Value
        End Set
    End Property
    Public Property iKRI_PeriodID() As Integer
        Get
            Return (KRI_PeriodID)
        End Get
        Set(ByVal Value As Integer)
            KRI_PeriodID = Value
        End Set
    End Property
    Public Property iKRI_MeasureID() As Integer
        Get
            Return (KRI_MeasureID)
        End Get
        Set(ByVal Value As Integer)
            KRI_MeasureID = Value
        End Set
    End Property
    Public Property iKRI_AttachID() As Integer
        Get
            Return (KRI_AttachID)
        End Get
        Set(ByVal Value As Integer)
            KRI_AttachID = Value
        End Set
    End Property
    Public Property sKRI_DelFlag() As String
        Get
            Return (KRI_DelFlag)
        End Get
        Set(ByVal Value As String)
            KRI_DelFlag = Value
        End Set
    End Property
    Public Property sKRI_STATUS() As String
        Get
            Return (KRI_STATUS)
        End Get
        Set(ByVal Value As String)
            KRI_STATUS = Value
        End Set
    End Property
    Public Property iKRI_CrBy() As Integer
        Get
            Return (KRI_CrBy)
        End Get
        Set(ByVal Value As Integer)
            KRI_CrBy = Value
        End Set
    End Property
    Public Property dKRI_CrOn() As Date
        Get
            Return (KRI_CrOn)
        End Get
        Set(ByVal Value As Date)
            KRI_CrOn = Value
        End Set
    End Property
    Public Property sKRI_IPAddress() As String
        Get
            Return (KRI_IPAddress)
        End Get
        Set(ByVal Value As String)
            KRI_IPAddress = Value
        End Set
    End Property
    Public Property iKRI_CompId() As Integer
        Get
            Return (KRI_CompId)
        End Get
        Set(ByVal Value As Integer)
            KRI_CompId = Value
        End Set
    End Property
    Public Property iKRI_YearID() As Integer
        Get
            Return (KRI_YearID)
        End Get
        Set(ByVal Value As Integer)
            KRI_YearID = Value
        End Set
    End Property
End Structure
Public Class clsKRI
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function SaveKRIDetails(ByVal sAC As String, ByVal objKRI As str_KRI)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKRI.iKRI_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_CategoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKRI.iKRI_CategoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_RiskID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objKRI.iKRI_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_SubCategoryID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objKRI.iKRI_SubCategoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_RiskDescription", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objKRI.sKRI_RiskDescription
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_PeriodID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objKRI.iKRI_PeriodID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_MeasureID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objKRI.iKRI_MeasureID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_AttachID", OleDb.OleDbType.Integer, 15)
            ObjParam(iParamCount).Value = objKRI.iKRI_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_DelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objKRI.sKRI_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_STATUS", OleDb.OleDbType.VarChar, 3)
            ObjParam(iParamCount).Value = objKRI.sKRI_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKRI.iKRI_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objKRI.sKRI_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_CompId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKRI.iKRI_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@KRI_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objKRI.iKRI_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_KRI", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKRIDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("PKID", GetType(Integer))
            dtTab.Columns.Add("Category")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("SubCategory")
            dtTab.Columns.Add("RiskDescription")
            dtTab.Columns.Add("Period")
            dtTab.Columns.Add("Measure")
            dtTab.Columns.Add("YTD15")
            dtTab.Columns.Add("July15")
            dtTab.Columns.Add("May16")
            dtTab.Columns.Add("June16")
            dtTab.Columns.Add("July16")
            dtTab.Columns.Add("YTD16")
            sSql = "Select KRI_PKID,a.RAM_Name,MRL_RiskName,b.RAM_Name as Sub_Category,KRI_RiskDescription,c.RAM_Name as Period,d.RAM_Name as Measure,KRI_AttachID"
            sSql = sSql & " from Risk_KRI left join Risk_GeneralMaster a on  KRI_CategoryID=a.RAM_PKID And a.RAM_CompID=" & iACID & " left join MST_RISK_Library on"
            sSql = sSql & " KRI_RISKID=MRL_PKID And MRL_CompID=" & iACID & " left join Risk_GeneralMaster as b on KRI_SubCategoryID=b.RAM_PKID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " left join Risk_GeneralMaster as c  on KRI_PeriodID=c.RAM_PKID And c.RAM_CompID=" & iACID & " left join Risk_GeneralMaster as d on "
            sSql = sSql & " KRI_MeasureID=d.RAM_PKID And d.RAM_CompID=" & iACID & " where KRI_YearID=" & iYearID & " and KRI_CompID=" & iACID & " and KRI_DelFlag='A'"
            sSql = sSql & " order by KRI_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("KRI_PKID")) = False Then
                    dr("PKID") = dt.Rows(i)("KRI_PKID")
                End If
                If IsDBNull(dt.Rows(i)("RAM_Name")) = False Then
                    dr("Category") = dt.Rows(i)("RAM_Name")
                End If
                If IsDBNull(dt.Rows(i)("MRL_RiskName")) = False Then
                    dr("Risk") = dt.Rows(i)("MRL_RiskName")
                End If
                If IsDBNull(dt.Rows(i)("Sub_Category")) = False Then
                    dr("SubCategory") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Sub_Category"))
                End If
                If IsDBNull(dt.Rows(i)("KRI_RiskDescription")) = False Then
                    dr("RiskDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("KRI_RiskDescription"))
                End If
                If IsDBNull(dt.Rows(i)("Period")) = False Then
                    dr("Period") = dt.Rows(i)("Period")
                End If
                If IsDBNull(dt.Rows(i)("Measure")) = False Then
                    dr("Measure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Measure"))
                End If
                dr("YTD15") = ""
                dr("July15") = ""
                dr("May16") = ""
                dr("June16") = ""
                dr("July16") = ""
                dr("YTD16") = ""
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveKRIAttahment(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal IKRIID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Risk_KRI set KRI_AttachID=" & iAttachID & " Where KRI_CompId=" & iACID & " And KRI_PKID=" & IKRIID & " and KRI_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub DeleteKRI(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal IKRIID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Risk_KRI set KRI_DelFlag='D',KRI_Status='AD',KRI_DeletedBy=" & iSessionUsrID & ",KRI_DeletedOn=Getdate()"
            sSql = sSql & " Where KRI_CompId=" & iACID & " And KRI_PKID=" & IKRIID & " and KRI_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ActivateKRI(ByVal sAC As String, ByVal iACID As Integer, ByVal IKRIID As Integer, ByVal iYearID As Integer)
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Update Risk_KRI set KRI_DelFlag='A',KRI_Status='C' Where KRI_CompId=" & iACID & " And KRI_PKID=" & IKRIID & " and KRI_YearID=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckKRIAttachID(ByVal sAC As String, ByVal iACID As Integer, ByVal IKRIID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select KRI_AttachID from Risk_KRI Where KRI_CompId=" & iACID & " And KRI_PKID=" & IKRIID & " and KRI_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckKRIStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal IKRIID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select KRI_PKID from Risk_KRI Where KRI_CompId=" & iACID & " And KRI_PKID=" & IKRIID & " And KRI_DelFlag='D' and KRI_YearID=" & iYearID & ""
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKIRAttachments(ByVal iDateFormatID As Integer, ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As DataSet
        Dim sSql As String
        Dim dt As New DataTable, dtAttach As New DataTable
        Dim dsAttach As New DataSet
        Dim drow As DataRow
        Try
            dtAttach.Columns.Add("SrNo")
            dtAttach.Columns.Add("PKID")
            dtAttach.Columns.Add("AttachID")
            dtAttach.Columns.Add("AtchID")
            dtAttach.Columns.Add("FName")
            dtAttach.Columns.Add("FDescription")
            dtAttach.Columns.Add("CreatedBy")
            dtAttach.Columns.Add("CreatedOn")
            dtAttach.Columns.Add("FileSize")

            sSql = "Select ATCH_ID,Atch_DocID,ATCH_FNAME,ATCH_EXT,ATCH_Desc,ATCH_CreatedBy,ATCH_CREATEDON,ATCH_SIZE,KRI_PKID,KRI_AttachID "
            sSql = sSql & " From Risk_KRI left join edt_attachments on ATCH_ID=KRI_AttachID where ATCH_CompID=" & iACID & " And KRI_PKID=" & iPKID & " And ATCH_Status <> 'D' Order by ATCH_CREATEDON"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                drow = dtAttach.NewRow
                drow("SrNo") = i + 1
                drow("PKID") = dt.Rows(i)("KRI_PKID")
                drow("AttachID") = dt.Rows(i)("ATCH_ID")
                drow("AtchID") = dt.Rows(i)("Atch_DocID")
                drow("FName") = dt.Rows(i)("ATCH_FNAME") & "." & dt.Rows(i)("ATCH_EXT")
                If IsDBNull(dt.Rows(i)("ATCH_Desc")) = False Then
                    drow("FDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ATCH_Desc"))
                Else
                    drow("FDescription") = ""
                End If
                drow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, dt.Rows(i)("ATCH_CreatedBy"))
                drow("CreatedOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("ATCH_CREATEDON"), "F")
                drow("FileSize") = String.Format("{0:0.00}", (dt.Rows(i)("ATCH_SIZE") / 1024)) & " KB"
                dtAttach.Rows.Add(drow)
            Next
            dsAttach.Tables.Add(dtAttach)
            Return dsAttach
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
            Throw
        End Try
    End Function
    Public Function GetPreviousMonth(ByVal sAC As String, ByVal iMonth As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select DateName(m,DATEADD(mm,-" & iMonth & ",GETDATE()))"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKRIDetailsReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dtTab As New DataTable
        Dim sStrCurrentYear As String, sStrPreviousYear As String, sMonth As String
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("PKID", GetType(Integer))
            dtTab.Columns.Add("Category")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("SubCategory")
            dtTab.Columns.Add("RiskDescription")
            dtTab.Columns.Add("Period")
            dtTab.Columns.Add("Measure")
            dtTab.Columns.Add("HeadingYTDPY")
            dtTab.Columns.Add("HeadingMonthPY")
            dtTab.Columns.Add("HeadingPPMonthCY")
            dtTab.Columns.Add("HeadingPMonthCY")
            dtTab.Columns.Add("HeadingMonthCY")
            dtTab.Columns.Add("HeadingYTDCY")
            dtTab.Columns.Add("YTDPY")
            dtTab.Columns.Add("MonthPY")
            dtTab.Columns.Add("PPMonthCY")
            dtTab.Columns.Add("PMonthCY")
            dtTab.Columns.Add("MonthCY")
            dtTab.Columns.Add("YTDCY")
            sSql = "Select KRI_PKID,a.RAM_Name,MRL_RIskDesc,b.RAM_Name as Sub_Category,KRI_RiskDescription,c.RAM_Name as Period,d.RAM_Name as Measure,KRI_AttachID"
            sSql = sSql & " from Risk_KRI left join Risk_GeneralMaster a on  KRI_CategoryID=a.RAM_PKID And a.RAM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_RISK_Library on KRI_RISKID=MRL_PKID And MRL_CompID=" & iACID & " left join Risk_GeneralMaster as b on KRI_SubCategoryID=b.RAM_PKID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " left join Risk_GeneralMaster as c  on KRI_PeriodID=c.RAM_PKID And c.RAM_CompID=" & iACID & " left join Risk_GeneralMaster as d on KRI_MeasureID=d.RAM_PKID And d.RAM_CompID=" & iACID & ""
            sSql = sSql & " where KRI_YearID=" & iYearID & " and KRI_CompID=" & iACID & " and KRI_DelFlag='A' order by KRI_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)

            sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
            sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
            sMonth = objclsGeneralFunctions.GetCurrentMonthName(sAC)
            sSql = "Select DateName(m,DATEADD(mm,-1,GETDATE()))"
            Dim PrevMonth As String = objDBL.SQLExecuteScalar(sAC, sSql)
            sSql = "Select DateName(m,DATEADD(mm,-2,GETDATE()))"
            Dim PreviousMonth As String = objDBL.SQLExecuteScalar(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("KRI_PKID")) = False Then
                    dr("PKID") = dt.Rows(i)("KRI_PKID")
                End If
                If IsDBNull(dt.Rows(i)("RAM_Name")) = False Then
                    dr("Category") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAM_Name"))
                End If
                If IsDBNull(dt.Rows(i)("MRL_RIskDesc")) = False Then
                    dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MRL_RIskDesc"))
                End If
                If IsDBNull(dt.Rows(i)("Sub_Category")) = False Then
                    dr("SubCategory") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Sub_Category"))
                End If
                If IsDBNull(dt.Rows(i)("KRI_RiskDescription")) = False Then
                    dr("RiskDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("KRI_RiskDescription"))
                End If
                If IsDBNull(dt.Rows(i)("Period")) = False Then
                    dr("Period") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Period"))
                End If
                If IsDBNull(dt.Rows(i)("Measure")) = False Then
                    dr("Measure") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Measure"))
                End If
                dr("HeadingYTDPY") = "YTD" & " " & sStrPreviousYear & ""
                dr("HeadingMonthPY") = sMonth & " " & sStrPreviousYear & ""
                dr("HeadingPPMonthCY") = PreviousMonth & " " & sStrCurrentYear & ""
                dr("HeadingPMonthCY") = PrevMonth & " " & sStrCurrentYear & ""
                dr("HeadingMonthCY") = sMonth & " " & sStrCurrentYear & ""
                dr("HeadingYTDCY") = "YTD" & " " & sStrCurrentYear & ""

                dr("YTDPY") = ""
                dr("MonthPY") = ""
                dr("PPMonthCY") = ""
                dr("PMonthCY") = ""
                dr("MonthCY") = ""
                dr("YTDCY") = ""
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
