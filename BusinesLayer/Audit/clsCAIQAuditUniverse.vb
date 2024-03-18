Public Class clsCAIQAuditUniverse
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    'section
    Private CAS_ID As Integer
    Private CAS_CODE As String
    Private CAS_SECTIONNAME As String
    Private CAS_POINTS As Integer
    Private CAS_Desc As String
    Private CAS_DELFLG As String
    Private CAS_CRBY As Integer
    Private CAS_STATUS As String
    Private CAS_UPDATEDBY As Integer
    Private CAS_IPAddress As String
    Private CAS_CompId As String
    Private CAS_YEARId As String

    'SubSection
    Private CASU_ID As Integer
    Private CASU_CODE As String
    Private CASU_SUBSECTIONNAME As String
    Private CASU_SECTIONID As Integer
    Private CASU_Points As Integer
    Private CASU_Desc As String
    Private CASU_DELFLG As String
    Private CASU_CRBY As Integer
    Private CASU_STATUS As String
    Private CASU_UPDATEDBY As String
    Private CASU_IPAddress As String
    Private CASU_CompId As Integer
    Private CASU_YEARId As Integer

    'Process
    Private CAP_ID As Integer
    Private CAP_CODE As String
    Private CAP_PROCESSNAME As String
    Private CAP_POINTS As Integer
    Private CAP_SECTIONID As Integer
    Private CAP_SUBSECTIONID As Integer
    Private CAP_Desc As String
    Private CAP_DELFLG As String
    Private CAP_CRBY As Integer
    Private CAP_STATUS As String
    Private CAP_UPDATEDBY As Integer
    Private CAP_IPAddress As String
    Private CAP_CompId As Integer
    Private CAP_YEARId As Integer

    'SubProcess
    Private CASP_ID As Integer
    Private CASP_CODE As String
    Private CASP_SUBPROCESSNAME As String
    Private CASP_POINTS As Integer
    Private CASP_SECTIONID As Integer
    Private CASP_SUBSECTIONID As Integer
    Private CASP_PROCESSID As Integer
    Private CASP_Desc As String
    Private CASP_DELFLG As String
    Private CASP_CRBY As Integer
    Private CASP_STATUS As String
    Private CASP_UPDATEDBY As Integer
    Private CASP_IPAddress As String
    Private CASP_CompId As Integer
    Private CASP_YEARId As Integer

    Public Property iId() As Integer
        Get
            Return (CAS_ID)
        End Get
        Set(ByVal Value As Integer)
            CAS_ID = Value
        End Set
    End Property

    Public Property SECTCODE() As String
        Get
            Return (CAS_CODE)
        End Get
        Set(ByVal Value As String)
            CAS_CODE = Value
        End Set
    End Property
    Public Property SECTNAME() As String
        Get
            Return (CAS_SECTIONNAME)
        End Get
        Set(ByVal Value As String)
            CAS_SECTIONNAME = Value
        End Set
    End Property
    Public Property SECTPOINTS() As Integer
        Get
            Return (CAS_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CAS_POINTS = Value
        End Set
    End Property
    Public Property SECTDESC() As String
        Get
            Return (CAS_Desc)
        End Get
        Set(ByVal Value As String)
            CAS_Desc = Value
        End Set
    End Property
    Public Property SECTDELFLAG() As String
        Get
            Return (CAS_DELFLG)
        End Get
        Set(ByVal Value As String)
            CAS_DELFLG = Value
        End Set
    End Property

    Public Property SECTCRBY() As Integer
        Get
            Return (CAS_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CAS_CRBY = Value
        End Set
    End Property
    Public Property SECTSTATUS() As String
        Get
            Return (CAS_STATUS)
        End Get
        Set(ByVal Value As String)
            CAS_STATUS = Value
        End Set
    End Property
    Public Property SECTUPDATEDBY() As Integer
        Get
            Return (CAS_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CAS_UPDATEDBY = Value
        End Set
    End Property

    Public Property SECTIPADDRESS() As String
        Get
            Return (CAS_IPAddress)
        End Get
        Set(ByVal Value As String)
            CAS_IPAddress = Value
        End Set
    End Property

    Public Property SECTCOMPID As Integer
        Get
            Return (CAS_CompId)
        End Get
        Set(ByVal Value As Integer)
            CAS_CompId = Value
        End Set
    End Property

    Public Property SECTYEARID() As Integer
        Get
            Return (CAS_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CAS_YEARId = Value
        End Set
    End Property

    'subsection
    Public Property sSectID() As Integer
        Get
            Return (CASU_ID)
        End Get
        Set(ByVal Value As Integer)
            CASU_ID = Value
        End Set
    End Property
    Public Property sSECTCODE() As String
        Get
            Return (CASU_CODE)
        End Get
        Set(ByVal Value As String)
            CASU_CODE = Value
        End Set
    End Property
    Public Property sSECTNAME() As String
        Get
            Return (CASU_SUBSECTIONNAME)
        End Get
        Set(ByVal Value As String)
            CASU_SUBSECTIONNAME = Value
        End Set
    End Property
    Public Property sSECTPOINTS() As Integer
        Get
            Return (CASU_Points)
        End Get
        Set(ByVal Value As Integer)
            CASU_Points = Value
        End Set
    End Property
    Public Property iSecID() As Integer
        Get
            Return (CASU_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASU_SECTIONID = Value
        End Set
    End Property
    Public Property sSECTDESC() As String
        Get
            Return (CASU_Desc)
        End Get
        Set(ByVal Value As String)
            CASU_Desc = Value
        End Set
    End Property
    Public Property sSECTDELFLAG() As String
        Get
            Return (CASU_DELFLG)
        End Get
        Set(ByVal Value As String)
            CASU_DELFLG = Value
        End Set
    End Property

    Public Property sSECTCRBY() As Integer
        Get
            Return (CASU_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CASU_CRBY = Value
        End Set
    End Property
    Public Property sSECTSTATUS() As String
        Get
            Return (CASU_STATUS)
        End Get
        Set(ByVal Value As String)
            CASU_STATUS = Value
        End Set
    End Property
    Public Property sSECTUPDATEDBY() As Integer
        Get
            Return (CASU_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CASU_UPDATEDBY = Value
        End Set
    End Property

    Public Property sSECTIPADDRESS() As String
        Get
            Return (CASU_IPAddress)
        End Get
        Set(ByVal Value As String)
            CASU_IPAddress = Value
        End Set
    End Property

    Public Property sSECTCOMPID() As Integer
        Get
            Return (CASU_CompId)
        End Get
        Set(ByVal Value As Integer)
            CASU_CompId = Value
        End Set
    End Property

    Public Property sSECTYEARID() As Integer
        Get
            Return (CASU_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CASU_YEARId = Value
        End Set
    End Property
    'Process
    Public Property iCAP_ID() As Integer
        Get
            Return (CAP_ID)
        End Get
        Set(ByVal Value As Integer)
            CAP_ID = Value
        End Set
    End Property
    Public Property sCAP_CODE() As String
        Get
            Return (CAP_CODE)
        End Get
        Set(ByVal Value As String)
            CAP_CODE = Value
        End Set
    End Property
    Public Property iCAP_PNAME() As String
        Get
            Return (CAP_PROCESSNAME)
        End Get
        Set(ByVal Value As String)
            CAP_PROCESSNAME = Value
        End Set
    End Property
    Public Property iCAP_Points() As Integer
        Get
            Return (CAP_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CAP_POINTS = Value
        End Set
    End Property
    Public Property iCAP_SECID() As Integer
        Get
            Return (CAP_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CAP_SECTIONID = Value
        End Set
    End Property
    Public Property iCAP_SubSECID() As Integer
        Get
            Return (CAP_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CAP_SUBSECTIONID = Value
        End Set
    End Property
    Public Property sCAP_Desc() As String
        Get
            Return (CAP_Desc)
        End Get
        Set(ByVal Value As String)
            CAP_Desc = Value
        End Set
    End Property
    Public Property sCAP_Delflag() As String
        Get
            Return (CAP_DELFLG)
        End Get
        Set(ByVal Value As String)
            CAP_DELFLG = Value
        End Set
    End Property
    Public Property iCAP_CRBY() As Integer
        Get
            Return (CAP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CAP_CRBY = Value
        End Set
    End Property
    Public Property sCAP_STATUS() As String
        Get
            Return (CAP_STATUS)
        End Get
        Set(ByVal Value As String)
            CAP_STATUS = Value
        End Set
    End Property
    Public Property iCAP_UPDATEDBY() As Integer
        Get
            Return (CAP_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CAP_UPDATEDBY = Value
        End Set
    End Property
    Public Property iCAP_IPAddress() As String
        Get
            Return (CAP_IPAddress)
        End Get
        Set(ByVal Value As String)
            CAP_IPAddress = Value
        End Set
    End Property
    Public Property iCAP_CompId() As Integer
        Get
            Return (CAP_CompId)
        End Get
        Set(ByVal Value As Integer)
            CAP_CompId = Value
        End Set
    End Property
    Public Property iCAP_YEARId() As Integer
        Get
            Return (CAP_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CAP_YEARId = Value
        End Set
    End Property

    'Sub Process
    Public Property iCASP_ID() As Integer
        Get
            Return (CASP_ID)
        End Get
        Set(ByVal Value As Integer)
            CASP_ID = Value
        End Set
    End Property
    Public Property sCASP_CODE() As String
        Get
            Return (CASP_CODE)
        End Get
        Set(ByVal Value As String)
            CASP_CODE = Value
        End Set
    End Property
    Public Property sCASP_SUBPNAME() As String
        Get
            Return (CASP_SUBPROCESSNAME)
        End Get
        Set(ByVal Value As String)
            CASP_SUBPROCESSNAME = Value
        End Set
    End Property
    Public Property iCASP_POINTS() As Integer
        Get
            Return (CASP_POINTS)
        End Get
        Set(ByVal Value As Integer)
            CASP_POINTS = Value
        End Set
    End Property
    Public Property iCASP_SECID() As Integer
        Get
            Return (CASP_SECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASP_SECTIONID = Value
        End Set
    End Property
    Public Property iCASP_SubSECID() As Integer
        Get
            Return (CASP_SUBSECTIONID)
        End Get
        Set(ByVal Value As Integer)
            CASP_SUBSECTIONID = Value
        End Set
    End Property
    Public Property iCASP_ProID() As Integer
        Get
            Return (CASP_PROCESSID)
        End Get
        Set(ByVal Value As Integer)
            CASP_PROCESSID = Value
        End Set
    End Property
    Public Property sCASP_Desc() As String
        Get
            Return (CASP_Desc)
        End Get
        Set(ByVal Value As String)
            CASP_Desc = Value
        End Set
    End Property
    Public Property sCASP_DELFLG() As String
        Get
            Return (CASP_DELFLG)
        End Get
        Set(ByVal Value As String)
            CASP_DELFLG = Value
        End Set
    End Property
    Public Property iCASP_CRBY() As Integer
        Get
            Return (CASP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            CASP_CRBY = Value
        End Set
    End Property
    Public Property sCASP_STATUS() As String
        Get
            Return (CASP_STATUS)
        End Get
        Set(ByVal Value As String)
            CASP_STATUS = Value
        End Set
    End Property
    Public Property iCASP_UPDATEDBY() As Integer
        Get
            Return (CASP_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            CASP_UPDATEDBY = Value
        End Set
    End Property
    Public Property sCASP_IPAddress() As String
        Get
            Return (CASP_IPAddress)
        End Get
        Set(ByVal Value As String)
            CASP_IPAddress = Value
        End Set
    End Property
    Public Property iCASP_CompId() As Integer
        Get
            Return (CASP_CompId)
        End Get
        Set(ByVal Value As Integer)
            CASP_CompId = Value
        End Set
    End Property
    Public Property iCASP_YearId() As Integer
        Get
            Return (CASP_YEARId)
        End Get
        Set(ByVal Value As Integer)
            CASP_YEARId = Value
        End Set
    End Property

    Public Function LoadAllSections(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("id")
            dtTab.Columns.Add("SectionCode")
            dtTab.Columns.Add("SectionName")
            dtTab.Columns.Add("SectionDesc")
            dtTab.Columns.Add("SectionPoints")
            dtTab.Columns.Add("Status")

            sSql = "Select CAS_ID,CAS_CODE,CAS_SECTIONNAME,CAS_DELFLG,CAS_POINTS,CAS_Desc,CAS_POINTS From CRPA_section where CAS_ID=5 and CAS_CompId =" & iACID & ""

            If iStatus = 0 Then
                sSql = sSql & " and CAS_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " and CAS_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " and CAS_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CAS_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("id") = dt.Rows(i)("CAS_ID")
                dRow("SectionCode") = dt.Rows(i)("CAS_CODE")
                dRow("SectionName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_SECTIONNAME"))
                dRow("SectionDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_DESC"))
                dRow("SectionPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_POINTS"))
                If dt.Rows(i)("CAS_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("CAS_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CAS_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function loadSectiongrid(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal iID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("id")
            dtTab.Columns.Add("SectionID")
            dtTab.Columns.Add("SectionCode")
            dtTab.Columns.Add("SectionName")
            dtTab.Columns.Add("SectionDesc")
            dtTab.Columns.Add("SectionPoints")
            dtTab.Columns.Add("Status")

            sSql = "Select CAS_ID,CAS_CODE,CAS_SECTIONNAME,CAS_DELFLG,CAS_POINTS,CAS_Desc,CAS_POINTS From CRPA_section where"

            If iID > 0 Then
                sSql = sSql & " CAS_ID =" & iID & " and"
            End If
            If iStatus = 0 Then
                sSql = sSql & " CAS_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " CAS_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " CAS_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CAS_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("sectionID") = dt.Rows(i)("CAS_ID")
                dRow("SectionCode") = dt.Rows(i)("CAS_CODE")
                dRow("SectionName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_SECTIONNAME"))
                dRow("SectionDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_DESC"))
                dRow("SectionPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAS_POINTS"))
                If dt.Rows(i)("CAS_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("CAS_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CAS_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function loadSubSectiongrid(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal isSId As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("subsid")
            dtTab.Columns.Add("sSectionID")
            dtTab.Columns.Add("sSectionCode")
            dtTab.Columns.Add("sSectionName")
            dtTab.Columns.Add("sid")
            dtTab.Columns.Add("sSectionPoints")
            dtTab.Columns.Add("sSectionDesc")
            dtTab.Columns.Add("Status")

            sSql = "Select CASU_ID,CASU_CODE,CASU_SUBSECTIONNAME,CASU_SECTIONID ,CASU_DELFLG,CASU_POINTS,CASU_Desc,CASU_POINTS From CRPA_SubSection"

            If isSId > 0 Then
                sSql = sSql & " where CASU_ID =" & isSId & ""
            End If
            If iStatus = 0 Then
                sSql = sSql & "and CASU_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & "and CASU_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & "and CASU_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CASU_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("sSectionID") = dt.Rows(i)("CASU_ID")
                dRow("sSectionCode") = dt.Rows(i)("CASU_CODE")
                dRow("sSectionName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_SUBSECTIONNAME"))
                dRow("sid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_SECTIONID"))
                dRow("sSectionPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_POINTS"))
                dRow("sSectionDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_DESC"))
                If dt.Rows(i)("CASU_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("CASU_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CASU_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function loadProcessgrid(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal iPId As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Pid")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("ProcessCode")
            dtTab.Columns.Add("ProcessName")
            dtTab.Columns.Add("sid")
            dtTab.Columns.Add("ssid")
            dtTab.Columns.Add("ProcessPoints")
            dtTab.Columns.Add("ProcessDesc")
            dtTab.Columns.Add("Status")
            sSql = "Select CAP_ID,CAP_CODE,CAP_PROCESSNAME,CAP_POINTS,CAP_SECTIONID ,CAP_SUBSECTIONID,CAP_Desc,CAP_DELFLG From CRPA_PROCESS"

            If iPId > 0 Then
                sSql = sSql & " where CAP_ID =" & iPId & ""
            End If
            If iStatus = 0 Then
                sSql = sSql & " and CAP_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " and CAP_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " and CAP_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CAP_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("Pid") = dt.Rows(i)("CAP_ID")
                dRow("ProcessID") = dt.Rows(i)("CAP_ID")
                dRow("ProcessCode") = dt.Rows(i)("CAP_CODE")
                dRow("ProcessName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_PROCESSNAME"))
                dRow("sid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_SECTIONID"))
                dRow("ssid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_SUBSECTIONID"))
                dRow("ProcessPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_POINTS"))
                dRow("ProcessDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_Desc"))
                If dt.Rows(i)("CAP_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("CAP_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CAP_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function loadSubProcessgrid(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal isPId As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("sPid")
            dtTab.Columns.Add("sProcessID")
            dtTab.Columns.Add("sProcessCode")
            dtTab.Columns.Add("sProcessName")
            dtTab.Columns.Add("sid")
            dtTab.Columns.Add("ssid")
            dtTab.Columns.Add("pid")
            dtTab.Columns.Add("sProcessPoints")
            dtTab.Columns.Add("sProcessDesc")
            dtTab.Columns.Add("Status")
            sSql = "Select CASP_ID,CASP_CODE,CASP_SUBPROCESSNAME,CASP_POINTS,CASP_SECTIONID ,CASP_SUBSECTIONID,CASP_PROCESSID,CASP_Desc,CASP_DELFLG From CRPA_subprocess"

            If isPId > 0 Then
                sSql = sSql & " where CASP_ID =" & isPId & ""
            End If
            If iStatus = 0 Then
                sSql = sSql & "and CASP_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & "and CASP_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & "and CASP_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CASP_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SrNo") = i + 1
                dRow("sPid") = dt.Rows(i)("CASP_ID")
                dRow("sProcessID") = dt.Rows(i)("CASP_ID")
                dRow("sProcessCode") = dt.Rows(i)("CASP_CODE")
                dRow("sProcessName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_SUBPROCESSNAME"))
                dRow("sid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_SECTIONID"))
                dRow("ssid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_SUBSECTIONID"))
                dRow("pid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_PROCESSID"))
                dRow("sProcessPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_POINTS"))
                dRow("sProcessDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_Desc"))
                If dt.Rows(i)("CASP_DELFLG") = "A" Then
                    dRow("Status") = "Activated"
                ElseIf dt.Rows(i)("CASP_DELFLG") = "D" Then
                    dRow("Status") = "De-Activated"
                ElseIf dt.Rows(i)("CASP_DELFLG") = "W" Then
                    dRow("Status") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function LoadAllsSections(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal iSect_ID As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("subsid")
            dtTab.Columns.Add("sSectionID")
            dtTab.Columns.Add("sSectionCode")
            dtTab.Columns.Add("sSectionName")
            dtTab.Columns.Add("sid")
            dtTab.Columns.Add("sSectionDesc")
            dtTab.Columns.Add("sSectionPoints")
            dtTab.Columns.Add("sStatus")

            sSql = "Select CASU_ID,CASU_CODE,CASU_SUBSECTIONNAME,CASU_SectionID,CASU_DELFLG,CASU_POINTS,CASU_Desc,CASU_POINTS From CRPA_SubSection where CASU_COMPID =" & iACID & ""

            If iSect_ID > 0 Then
                sSql = sSql & "and CASU_SECTIONID =" & iSect_ID & ""
            End If
            If iStatus = 0 Then
                sSql = sSql & " and CASU_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & "  and CASU_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " and CASU_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CASU_ID"

            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow()
                drow("SrNo") = i + 1
                drow("subsid") = dt.Rows(i)("CASU_ID")
                drow("sSectionID") = dt.Rows(i)("CASU_ID")
                drow("sSectionCode") = dt.Rows(i)("CASU_CODE")
                drow("sSectionName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_SUBSECTIONNAME"))
                drow("sid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_SECTIONID"))
                drow("sSectionDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_DESC"))
                drow("sSectionPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASU_POINTS"))
                If dt.Rows(i)("CASU_DELFLG") = "A" Then
                    drow("sStatus") = "Activated"
                ElseIf dt.Rows(i)("CASU_DELFLG") = "D" Then
                    drow("sStatus") = "De-Activated"
                ElseIf dt.Rows(i)("CASU_DELFLG") = "W" Then
                    drow("sStatus") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function LoadAllProcess(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal sid As Integer, ByVal subSid As Integer, ByVal ipid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("IpID")
            dtTab.Columns.Add("ProcessCode")
            dtTab.Columns.Add("ProcessName")
            dtTab.Columns.Add("sid")
            dtTab.Columns.Add("ssid")
            dtTab.Columns.Add("ProcessDesc")
            dtTab.Columns.Add("ProcessPoints")
            dtTab.Columns.Add("PStatus")

            sSql = "Select CAP_ID,CAP_CODE,CAP_PROCESSNAME,CAP_SECTIONID,CAP_SUBSECTIONID,CAP_POINTS ,CAP_Desc,CAP_DELFLG,CAP_Desc From CRPA_PROCESS where CAP_COMPID =" & iACID & ""
            If subSid > 0 Then
                sSql = sSql & "and CAP_SECTIONID =" & sid & " and CAP_SUBSECTIONID=" & subSid & ""
            End If
            If iStatus = 0 Then
                sSql = sSql & " and CAP_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " and CAP_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " and CAP_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CAP_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow()
                drow("SrNo") = i + 1
                drow("IpID") = dt.Rows(i)("CAP_ID")
                drow("ProcessID") = dt.Rows(i)("CAP_ID")
                drow("ProcessCode") = dt.Rows(i)("CAP_CODE")
                drow("ProcessName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_PROCESSNAME"))
                drow("sid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_SECTIONID"))
                drow("ssid") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_SUBSECTIONID"))
                drow("ProcessDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_Desc"))
                drow("ProcessPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CAP_POINTS"))
                If dt.Rows(i)("CAP_DELFLG") = "A" Then
                    drow("PStatus") = "Activated"
                ElseIf dt.Rows(i)("CAP_DELFLG") = "D" Then
                    drow("PStatus") = "De-Activated"
                ElseIf dt.Rows(i)("CAP_DELFLG") = "W" Then
                    drow("PStatus") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function LoadAllSubProcess(ByVal sAc As String, ByVal iACID As Integer, ByVal iStatus As Integer, ByVal sid As Integer, ByVal subSid As Integer, ByVal pId As Integer, ByVal sPid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim drow As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SProcessID")
            dtTab.Columns.Add("SProcessCode")
            dtTab.Columns.Add("SProcessName")
            dtTab.Columns.Add("SProcessDesc")
            dtTab.Columns.Add("SProcessPoints")
            dtTab.Columns.Add("SPStatus")

            sSql = "Select CASP_ID,CASP_CODE,CASP_SUBPROCESSNAME,CASP_POINTS ,CASP_Desc,CASP_DELFLG,CASP_Desc From CRPA_SUBPROCESS where CASP_COMPID =" & iACID & ""

            If pId > 0 Then
                sSql = sSql & " and CASP_SECTIONID =" & sid & " and CASP_SUBSECTIONID =" & subSid & " and CASP_PROCESSID=" & pId & ""
            End If

            If iStatus = 0 Then
                sSql = sSql & " and CASP_DELFLG ='A'" 'Activated
            ElseIf iStatus = 1 Then
                sSql = sSql & " and CASP_DELFLG='D'" 'De-Activated
            ElseIf iStatus = 2 Then
                sSql = sSql & " and CASP_DELFLG='W'" 'Waiting for approval
            End If
            sSql = sSql & " Order By CASP_ID"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                drow = dtTab.NewRow()
                drow("SrNo") = i + 1
                drow("SProcessID") = dt.Rows(i)("CASP_ID")
                drow("SProcessCode") = dt.Rows(i)("CASP_CODE")
                drow("SProcessName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_SUBPROCESSNAME"))
                drow("SProcessDesc") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_Desc"))
                drow("SProcessPoints") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CASP_POINTS"))
                If dt.Rows(i)("CASP_DELFLG") = "A" Then
                    drow("SPStatus") = "Activated"
                ElseIf dt.Rows(i)("CASP_DELFLG") = "D" Then
                    drow("SPStatus") = "De-Activated"
                ElseIf dt.Rows(i)("CASP_DELFLG") = "W" Then
                    drow("SPStatus") = "Waiting for Approval"
                End If
                dtTab.Rows.Add(drow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Sub ApproveSectionStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal ISectionId As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_section set"
            If sType = "Created" Then
                sSql = sSql & " CAS_DELFLG='A',CAS_STATUS='A',CAS_APPROVEDBY=" & iSessionUsrID & ", CAS_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CAS_DELFLG='D',CAS_STATUS='AD',CAS_DELETEDBy=" & iSessionUsrID & ", CAS_DELETEDON=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CAS_DELFLG='A',CAS_STATUS='AR',CAS_RECALLBY=" & iSessionUsrID & ", CAS_RECALLON=Getdate(),"
            End If
            sSql = sSql & "CAS_IPAddress='" & sIPAddress & "' Where CAS_CompId=" & iACID & " And CAS_ID=" & ISectionId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ApproveSubSectionStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal ISectionId As Integer, ByVal ISubSectionID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_SubSection set"
            If sType = "Created" Then
                sSql = sSql & " CASU_DELFLG='A',CASU_STATUS='A',CASU_APPROVEDBY=" & iSessionUsrID & ", CASU_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CASU_DELFLG='D',CASU_STATUS='AD',CASU_DELETEDby=" & iSessionUsrID & ", CASU_DELETEDON=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CASU_DELFLG='A',CASU_STATUS='AR',CASU_RECALLBY=" & iSessionUsrID & ", CASU_RECALLON=Getdate(),"
            End If
            sSql = sSql & "CASU_IPAddress='" & sIPAddress & "' Where CASU_CompId=" & iACID & " And CASU_ID=" & ISubSectionID & " And CASU_SECTIONID=" & ISectionId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ApproveProcessStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal ISectionId As Integer, ByVal ISubSectionID As Integer, ByVal IProcessId As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_Process set"
            If sType = "Created" Then
                sSql = sSql & " CAP_DELFLG='A',CAP_STATUS='A',CAP_APPROVEDBY=" & iSessionUsrID & ", CAP_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CAP_DELFLG='D',CAP_STATUS='AD',CAP_DELETEDby=" & iSessionUsrID & ", CAP_UPDATEDON=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CAP_DELFLG='A',CAP_STATUS='AR',CAP_RECALLBY=" & iSessionUsrID & ", CAP_RECALLON=Getdate(),"
            End If
            sSql = sSql & "CAP_IPAddress='" & sIPAddress & "' Where CAP_CompId=" & iACID & " And CAP_ID=" & IProcessId & " And CAP_SECTIONID=" & ISectionId & "And CAP_SUBSECTIONID=" & ISubSectionID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub ApproveSubProcessStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iSessionUsrID As Integer, ByVal ISectionId As Integer, ByVal ISubSectionID As Integer, ByVal IProcessId As Integer, ByVal ISubProcessId As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CRPA_SubProcess set"
            If sType = "Created" Then
                sSql = sSql & " CASP_DELFLG='A',CASP_STATUS='A',CASP_APPROVEDBY=" & iSessionUsrID & ", CASP_APPROVEDON=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CASP_DELFLG='D',CASP_STATUS='AD',CASP_DELETEDby=" & iSessionUsrID & ", CASP_UPDATEDON=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CASP_DELFLG='A',CASP_STATUS='AR',CASP_RECALLBY=" & iSessionUsrID & ", CASP_RECALLON=Getdate(),"
            End If
            sSql = sSql & "CASP_IPAddress='" & sIPAddress & "' Where CASP_CompId=" & iACID & " And CASP_ID=" & ISubProcessId & " And CASP_SECTIONID=" & ISectionId & " And CASP_SUBSECTIONID = " & ISubSectionID & " And CASP_PROCESSID =" & IProcessId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Function SaveSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objSection As clsCAIQAuditUniverse) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.iId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objSection.SECTCODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_SECTIONNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objSection.SECTNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.SECTPOINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objSection.SECTDESC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objSection.SECTDELFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.SECTCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objSection.SECTSTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.SECTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objSection.SECTIPADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.SECTCOMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAS_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objSection.SECTYEARID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_Section_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavesSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objsubSection As clsCAIQAuditUniverse) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSectID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objsubSection.sSECTCODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_SUBSECTIONNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objsubSection.sSECTNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.iSecID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_Points", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSECTPOINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objsubSection.sSECTDESC
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objsubSection.sSECTDELFLAG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSECTCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objsubSection.sSECTSTATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@@CASU_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSECTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objsubSection.sSECTIPADDRESS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSECTCOMPID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASU_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsubSection.sSECTYEARID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_SubSection", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveProcessDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objProcess As clsCAIQAuditUniverse) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objProcess.sCAP_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_PROCESSNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objProcess.CAP_PROCESSNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_Points
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_SECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_SubSECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objProcess.sCAP_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objProcess.sCAP_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objProcess.sCAP_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objProcess.iCAP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CAP_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objProcess.iCAP_YEARId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_Process", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveSubProcessSectionDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objsProcess As clsCAIQAuditUniverse) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_ID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CODE", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objsProcess.sCASP_CODE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SUBPROCESSNAME", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objsProcess.sCASP_SUBPNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_POINTS", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_POINTS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_SECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_SUBSECTIONID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_SubSECID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_PROCESSID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_ProID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objsProcess.sCASP_Desc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_DELFLG", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objsProcess.sCASP_DELFLG
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CRBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_CRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objsProcess.sCASP_STATUS
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_UPDATEDBY", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_UPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objsProcess.sCASP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_CompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CASP_YEARId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objsProcess.iCASP_YearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spCRPA_SubProcess", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSectionNameExist(ByVal sAC As String, ByVal sSectionName As String, ByVal isecID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CAS_SECTIONNAME from CRPA_Section where CAS_SECTIONNAME='" & sSectionName & "' And CAS_ID <>" & isecID & " and (CAS_DELFLG='A' or CAS_STATUS = 'W')"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChecksSectionNameExist(ByVal sAC As String, ByVal subSectionName As String, ByVal Isectionid As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "" : sSql = "Select  CASU_SUBSECTIOnNAME from CRPA_SubSection where CASU_SUBSECTIONNAME='" & subSectionName & "'"
            sSql = sSql & " and (CASU_DELFLG ='A' or CASU_DELFLG ='W') and CASU_SECTIONID=" & Isectionid & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function checkProcessNameExist(ByVal sAC As String, ByVal ProcessName As String, ByVal IsectionId As Integer, ByVal IsubsectionId As Integer) As Boolean
        Dim sSql As String
        Try

            sSql = "" : sSql = "Select  CAP_PROCESSNAME from CRPA_Process where CAP_PROCESSNAME='" & ProcessName & "'"
            sSql = sSql & "And (CAP_DELFLG ='A' or CAP_DELFLG ='W') and CAP_SECTIONID=" & IsectionId & " and CAP_SUBSECTIONID=" & IsubsectionId & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Checkcondtion(ByVal sAC As String, ByVal sSelect As String, ByVal sColumn As String, ByVal sTable As String,
                                         ByVal sNewname As String, ByVal iPkId As Integer) As Boolean    'Vijeth
        Dim sSql As String
        Dim sExistname As String
        Try
            sSql = "" : sSql = "SELECT  UPPER(" & sSelect & ") from " & sTable & " where " & sColumn & "='" & iPkId & "'"
            sExistname = objDBL.SQLExecuteScalar(sAC, sSql)
            If sExistname = (sNewname.ToUpper()) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ChecksProcessNameExist(ByVal sAC As String, ByVal sSprocessName As String, ByVal IsectionId As Integer, ByVal IsubsectionId As Integer, ByVal Iprocessid As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "" : sSql = "Select  CASP_SUBPROCESSNAME from CRPA_SUBPROCESS where CASP_SUBPROCESSNAME='" & sSprocessName & "' And CASP_ID <>" & Iprocessid & ""
            sSql = sSql & " and (CASP_DELFLG ='A' or CASP_STATUS ='W') and CASP_SECTIONID=" & IsectionId & " and CASP_SUBSECTIONID=" & IsubsectionId & " and CASP_PROCESSID=" & Iprocessid & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkSectionCodeNameExist(ByVal sAC As String, ByVal iCompid As Integer, ByVal sSectionCOde As String, ByVal isecID As Integer, ByVal iYearID As Integer) As Boolean
        Dim sSql As String
        Try

            sSql = "Select CAS_ID from CRPA_SECTION where CAS_CODE='" & sSectionCOde & "' and CAS_CompId=" & iCompid & ""
            If isecID > 0 Then
                sSql = sSql & "and CAS_ID <> " & isecID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkProcessCodeNameExist(ByVal sAC As String, ByVal icompid As Integer, ByVal ProcessCode As String, ByVal IsectionId As Integer, ByVal IsubSectionId As Integer, ByVal IProcessId As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "" : sSql = "Select  CAP_CODE from CRPA_Process where CAP_CODE='" & ProcessCode & "' And CAP_ID <>" & IProcessId & " "
            sSql = sSql & " and (CAP_DELFLG ='A' or CAP_DELFLG ='W') and CAP_SECTIONID=" & IsectionId & " and CAP_SUBSECTIONID=" & IsubSectionId & ""

            If iSecID > 0 Then
                sSql = sSql & "and CAP_ID <> " & IProcessId & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function checksProcessCodeNameExist(ByVal sAC As String, ByVal Icompid As Integer, ByVal spCODE As String, ByVal IspID As Integer, ByVal iYearID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CASP_ID from CRPA_subProcess where CASP_CODE='" & spCODE & "' And CASP_compid =" & Icompid & ""
            If IspID > 0 Then
                sSql = sSql & " And CASP_ID <>" & IspID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try

    End Function


    Public Function checksSectionCodeNameExist(ByVal sAC As String, ByVal iCompid As Integer, ByVal sSectionCOde As String, ByVal isectionID As Integer, ByVal IsubsectionId As Integer) As Boolean
        Dim sSql As String
        Try

            sSql = "" : sSql = "Select  CASU_CODE from CRPA_SubSection where CASU_CODE='" & sSectionCOde & "' And CASU_ID <>" & IsubsectionId & " "
            sSql = sSql & " and (CASU_DELFLG ='A' or CASU_DELFLG ='W') and CASU_SECTIONID=" & isectionID & ""

            If iSecID > 0 Then
                sSql = sSql & "and CASU_ID <> " & iSecID & " "
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    '-------------Excel Upload---------------------
    Public Function CheckSection(ByVal sAC As String, ByVal iACID As Integer, ByVal sSectionName As String) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            sSql = "Select CAS_ID from CRPA_Section where Upper(CAS_SECTIONNAME)=Upper('" & sSectionName & "') and (CAS_DELFLG='A' or CAS_STATUS = 'W')"
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubSection(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubSectionName As String, ByVal iSecID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iSecID = 0 Then
                sSql = "Select CASU_ID from CRPA_SubSection where Upper(CASU_SUBSECTIONNAME)=Upper('" & sSubSectionName & "') And  (CASU_DELFLG ='A' or CASU_DELFLG ='W') AND CASU_CompId=" & iACID & ""
            Else
                sSql = "Select CASU_ID from CRPA_SubSection where Upper(CASU_SUBSECTIONNAME)=Upper('" & sSubSectionName & "') And CASU_SECTIONID=" & iSecID & " And  (CASU_DELFLG ='A' or CASU_DELFLG ='W')  AND CASU_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcessName As String, ByVal iSecID As Integer, ByVal iSSecID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iSSecID = 0 Then
                sSql = "Select CAP_ID from CRPA_Process where Upper(CAP_PROCESSNAME)=Upper('" & sProcessName & "') And (CAP_DELFLG ='A' or CAP_DELFLG ='W') and  CAP_CompId=" & iACID & ""
            Else
                sSql = "Select CAP_ID from CRPA_Process where Upper(CAP_PROCESSNAME)=Upper('" & sProcessName & "')  And CAP_SECTIONID=" & iSecID & " And CAP_SubSECTIONID=" & iSSecID & " And (CAP_DELFLG ='A' or CAP_DELFLG ='W') and  CAP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcess(ByVal sAC As String, ByVal iACID As Integer, ByVal sProcessName As String, ByVal iSecID As Integer, ByVal iSSecID As Integer, ByVal iProID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iProID = 0 Then
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_SUBPROCESSNAME)=Upper('" & sProcessName & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_CompId=" & iACID & ""
            Else
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_SUBPROCESSNAME)=Upper('" & sProcessName & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_SECTIONID=" & iSecID & " and CASP_SUBSECTIONID=" & iSSecID & " AND CASP_PROCESSID=" & iProID & " AND CASP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcessCode(ByVal sAC As String, ByVal iACID As Integer, ByVal sSubProCode As String, ByVal iSecID As Integer, ByVal iSSecID As Integer, ByVal iProID As Integer) As Integer
        Dim sSql As String = "" : Dim ChkRec As Integer = 0
        Try
            If iProID = 0 Then
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_CODE)=Upper('" & sSubProCode & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')  And CASP_CompId=" & iACID & ""
            Else
                sSql = "Select CASP_ID from CRPA_SubProcess where Upper(CASP_CODE)=Upper('" & sSubProCode & "') and (CASP_DELFLG ='A' or CASP_STATUS ='W')"
                sSql = sSql & " And CASP_SECTIONID=" & iSecID & " and CASP_SUBSECTIONID=" & iSSecID & " AND CASP_PROCESSID=" & iProID & " AND CASP_CompId=" & iACID & ""
            End If
            ChkRec = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetName(ByVal sAC As String, ByVal iACID As Integer, ByVal iSecID As Integer, ByVal iSSecID As Integer, ByVal iProID As Integer) As DataTable
        Dim sSql As String = "" : Dim dtname As DataTable
        Try
            If iSecID <> 0 And iSSecID = 0 And iProID = 0 Then
                sSql = "select distinct(b.CAS_SECTIONNAME) as  SECTIONNAME from CRPA_subSection"
                sSql = sSql & " left join CRPA_Section b on b.cas_id = CASU_SECTIONID"
                sSql = sSql & " where CASU_SECTIONID=" & iSecID & " and CASU_DELFLG ='A'  And CASU_CompId=" & iACID & ""
            ElseIf iSSecID <> 0 And iSSecID <> 0 And iProID = 0 Then
                sSql = "select b.CAS_SECTIONNAME as SECTIONNAME,c.CASU_SUBSECTIONNAME as SUBSECTIONNAME from crpa_process"
                sSql = sSql & " left join CRPA_Section b on b.CAS_ID = CAP_SECTIONID"
                sSql = sSql & " left join CRPA_SubSection c on c.CASU_ID = CAP_SUbSECTIONID"
                sSql = sSql & " where CAP_SECTIONID=" & iSecID & "and CAP_SUBSECTIONID=" & iSSecID & " And CAP_DELFLG ='A'  And CAP_CompId=" & iACID & ""
                sSql = sSql & " group by b.CAS_SECTIONNAME,c.CASU_SUBSECTIONNAME"
            ElseIf iProID <> 0 And iSSecID <> 0 And iProID <> 0 Then
                sSql = "select b.CAS_SECTIONNAME as SECTIONNAME,c.CASU_SUBSECTIONNAME as SUBSECTIONNAME,d.CAP_PROCESSNAME as Process from CRPA_SubProcess"
                sSql = sSql & " left join CRPA_Section b on b.CAS_ID = CASP_SECTIONID"
                sSql = sSql & " left join CRPA_SubSection c on c.CASU_ID = CASP_SUbSECTIONID"
                sSql = sSql & " left join crpa_process d on d.CAP_ID = CASP_Processid"
                sSql = sSql & " where CASP_SECTIONID=" & iSecID & " And CASP_SUBSECTIONID=" & iSSecID & "and CASP_Processid= " & iProID & " And CASP_DELFLG ='A'  And CASP_CompId=" & iACID & ""
                sSql = sSql & " group by b.CAS_SECTIONNAME,c.CASU_SUBSECTIONNAME,d.CAP_PROCESSNAME"
            End If
            dtname = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtname
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
