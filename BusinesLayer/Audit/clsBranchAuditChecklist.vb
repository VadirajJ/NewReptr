Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsBranchAuditChecklist
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private iCM_Id As Integer
    Private iCM_CustId As Integer
    Private iCM_FunctionId As Integer
    Private iCM_AreaId As Integer
    Private sCM_RiskCategory As String
    Private fCM_RiskWeight As Double
    Private sCM_CheckPointNo As String
    Private sCM_CheckPoint As String
    Private iCM_MethodologyId As Integer
    Private sCM_Delflag As String
    Private iCM_SampleSize As Integer
    Private sCM_AreaNo As String
    Private iCM_YearId As Integer
    Private sCM_FunType As String
    Private sCM_Status As String
    Private iCM_CrBy As Integer
    Private iCM_UpdatedBy As Integer
    Private sCM_IPAddress As String
    Private iCM_CompID As Integer
    Public Property iCMId() As Integer
        Get
            Return (iCM_Id)
        End Get
        Set(ByVal Value As Integer)
            iCM_Id = Value
        End Set
    End Property
    Public Property iCMCustId() As Integer
        Get
            Return (iCM_CustId)
        End Get
        Set(ByVal Value As Integer)
            iCM_CustId = Value
        End Set
    End Property
    Public Property iCMYearId() As Integer
        Get
            Return (iCM_YearId)
        End Get
        Set(ByVal Value As Integer)
            iCM_YearId = Value
        End Set
    End Property
    Public Property iCMCrBy() As Integer
        Get
            Return (iCM_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iCM_CrBy = Value
        End Set
    End Property
    Public Property iCMFunctionId() As Integer
        Get
            Return (iCM_FunctionId)
        End Get
        Set(ByVal Value As Integer)
            iCM_FunctionId = Value
        End Set
    End Property
    Public Property iCMAreaId() As Integer
        Get
            Return (iCM_AreaId)
        End Get
        Set(ByVal Value As Integer)
            iCM_AreaId = Value
        End Set
    End Property
    Public Property sCMRiskCategory() As String
        Get
            Return (sCM_RiskCategory)
        End Get
        Set(ByVal Value As String)
            sCM_RiskCategory = Value
        End Set
    End Property
    Public Property fCMRiskWeight() As Double
        Get
            Return (fCM_RiskWeight)
        End Get
        Set(ByVal Value As Double)
            fCM_RiskWeight = Value
        End Set
    End Property
    Public Property sCMCheckPointNo() As String
        Get
            Return (sCM_CheckPointNo)
        End Get
        Set(ByVal Value As String)
            sCM_CheckPointNo = Value
        End Set
    End Property
    Public Property sCMCheckPoint() As String
        Get
            Return (sCM_CheckPoint)
        End Get
        Set(ByVal Value As String)
            sCM_CheckPoint = Value
        End Set
    End Property
    Public Property iCMMethodologyId() As Integer
        Get
            Return (iCM_MethodologyId)
        End Get
        Set(ByVal Value As Integer)
            iCM_MethodologyId = Value
        End Set
    End Property
    Public Property sCMDelflag() As String
        Get
            Return (sCM_Delflag)
        End Get
        Set(ByVal Value As String)
            sCM_Delflag = Value
        End Set
    End Property
    Public Property iCMSampleSize() As Integer
        Get
            Return (iCM_SampleSize)
        End Get
        Set(ByVal Value As Integer)
            iCM_SampleSize = Value
        End Set
    End Property
    Public Property sCMAreaNo() As String
        Get
            Return (sCM_AreaNo)
        End Get
        Set(ByVal Value As String)
            sCM_AreaNo = Value
        End Set
    End Property
    Public Property sCMFunType() As String
        Get
            Return (sCM_FunType)
        End Get
        Set(ByVal Value As String)
            sCM_FunType = Value
        End Set
    End Property
    Public Property iCMUpdatedBy() As Integer
        Get
            Return (iCM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iCM_UpdatedBy = Value
        End Set
    End Property
    Public Property iCMCompID() As Integer
        Get
            Return (iCM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iCM_CompID = Value
        End Set
    End Property
    Public Property sCMStatus() As String
        Get
            Return (sCM_Status)
        End Get
        Set(ByVal Value As String)
            sCM_Status = Value
        End Set
    End Property
    Public Property sCMIPAddress() As String
        Get
            Return (sCM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sCM_IPAddress = Value
        End Set
    End Property
    'Load Function
    Public Function LoadAllFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal sSearch As String, sCheckStatus As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " "
            If sCheckStatus = "YES" Then
                sSql = sSql & " And ENT_DELFLG='A' "
            End If
            If sSearch <> "" Then
                sSql = sSql & " And (ENT_ENTITYName Like '" & sSearch & "%')"
            End If
            sSql = sSql & " Order by Ent_Entityname"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load Area,Methodology,SampleSize Master
    Public Function LoadMasterChkLst(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String, ByVal sFileName As String, ByVal iUpdateOrSave As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select cmm_ID,SubString(cmm_Desc,0,200)cmm_Desc from Content_Management_Master Where cmm_Category='" & sCategory & "' and cmm_delflag='A' and CMM_CompID=" & iACID & " Order by cmm_Desc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load BCMCheckListMasters To Grid
    Public Function LoadGridBCMCheckListMaster(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAreaID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("statusID")
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("CheckPointNo")
            dt.Columns.Add("CheckPoint")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("RiskRatingColor")
            dt.Columns.Add("Methodology")
            dt.Columns.Add("SampleSize")
            dt.Columns.Add("Status")
            dt.Columns.Add("FunctionType")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("MethodologyID")
            dt.Columns.Add("SampleSizeID")

            sSql = "Select * from CMACheckMaster Where CM_CompID=" & iACID & " and CM_Yearid=" & iYearID & "  "
            If iFunctionID > 0 Then
                sSql = sSql & " and cm_functionID=" & iFunctionID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and CM_AreaId=" & iAreaID & ""
            End If
            sSql = sSql & " order by CM_CheckPointNo "
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1

                    If IsDBNull(dtDetails.Rows(i)("CM_FunctionId")) = False Then
                        dRow("Function") = objDBL.SQLGetDescription(sAC, "Select  ENT_ENTITYName from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("CM_FunctionId") & "")
                        dRow("FunctionID") = objDBL.SQLGetDescription(sAC, "Select  ENT_id from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("CM_FunctionId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_AreaId")) = False Then
                        dRow("Area") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("CM_AreaId") & "")
                        dRow("AreaID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where   Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("CM_AreaId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_MethodologyId")) = False Then
                        dRow("Methodology") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("CM_MethodologyId") & "")
                        dRow("MethodologyID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where   Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("CM_MethodologyId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_SampleSize")) = False Then
                        dRow("SampleSize") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("CM_SampleSize") & "")
                        dRow("SampleSizeID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where   Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("CM_SampleSize") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_Status")) = False Then
                        dRow("statusID") = dtDetails.Rows(i)("CM_Status")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_Id")) = False Then
                        dRow("ID") = dtDetails.Rows(i)("CM_Id")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_CheckPointNo")) = False Then
                        dRow("CheckPointNo") = dtDetails.Rows(i)("CM_CheckPointNo")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("CM_CheckPoint")) = False Then
                        dRow("CheckPoint") = dtDetails.Rows(i)("CM_CheckPoint")
                    End If

                    dRow("Status") = ""
                    If IsDBNull(dtDetails.Rows(i)("CM_Delflag")) = False Then
                        If dtDetails.Rows(i)("CM_Delflag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        ElseIf dtDetails.Rows(i)("CM_Delflag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dtDetails.Rows(i)("CM_Delflag") = "D" Then
                            dRow("Status") = "De-Activated"
                        End If
                    End If

                    dRow("RiskCategory") = ""
                    If IsDBNull(dtDetails.Rows(i)("CM_RiskCategory")) = False Then
                        If dtDetails.Rows(i)("CM_RiskCategory") = "HIGH" Then
                            dRow("RiskCategory") = "HIGH"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "HIGH")
                        ElseIf dtDetails.Rows(i)("CM_RiskCategory") = "MEDIUM" Then
                            dRow("RiskCategory") = "MEDIUM"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "MEDIUM")
                        ElseIf dtDetails.Rows(i)("CM_RiskCategory") = "LOW" Then
                            dRow("RiskCategory") = "LOW"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "LOW")
                        End If
                    End If

                    dRow("FunctionType") = ""
                    If IsDBNull(dtDetails.Rows(i)("CM_FunType")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("CM_FunType")) = False Then
                            If dtDetails.Rows(i)("CM_FunType") = "C" Then
                                dRow("FunctionType") = "Branch Core Process"
                            ElseIf dtDetails.Rows(i)("CM_FunType") = "S" Then
                                dRow("FunctionType") = "Branch Sales Support Process"
                            End If
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load BIACheckListMasters To Grid
    Public Function LoadGridBIACheckListMaster(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAreaID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("statusID")
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("CheckPointNo")
            dt.Columns.Add("CheckPoint")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("RiskRatingColor")
            dt.Columns.Add("Methodology")
            dt.Columns.Add("SampleSize")
            dt.Columns.Add("Status")
            dt.Columns.Add("FunctionType")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("MethodologyID")
            dt.Columns.Add("SampleSizeID")

            sSql = "Select * from Audit_CheckList_Master Where ACM_CompID=" & iACID & " and ACM_Yearid=" & iYearID & "  "
            If iFunctionID > 0 Then
                sSql = sSql & " and Acm_functionID=" & iFunctionID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and ACM_AreaId=" & iAreaID & ""
            End If
            sSql = sSql & " order by ACM_CheckPointNo "
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1

                    If IsDBNull(dtDetails.Rows(i)("ACM_FunctionId")) = False Then
                        dRow("Function") = objDBL.SQLGetDescription(sAC, "Select  ENT_ENTITYName from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("ACM_FunctionId") & "")
                        dRow("FunctionID") = objDBL.SQLGetDescription(sAC, "Select  ENT_id from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("ACM_FunctionId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_AreaId")) = False Then
                        dRow("Area") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("ACM_AreaId") & "")
                        dRow("AreaID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where   Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("ACM_AreaId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_MethodologyId")) = False Then
                        dRow("Methodology") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("ACM_MethodologyId") & "")
                        dRow("MethodologyID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where   Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("ACM_MethodologyId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_SampleSize")) = False Then
                        dRow("SampleSize") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("ACM_SampleSize") & "")
                        dRow("SampleSizeID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("ACM_SampleSize") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_Status")) = False Then
                        dRow("statusID") = dtDetails.Rows(i)("ACM_Status")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_Id")) = False Then
                        dRow("ID") = dtDetails.Rows(i)("ACM_Id")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_CheckPointNo")) = False Then
                        dRow("CheckPointNo") = dtDetails.Rows(i)("ACM_CheckPointNo")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("ACM_CheckPoint")) = False Then
                        dRow("CheckPoint") = dtDetails.Rows(i)("ACM_CheckPoint")
                    End If

                    dRow("Status") = ""
                    If IsDBNull(dtDetails.Rows(i)("ACM_Delflag")) = False Then
                        If dtDetails.Rows(i)("ACM_Delflag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        ElseIf dtDetails.Rows(i)("ACM_Delflag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dtDetails.Rows(i)("ACM_Delflag") = "D" Then
                            dRow("Status") = "De-Activated"
                        End If
                    End If

                    dRow("RiskCategory") = ""
                    If IsDBNull(dtDetails.Rows(i)("ACM_RiskCategory")) = False Then
                        If dtDetails.Rows(i)("ACM_RiskCategory") = "HIGH" Then
                            dRow("RiskCategory") = "HIGH"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "HIGH")
                        ElseIf dtDetails.Rows(i)("ACM_RiskCategory") = "MEDIUM" Then
                            dRow("RiskCategory") = "MEDIUM"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "MEDIUM")
                        ElseIf dtDetails.Rows(i)("ACM_RiskCategory") = "LOW" Then
                            dRow("RiskCategory") = "LOW"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "LOW")
                        End If
                    End If

                    dRow("FunctionType") = ""
                    If IsDBNull(dtDetails.Rows(i)("ACM_FunType")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("ACM_FunType")) = False Then
                            If dtDetails.Rows(i)("ACM_FunType") = "C" Then
                                dRow("FunctionType") = "Branch Core Process"
                            ElseIf dtDetails.Rows(i)("ACM_FunType") = "S" Then
                                dRow("FunctionType") = "Branch Sales Support Process"
                            End If
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Load BRRCheckListMasters To Grid
    Public Function LoadGridBRRCheckListMaster(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal iAreaID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("StatusID")
            dt.Columns.Add("Function")
            dt.Columns.Add("Area")
            dt.Columns.Add("CheckPointNo")
            dt.Columns.Add("CheckPoint")
            dt.Columns.Add("RiskCategory")
            dt.Columns.Add("RiskRatingColor")
            dt.Columns.Add("Methodology")
            dt.Columns.Add("SampleSize")
            dt.Columns.Add("Status")
            dt.Columns.Add("FunctionType")
            dt.Columns.Add("FunctionID")
            dt.Columns.Add("AreaID")
            dt.Columns.Add("MethodologyID")
            dt.Columns.Add("SampleSizeID")

            sSql = "Select * from Risk_CheckList_Master Where RCM_CompID=" & iACID & " and RCM_Yearid=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " and RCM_CustID=" & iCustID & ""
            End If
            If iFunctionID > 0 Then
                sSql = sSql & " and Rcm_functionID=" & iFunctionID & ""
            End If
            If iAreaID > 0 Then
                sSql = sSql & " and RCM_AreaId=" & iAreaID & ""
            End If
            sSql = sSql & " order by RCM_CheckPointNo "
            dtDetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    If IsDBNull(dtDetails.Rows(i)("RCM_FunctionId")) = False Then
                        dRow("Function") = objDBL.SQLGetDescription(sAC, "Select  ENT_ENTITYName from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("RCM_FunctionId") & "")
                        dRow("FunctionID") = objDBL.SQLGetDescription(sAC, "Select  ENT_id from mst_entity_master where ent_compid=" & iACID & " and ENT_id=" & dtDetails.Rows(i)("RCM_FunctionId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_AreaId")) = False Then
                        dRow("Area") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("RCM_AreaId") & "")
                        dRow("AreaID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where Cmm_Category='AR' And Cmm_ID=" & dtDetails.Rows(i)("RCM_AreaId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_MethodologyId")) = False Then
                        dRow("Methodology") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("RCM_MethodologyId") & "")
                        dRow("MethodologyID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where Cmm_Category='M' And Cmm_ID=" & dtDetails.Rows(i)("RCM_MethodologyId") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_SampleSize")) = False Then
                        dRow("SampleSize") = objDBL.SQLGetDescription(sAC, "Select cmm_Desc from Content_Management_master Where Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("RCM_SampleSize") & "")
                        dRow("SampleSizeID") = objDBL.SQLGetDescription(sAC, "Select Cmm_ID from Content_Management_master Where Cmm_Category='SS' And Cmm_ID=" & dtDetails.Rows(i)("RCM_SampleSize") & "")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_Status")) = False Then
                        dRow("StatusID") = dtDetails.Rows(i)("RCM_Status")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_Id")) = False Then
                        dRow("ID") = dtDetails.Rows(i)("RCM_Id")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_CheckPointNo")) = False Then
                        dRow("CheckPointNo") = dtDetails.Rows(i)("RCM_CheckPointNo")
                    End If

                    If IsDBNull(dtDetails.Rows(i)("RCM_CheckPoint")) = False Then
                        dRow("CheckPoint") = dtDetails.Rows(i)("RCM_CheckPoint")
                    End If

                    dRow("Status") = ""
                    If IsDBNull(dtDetails.Rows(i)("RCM_Delflag")) = False Then
                        If dtDetails.Rows(i)("RCM_Delflag") = "W" Then
                            dRow("Status") = "Waiting for Approval"
                        ElseIf dtDetails.Rows(i)("RCM_Delflag") = "A" Then
                            dRow("Status") = "Activated"
                        ElseIf dtDetails.Rows(i)("RCM_Delflag") = "D" Then
                            dRow("Status") = "De-Activated"
                        End If
                    End If

                    dRow("RiskCategory") = ""
                    If IsDBNull(dtDetails.Rows(i)("RCM_RiskCategory")) = False Then
                        If dtDetails.Rows(i)("RCM_RiskCategory") = "HIGH" Then
                            dRow("RiskCategory") = "HIGH"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "HIGH")
                        ElseIf dtDetails.Rows(i)("RCM_RiskCategory") = "MEDIUM" Then
                            dRow("RiskCategory") = "MEDIUM"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "MEDIUM")
                        ElseIf dtDetails.Rows(i)("RCM_RiskCategory") = "LOW" Then
                            dRow("RiskCategory") = "LOW"
                            dRow("RiskRatingColor") = objclsAllActiveMaster.LoadInherentColor(sAC, iACID, "LOW")
                        End If
                    End If

                    dRow("FunctionType") = ""
                    If IsDBNull(dtDetails.Rows(i)("RCM_FunType")) = False Then
                        If IsDBNull(dtDetails.Rows(i)("RCM_FunType")) = False Then
                            If dtDetails.Rows(i)("RCM_FunType") = "C" Then
                                dRow("FunctionType") = "Branch Core Process"
                            ElseIf dtDetails.Rows(i)("RCM_FunType") = "S" Then
                                dRow("FunctionType") = "Branch Sales Support Process"
                            End If
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'GetFunctionID
    Public Function GetFunctionID(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ENT_ID from mst_entity_master where ent_compid=" & iACID & " And ENT_ID=" & iID & "  order by ENT_ID"
            GetFunctionID = objDBL.SQLExecuteScalar(sAC, sSql)
            Return GetFunctionID
        Catch ex As Exception
            Throw
        End Try
    End Function
    'GetAreaID
    Public Function GetAreaID(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer, ByVal sCategory As String)
        Dim sSql As String
        Try
            sSql = "Select cmm_id from content_management_master where cmm_compid=" & iACID & " And cmm_id=" & iID & " And Cmm_Category='" & sCategory & "' order by cmm_id"
            GetAreaID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return GetAreaID
        Catch ex As Exception
            Throw
        End Try
    End Function
    'GetMethodologyID
    Public Function GetMethodologyID(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer, ByVal sCategory As String)
        Dim sSql As String
        Try
            sSql = "Select cmm_id from content_management_master where cmm_compid=" & iACID & " and cmm_id=" & iID & " and Cmm_Category='" & sCategory & "' order by cmm_id"
            GetMethodologyID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return GetMethodologyID
        Catch ex As Exception
            Throw
        End Try
    End Function
    'GetSSID
    Public Function GetSSID(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer, ByVal sCategory As String)
        Dim sSql As String
        Try
            sSql = "Select cmm_id from content_management_master where cmm_compid=" & iACID & " and cmm_id=" & iID & " and Cmm_Category='" & sCategory & "' order by cmm_id"
            GetSSID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return GetSSID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub BranchChecklistApproveBCMStatus(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iCMID As Integer, ByVal iSessionUsrID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update CMACheckMaster set "
            If sType = "Created" Then
                sSql = sSql & " CM_Delflag='A',CM_Status='A',CM_ApprovedBy=" & iSessionUsrID & ", CM_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " CM_Delflag='D',CM_Status='AD',CM_DeletedBy=" & iSessionUsrID & ", CM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " CM_Delflag='A',CM_Status='AR',CM_RecallBy=" & iSessionUsrID & ", CM_RecallOn=Getdate(),"
            End If
            sSql = sSql & "CM_IPAddress='" & sIPAddress & "' Where CM_CompID=" & iACID & " And CM_Id=" & iCMID & " And CM_YearId=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BranchChecklistApproveBIAStatus(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iCMID As Integer, ByVal iSessionUsrID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update Audit_CheckList_Master set "
            If sType = "Created" Then
                sSql = sSql & " ACM_Delflag='A',ACM_Status='A',ACM_ApprovedBy=" & iSessionUsrID & ", ACM_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " ACM_Delflag='D',ACM_Status='AD',ACM_DeletedBy=" & iSessionUsrID & ", ACM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " ACM_Delflag='A',ACM_Status='AR',ACM_RecallBy=" & iSessionUsrID & ", ACM_RecallOn=Getdate(),"
            End If
            sSql = sSql & "ACM_IPAddress='" & sIPAddress & "' Where ACM_CompID=" & iACID & " And ACM_Id=" & iCMID & " And ACM_YearId=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BranchChecklistApproveBRRStatus(ByVal sAC As String, ByVal iACID As Integer, iYearID As Integer, ByVal iCustID As Integer, ByVal iCMID As Integer, ByVal iSessionUsrID As Integer, ByVal sIPAddress As String, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update Risk_CheckList_Master set "
            If sType = "Created" Then
                sSql = sSql & " RCM_Delflag='A',RCM_Status='A',RCM_ApprovedBy=" & iSessionUsrID & ", RCM_ApprovedOn=Getdate(),"
            ElseIf sType = "DeActivated" Then
                sSql = sSql & " RCM_Delflag='D',RCM_Status='AD',RCM_DeletedBy=" & iSessionUsrID & ", RCM_DeletedOn=Getdate(),"
            ElseIf sType = "Activated" Then
                sSql = sSql & " RCM_Delflag='A',RCM_Status='AR',RCM_RecallBy=" & iSessionUsrID & ", RCM_RecallOn=Getdate(),"
            End If
            sSql = sSql & "RCM_IPAddress='" & sIPAddress & "' Where RCM_CustID=" & iCustID & " And RCM_CompID=" & iACID & " And RCM_Id=" & iCMID & " And RCM_YearId=" & iYearID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveBCMCheckListMasterDetails(ByVal sAC As String, ByVal objclsBranchAuditChecklist As clsBranchAuditChecklist) As Object
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCM_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMFunctionId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_AreaId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMAreaId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_RiskCategory", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMRiskCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_RiskWeight", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.fCMRiskWeight
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CheckPointNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPointNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CheckPoint", OleDb.OleDbType.VarChar, 600)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_MethodologyId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMMethodologyId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_SampleSize", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMSampleSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_AreaNo", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMAreaNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_FunType", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMFunType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_Status", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CrBy", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCMACheckMaster", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBACheckListMasterDetails(ByVal sAC As String, ByVal objclsBranchAuditChecklist As clsBranchAuditChecklist) As Object
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(19) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCM_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMFunctionId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AreaId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMAreaId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_RiskCategory", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMRiskCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_RiskWeight", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.fCMRiskWeight
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CheckPointNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPointNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CheckPoint", OleDb.OleDbType.VarChar, 600)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_MethodologyId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMMethodologyId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_SampleSize", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMSampleSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_AreaNo", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMAreaNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_FunType", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMFunType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_Status", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CrBy", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ACM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_CheckList_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveBRRCheckListMasterDetails(ByVal sAC As String, ByVal objclsBranchAuditChecklist As clsBranchAuditChecklist) As Object
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Id", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCM_Id
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_FunctionId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMFunctionId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_AreaId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMAreaId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_RiskCategory", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMRiskCategory
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_RiskWeight", OleDb.OleDbType.Double, 8)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.fCMRiskWeight
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CheckPointNo", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPointNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CheckPoint", OleDb.OleDbType.VarChar, 600)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMCheckPoint
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_MethodologyId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMMethodologyId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Delflag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMDelflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_SampleSize", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMSampleSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_AreaNo", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMAreaNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_YearId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMYearId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_FunType", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMFunType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_Status", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CrBy", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.sCMIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RCM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsBranchAuditChecklist.iCMCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_CheckList_Master", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
