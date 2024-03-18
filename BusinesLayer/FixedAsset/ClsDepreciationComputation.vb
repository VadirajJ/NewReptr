Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data

Public Class ClsDepreciationComputation
    Dim objGen As New clsGRACeGeneral
    Private objDBL As New DatabaseLayer.DBHelper
    Public objFAS As New clsGRACeGeneral

    Dim ADep_ID As Integer
    Dim ADep_AssetID As Integer
    Dim ADep_Item As String
    Dim ADep_RateofDep As Double
    Dim ADep_OPBForYR As Double
    Dim ADep_DepreciationforFY As Double
    Dim ADep_WrittenDownValue As Double
    Dim ADep_ClosingDate As DateTime
    Dim ADep_CreatedBy As Integer
    Dim ADep_CreatedOn As DateTime
    Dim ADep_UpdatedBy As Integer
    Dim ADep_UpdatedOn As DateTime
    Dim ADep_ApprovedBy As Integer
    Dim ADep_ApprovedOn As DateTime
    Dim ADep_DelFlag As String
    Dim ADep_Status As String
    Dim ADep_YearID As Integer
    Dim ADep_CompID As Integer
    Dim ADep_Opeartion As String
    Dim ADep_IPAddress As String
    Dim ADep_CustId As Integer

    Dim ADep_Location As Integer
    Dim ADep_Division As Integer
    Dim ADep_Department As Integer
    Dim ADep_Bay As Integer
    Dim ADep_TransType As Integer
    Dim ADep_Method As Integer
    Public Property iADep_TransType() As Integer
        Get
            Return (ADep_TransType)
        End Get
        Set(ByVal Value As Integer)
            ADep_TransType = Value
        End Set
    End Property
    Public Property iADep_Method() As Integer
        Get
            Return (ADep_Method)
        End Get
        Set(ByVal Value As Integer)
            ADep_Method = Value
        End Set
    End Property
    Public Property iADep_CustId() As Integer
        Get
            Return (ADep_CustId)
        End Get
        Set(ByVal Value As Integer)
            ADep_CustId = Value
        End Set
    End Property
    Public Property iADep_Location() As Integer
        Get
            Return (ADep_Location)
        End Get
        Set(ByVal Value As Integer)
            ADep_Location = Value
        End Set
    End Property
    Public Property iADep_Division() As Integer
        Get
            Return (ADep_Division)
        End Get
        Set(ByVal Value As Integer)
            ADep_Division = Value
        End Set
    End Property
    Public Property iADep_Department() As Integer
        Get
            Return (ADep_Department)
        End Get
        Set(ByVal Value As Integer)
            ADep_Department = Value
        End Set
    End Property
    Public Property iADep_Bay() As Integer
        Get
            Return (ADep_Bay)
        End Get
        Set(ByVal Value As Integer)
            ADep_Bay = Value
        End Set
    End Property
    Public Property iADep_ID() As Integer
        Get
            Return (ADep_ID)
        End Get
        Set(ByVal Value As Integer)
            ADep_ID = Value
        End Set
    End Property
    Public Property iADep_AssetID() As Integer
        Get
            Return (ADep_AssetID)
        End Get
        Set(ByVal Value As Integer)
            ADep_AssetID = Value
        End Set
    End Property
    Public Property sADep_Item() As String
        Get
            Return (ADep_Item)
        End Get
        Set(ByVal Value As String)
            ADep_Item = Value
        End Set
    End Property
    Public Property dADep_RateofDep() As Double
        Get
            Return (ADep_RateofDep)
        End Get
        Set(ByVal Value As Double)
            ADep_RateofDep = Value
        End Set
    End Property
    Public Property dADep_OPBForYR() As Double
        Get
            Return (ADep_OPBForYR)
        End Get
        Set(ByVal Value As Double)
            ADep_OPBForYR = Value
        End Set
    End Property
    Public Property dADep_DepreciationforFY() As Double
        Get
            Return (ADep_DepreciationforFY)
        End Get
        Set(ByVal Value As Double)
            ADep_DepreciationforFY = Value
        End Set
    End Property
    Public Property dADep_WrittenDownValue() As Double
        Get
            Return (ADep_WrittenDownValue)
        End Get
        Set(ByVal Value As Double)
            ADep_WrittenDownValue = Value
        End Set
    End Property
    Public Property dADep_ClosingDate() As Date
        Get
            Return (ADep_ClosingDate)
        End Get
        Set(ByVal Value As Date)
            ADep_ClosingDate = Value
        End Set
    End Property
    Public Property iADep_CreatedBy() As Integer
        Get
            Return (ADep_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ADep_CreatedBy = Value
        End Set
    End Property
    Public Property dADep_CreatedOn() As DateTime
        Get
            Return (ADep_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            ADep_CreatedOn = Value
        End Set
    End Property
    Public Property iADep_UpdatedBy() As Integer
        Get
            Return (ADep_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ADep_UpdatedBy = Value
        End Set
    End Property
    Public Property dADep_UpdatedOn() As DateTime
        Get
            Return (ADep_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            ADep_UpdatedOn = Value
        End Set
    End Property

    Public Property iADep_ApprovedBy() As Integer
        Get
            Return (ADep_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            ADep_ApprovedBy = Value
        End Set
    End Property
    Public Property dADep_ApprovedOn() As DateTime
        Get
            Return (ADep_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            ADep_ApprovedOn = Value
        End Set
    End Property
    Public Property sADep_DelFlag() As String
        Get
            Return (ADep_DelFlag)
        End Get
        Set(ByVal Value As String)
            ADep_DelFlag = Value
        End Set
    End Property
    Public Property sADep_Status() As String
        Get
            Return (ADep_Status)
        End Get
        Set(ByVal Value As String)
            ADep_Status = Value
        End Set
    End Property
    Public Property iADep_YearID() As Integer
        Get
            Return (ADep_YearID)
        End Get
        Set(ByVal Value As Integer)
            ADep_YearID = Value
        End Set
    End Property
    Public Property iADep_CompID() As Integer
        Get
            Return (ADep_CompID)
        End Get
        Set(ByVal Value As Integer)
            ADep_CompID = Value
        End Set
    End Property
    Public Property sADep_Opeartion() As String
        Get
            Return (ADep_Opeartion)
        End Get
        Set(ByVal Value As String)
            ADep_Opeartion = Value
        End Set
    End Property
    Public Property sADep_IPAddress() As String
        Get
            Return (ADep_IPAddress)
        End Get
        Set(ByVal Value As String)
            ADep_IPAddress = Value
        End Set
    End Property
    'IT Act

    Dim ADITAct_ID As Integer
    Dim ADITAct_AssetClassID As Integer
    Dim ADITAct_RateofDep As Double
    Dim ADITAct_OPBForYR As Double
    Dim ADITAct_DepreciationforFY As Double
    Dim ADITAct_WrittenDownValue As Double
    Dim ADITAct_CreatedBy As Integer
    Dim ADITAct_CreatedOn As DateTime
    Dim ADITAct_UpdatedBy As Integer
    Dim ADITAct_UpdatedOn As DateTime
    Dim ADITAct_ApprovedBy As Integer
    Dim ADITAct_ApprovedOn As DateTime
    Dim ADITAct_DelFlag As String
    Dim ADITAct_Status As String
    Dim ADITAct_YearID As Integer
    Dim ADITAct_CompID As Integer
    Dim ADITAct_CustId As Integer
    Dim ADITAct_Opeartion As String
    Dim ADITAct_IPAddress As String

    Dim ADITAct_BfrQtrAmount As Double
    Dim ADITAct_BfrQtrDep  As double
	Dim ADITAct_AftQtrAmount As Double
    Dim ADITAct_AftQtrDep As Double
    Dim ADITAct_DelAmount As Double
    Dim ADITAct_InitAmt As Double
    Public Property dADITAct_InitAmt() As Double
        Get
            Return (ADITAct_InitAmt)
        End Get
        Set(ByVal Value As Double)
            ADITAct_InitAmt = Value
        End Set
    End Property

    Public Property dADITAct_BfrQtrAmount() As Double
        Get
            Return (ADITAct_BfrQtrAmount)
        End Get
        Set(ByVal Value As Double)
            ADITAct_BfrQtrAmount = Value
        End Set
    End Property
    Public Property dADITAct_BfrQtrDep() As Double
        Get
            Return (ADITAct_BfrQtrDep)
        End Get
        Set(ByVal Value As Double)
            ADITAct_BfrQtrDep = Value
        End Set
    End Property
    Public Property dADITAct_AftQtrAmount() As Double
        Get
            Return (ADITAct_AftQtrAmount)
        End Get
        Set(ByVal Value As Double)
            ADITAct_AftQtrAmount = Value
        End Set
    End Property
    Public Property dADITAct_DelAmount() As Double
        Get
            Return (ADITAct_DelAmount)
        End Get
        Set(ByVal Value As Double)
            ADITAct_DelAmount = Value
        End Set
    End Property
    Public Property dADITAct_AftQtrDep() As Double
        Get
            Return (ADITAct_AftQtrDep)
        End Get
        Set(ByVal Value As Double)
            ADITAct_AftQtrDep = Value
        End Set
    End Property
    Public Property iADITAct_ID() As Integer
        Get
            Return (ADITAct_ID)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_ID = Value
        End Set
    End Property
    Public Property iADITAct_AssetClassID() As Integer
        Get
            Return (ADITAct_AssetClassID)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_AssetClassID = Value
        End Set
    End Property

    Public Property dADITAct_RateofDep() As Double
        Get
            Return (ADITAct_RateofDep)
        End Get
        Set(ByVal Value As Double)
            ADITAct_RateofDep = Value
        End Set
    End Property

    Public Property dADITAct_OPBForYR() As Double
        Get
            Return (ADITAct_OPBForYR)
        End Get
        Set(ByVal Value As Double)
            ADITAct_OPBForYR = Value
        End Set
    End Property
    Public Property dADITAct_DepreciationforFY() As Double
        Get
            Return (ADITAct_DepreciationforFY)
        End Get
        Set(ByVal Value As Double)
            ADITAct_DepreciationforFY = Value
        End Set
    End Property
    Public Property dADITAct_WrittenDownValue() As Double
        Get
            Return (ADITAct_WrittenDownValue)
        End Get
        Set(ByVal Value As Double)
            ADITAct_WrittenDownValue = Value
        End Set
    End Property

    Public Property iADITAct_CreatedBy() As Integer
        Get
            Return (ADITAct_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_CreatedBy = Value
        End Set
    End Property
    Public Property dADITAct_CreatedOn() As Date
        Get
            Return (ADITAct_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            ADITAct_CreatedOn = Value
        End Set
    End Property
    Public Property iADITAct_UpdatedBy() As Integer
        Get
            Return (ADITAct_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_UpdatedBy = Value
        End Set
    End Property
    Public Property dADITAct_UpdatedOn() As Date
        Get
            Return (ADITAct_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            ADITAct_UpdatedOn = Value
        End Set
    End Property
    Public Property iADITAct_ApprovedBy() As Integer
        Get
            Return (ADITAct_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_ApprovedBy = Value
        End Set
    End Property
    Public Property dADITAct_ApprovedOn() As DateTime
        Get
            Return (ADITAct_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            ADITAct_ApprovedOn = Value
        End Set
    End Property
    Public Property sADITAct_DelFlag() As String
        Get
            Return (ADITAct_DelFlag)
        End Get
        Set(ByVal Value As String)
            ADITAct_DelFlag = Value
        End Set
    End Property
    Public Property sADITAct_Status() As String
        Get
            Return (ADITAct_Status)
        End Get
        Set(ByVal Value As String)
            ADITAct_Status = Value
        End Set
    End Property
    Public Property iADITAct_YearID() As Integer
        Get
            Return (ADITAct_YearID)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_YearID = Value
        End Set
    End Property
    Public Property iADITAct_CompID() As Integer
        Get
            Return (ADITAct_CompID)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_CompID = Value
        End Set
    End Property
    Public Property iADITAct_CustId() As Integer
        Get
            Return (ADITAct_CustId)
        End Get
        Set(ByVal Value As Integer)
            ADITAct_CustId = Value
        End Set
    End Property
    Public Property iADITAct_Opeartion() As String
        Get
            Return (ADITAct_Opeartion)
        End Get
        Set(ByVal Value As String)
            ADITAct_Opeartion = Value
        End Set
    End Property
    Public Property sADITAct_IPAddress() As String
        Get
            Return (ADITAct_IPAddress)
        End Get
        Set(ByVal Value As String)
            ADITAct_IPAddress = Value
        End Set
    End Property
    Public Function LoadAssetDetails(ByVal sNameSpace As String, ByVal iAsstId As Integer, ByVal iCompId As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dtAsstDetails As New DataTable
        Dim dRow As DataRow
        Dim j = 0
        Try
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("PurchaseDate")
            sSql = "" : sSql = "Select AFAM_AssetCode,AFAM_AssetAge,AFAM_CommissionDate  from Acc_FixedAssetMaster  Where AFAM_ID=" & iAsstId & " and AFAM_CompID=" & iCompId & " and AFAM_CustId=" & iCustID & ""
            dtAsstDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dtAsstDetails.Rows.Count > 0 Then
                For i = 0 To dtAsstDetails.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("AssetCode") = dtAsstDetails.Rows(i)("AFAM_AssetCode").ToString()
                    dRow("AssetAge") = dtAsstDetails.Rows(i)("AFAM_AssetAge").ToString()
                    dRow("PurchaseDate") = dtAsstDetails.Rows(i)("AFAM_CommissionDate")
                    'dRow("PurchaseDate") = objGen.FormatDtForRDBMS(dtAsstDetails.Rows(i)("AFAM_CommissionDate").ToString(), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDepreciationCompWDV(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal NoOfDays As Integer, ByVal TNoOfDays As Integer, ByVal iDuration As Integer,
                                              sStartDt As Date, sEndDate As Date, ByVal iCustID As Integer, ByVal iMethod As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt, dt1, dt2 As New DataTable
        Dim ds1 As New DataSet
        Dim dr As DataRow, dr2 As DataRow
        Dim dDeprec As Double = 0
        Dim iNoOfDays As Integer = 0
        Dim dtAsstDetails As New DataTable
        Dim dOPBAmt As Double = 0.0
        Dim ddelOPBAmt As Double = 0.0
        Dim ddelAddAmt As Double = 0.0
        Dim dOPBPreviousAmt As Double = 0.0
        Dim dtdel As New DataTable

        Dim iFLCount As Integer = 0
        Dim dPreviousOPBAmt As Double = 0.0
        Dim dAddtnAmt As Double = 0.0
        Dim ddate As Date
        Dim assetage As Integer = 0
        Dim IntervalType As String
        Dim AddDate As Date
        Dim sStat As String = ""
        Dim fDate As Integer = 0
        Try
            dt.Columns.Add("AssetClassID")
            dt.Columns.Add("AssetID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            ' dt.Columns.Add("AssetLocationCode")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("LocationID")
            dt.Columns.Add("DivisionID")
            dt.Columns.Add("DepartmentID")
            dt.Columns.Add("BayID")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("TrType")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("Item")
            dt.Columns.Add("OrignalCost")
            dt.Columns.Add("Rsdulvalue")
            dt.Columns.Add("SalvageValue")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("DepreciationRate")
            dt.Columns.Add("AddtnAmt")
            dt.Columns.Add("OPBForYR")
            dt.Columns.Add("DepreciationforFY")
            dt.Columns.Add("wrtnvalue")

            sSql = "Select distinct(AFAA_TrType),AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay,AFAA_Delflag from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustID & " and AFAA_YearID <=" & iYearId & " and AFAA_Delflag = 'A' "
            'If iDuration = 3 Then
            '    sSql = sSql & ""
            'End If
            sSql = sSql & "order by AFAA_ItemType"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt1.Rows.Count > 0 Then
                '  For i = 0 To dt1.Rows.Count - 1

                For i = 0 To dt1.Rows.Count - 1
                    sStat = ""
                    If dt1.Rows(i)("AFAA_ItemType") = 3 Then
                        sStat = ""
                    End If
                    dr = dt.NewRow




                    dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
                    dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & "")

                    dr("LocationID") = dt1.Rows(i)("AFAA_Location")
                    dr("DivisionID") = dt1.Rows(i)("AFAA_Division")
                    dr("DepartmentID") = dt1.Rows(i)("AFAA_Department")
                    dr("BayID") = dt1.Rows(i)("AFAA_Bay")

                    'dr("AssetLocationCode") = dt1.Rows(i)("AFAA_ItemType")
                    dr("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Location") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
                    dr("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Division") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
                    dr("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Department") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
                    dr("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Bay") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")

                    dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustID)
                    If dtAsstDetails.Rows.Count > 0 Then
                        dr("AssetCode") = dtAsstDetails.Rows(0)("AssetCode")
                        dr("AssetAge") = dtAsstDetails.Rows(0)("AssetAge")
                        assetage = dtAsstDetails.Rows(0)("AssetAge")
                        ddate = dtAsstDetails.Rows(0)("PurchaseDate")
                    Else
                        dr("AssetCode") = ""
                        dr("AssetAge") = 0
                        dr("PurchaseDate") = "01-01-1900"
                    End If
                    ' IntervalType = "m"
                    'ADate = DateAdd(IntervalType, assetage, ddate)
                    Dim sAddDate As Date
                    Dim dAddDate As Date
                    'AddDate = ddate.AddYears(assetage)
                    'dAddDate = ddate.AddYears(5)
                    'sAddDate = ddate.AddYears(assetage)

                    'AddDate = Date.ParseExact(ddate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'dAddDate = DateAdd("d", 60, DateValue(AddDate))
                    'sAddDate = Format(dAddDate, "dd/MM/yyyy")
                    dAddDate = ddate

                    sAddDate = dAddDate.AddYears(assetage)
                    'AddDate = dAddDate.AddYears(3)
                    If sAddDate > sStartDt Then
                        If ddate > sStartDt Then
                            If ddate <= sEndDate Then
                                If ddate = sEndDate Then
                                    If iDuration = 3 Then
                                        iNoOfDays = 0
                                    Else
                                        iNoOfDays = 1
                                    End If

                                Else
                                    iNoOfDays = DateDiff(DateInterval.Day, ddate, sEndDate)
                                End If
                                If iNoOfDays = 1 Then
                                    iNoOfDays = iNoOfDays
                                End If
                            ElseIf ddate > sEndDate Then
                                If iDuration = 3 Then
                                    sStat = "A"
                                Else
                                    iNoOfDays = 0
                                End If

                            End If
                        Else
                            If dt1.Rows(i)("AFAA_TrType") = 1 Then
                                iNoOfDays = NoOfDays
                            Else
                                If iDuration = 3 Then ''For monthly calculation purpose, 
                                    iNoOfDays = 0
                                Else
                                    iNoOfDays = NoOfDays
                                End If
                            End If

                        End If
                    ElseIf sAddDate = sStartDt Then
                        iNoOfDays = 365
                    Else
                        iNoOfDays = 0
                    End If
                    dr("NoOfDays") = iNoOfDays
                    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(ddate, "D")
                    If dr("PurchaseDate") = "01/01/1900" Then
                        iNoOfDays = 0
                        dr("NoOfDays") = 0
                        dr("PurchaseDate") = ""
                    End If
                    fDate = 0
                    If dr("PurchaseDate") = objFAS.FormatDtForRDBMS(sEndDate, "D") Then
                        fDate = 1
                    End If
                    dr("Item") = objDBL.SQLExecuteScalar(sNameSpace, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_ID= " & dt1.Rows(i)("AFAA_ItemType") & " and AFAM_CustId=" & iCustID & " and AFAM_Location=" & dt1.Rows(i)("AFAA_Location") & " and AFAM_Division=" & dt1.Rows(i)("AFAA_Division") & " and AFAM_Department=" & dt1.Rows(i)("AFAA_Department") & " and AFAM_Bay=" & dt1.Rows(i)("AFAA_Bay") & "")
                    Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CustId=" & iCustID & "")
                    If sStat = "A" Then
                        GoTo m
                    End If
                    'If iDuration = 3 Then
                    '    If iNoOfDays = 0 Then
                    '        GoTo m
                    '    End If
                    'End If

                    iFLCount = GetPreviousYrFreezeLedgerCount(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                    If iFLCount > 0 Then  'Old Asset

                        If dt1.Rows(i)("AFAA_TrType") = 1 Then

                            dr("TrType") = 1

                            dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                            dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")

                            'dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
                            'Dim dtdel As New DataTable
                            Dim dDiffAmount As Double = 0.0

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    ddelOPBAmt = dtdel.Rows(0)("Amount")
                            '    dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
                            '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                            'Else
                            dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0.00")
                            'End If

                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If

                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim salValue As Double = 0.0
                            salValue = dr("SalvageValue")

                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                                If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
                                    If Val(dr("AssetAge")) <> 0 Then
                                        Dim w As Integer = dr("AssetAge")
                                        dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / w)) * 100)
                                    Else
                                        dr("DepreciationRate") = 0
                                    End If
                                Else
                                    dr("DepreciationRate") = 0
                                End If
                            Else
                                dr("DepreciationRate") = 0
                            End If

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    ddelOPBAmt = dtdel.Rows(0)("Amount")
                            '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                            'Else
                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
                            'End If

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    dr("wrtnvalue") = 0
                            'Else
                            dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                            Dim wdv As Double = 0.0
                            If iNoOfDays > 0 Then
                                wdv = dr("wrtnvalue")
                            Else
                                wdv = 0
                            End If

                            If salValue < wdv Then
                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Else
                                '   dr("DepreciationforFY") = 0
                                '  dr("wrtnvalue") = dr("SalvageValue")
                                Dim dAmount As Double = 0.0
                                dAmount = dr("OPBForYR") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If

                            dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            If dtdel.Rows.Count > 0 Then
                                ddelOPBAmt = dtdel.Rows(0)("Amount")
                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                                dr("wrtnvalue") = 0
                            End If
                        Else

                            dr("TrType") = 1

                            dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                            dr("AddtnAmt") = ""

                            'Dim dtdel As New DataTable
                            Dim dDiffAmount As Double = 0.0

                            dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    ddelAddAmt = dtdel.Rows(0)("Amount")
                            '    dDiffAmount = dOPBAmt - ddelAddAmt
                            '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                            'Else
                            dr("OrignalCost") = dOPBAmt
                            'End If

                            dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")

                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If
                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim salvalue As Double = 0.0
                            salvalue = dr("SalvageValue")

                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                                If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
                                    If Val(dr("AssetAge")) <> 0 Then
                                        Dim w As Integer = dr("AssetAge")
                                        dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / w)) * 100)
                                    Else
                                        dr("DepreciationRate") = 0
                                    End If
                                Else
                                    dr("DepreciationRate") = 0
                                End If
                            Else
                                dr("DepreciationRate") = 0
                            End If


                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    ddelOPBAmt = dtdel.Rows(0)("Amount")
                            '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                            'Else
                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
                            'End If


                            dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                            Dim wdv As Double = 0.0
                            If iNoOfDays > 0 Then
                                wdv = dr("wrtnvalue")
                            Else
                                wdv = 0
                            End If

                            If salvalue < wdv Then
                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Else
                                Dim dAmount As Double = 0.0
                                dAmount = dr("OPBForYR") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If
                        End If
                        dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                        If dtdel.Rows.Count > 0 Then
                            ddelOPBAmt = dtdel.Rows(0)("Amount")
                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                            dr("wrtnvalue") = 0
                        End If

                    Else 'New Asset
                        Dim w As Integer = dr("AssetAge")
                        'dr("OPBForYR") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0.00")
                        '  dRow("Debit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_DebitAmt").ToString()).ToString("#,##0.00")

                        dr("OPBForYR") = Convert.ToDecimal(dt1.Rows(i)("AFAA_FYAmount").ToString()).ToString("#,##0.00")

                        If dt1.Rows(i)("AFAA_TrType") = 1 Then

                            dr("TrType") = "1"

                            dr("AddtnAmt") = ""

                            Dim salValue As Double = 0.0
                            Dim dDiffAmount As Double = 0.0
                            'Dim dtdel As New DataTable

                            'If dtdel.Rows(i)("AFAD_AssetDeletionType") = 1 Then

                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then

                                'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                                'If dtdel.Rows.Count > 0 Then
                                '    ddelOPBAmt = dtdel.Rows(0)("Amount")
                                '    dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
                                '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                                'Else
                                dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
                                'End If

                                If ResidualValue <> 0 Then
                                    dr("Rsdulvalue") = ResidualValue
                                Else
                                    dr("Rsdulvalue") = 0
                                End If
                                dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                                salValue = dr("SalvageValue")
                            Else
                                dr("SalvageValue") = 0
                                dr("OrignalCost") = 0
                            End If
                            'End If

                            'If dtdel.Rows(i)("AFAD_AssetDeletionType") = 2 Then

                            '    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                            '        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")

                            '        If ResidualValue <> 0 Then
                            '            dr("Rsdulvalue") = ResidualValue
                            '        Else
                            '            dr("Rsdulvalue") = 0
                            '        End If
                            '        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            '        salValue = dr("SalvageValue")
                            '    Else
                            '        dr("SalvageValue") = 0
                            '        dr("OrignalCost") = 0
                            '    End If

                            'End If
                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                                If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
                                    If Val(dr("AssetAge")) <> 0 Then
                                        Dim a As Integer = dr("AssetAge")
                                        dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / a)) * 100)
                                    Else
                                        dr("DepreciationRate") = 0
                                    End If
                                Else
                                    dr("DepreciationRate") = 0
                                End If
                            Else
                                dr("DepreciationRate") = 0
                            End If

                            If dr("OrignalCost") <> 0 Then
                                If dr("OPBForYR") <> 0 Then

                                    dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                                    If dtdel.Rows.Count > 0 Then
                                        ddelOPBAmt = dtdel.Rows(0)("Amount")
                                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")

                                    Else
                                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
                                    End If
                                Else
                                    dr("OPBForYR") = 0
                                    dr("DepreciationforFY") = 0
                                End If
                            Else
                                dr("OPBForYR") = 0
                                dr("DepreciationforFY") = 0
                            End If

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    dr("wrtnvalue") = 0
                            'Else
                            If dtdel.Rows.Count > 0 Then
                                dr("wrtnvalue") = 0
                            Else
                                dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Dim wdv As Double = 0.0
                                If iNoOfDays > 0 Then
                                    wdv = dr("wrtnvalue")
                                Else
                                    wdv = 0
                                End If


                                If salValue < wdv Then
                                    dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                                Else
                                    Dim dAmount As Double = 0.0
                                    dAmount = dr("OPBForYR") - dr("SalvageValue")
                                    If dAmount >= 0 Then
                                        If iNoOfDays > 0 Then
                                            dr("DepreciationforFY") = dAmount
                                            dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                        Else
                                            dr("DepreciationforFY") = 0
                                            dr("wrtnvalue") = dr("SalvageValue")
                                        End If
                                    End If
                                End If
                            End If
                            'End If

                            dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            If dtdel.Rows.Count > 0 Then
                                ddelOPBAmt = dtdel.Rows(0)("Amount")
                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                                dr("wrtnvalue") = 0
                            End If

                        ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
                            dr("TrType") = 2

                            dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

                            'Dim dtdel As New DataTable
                            Dim dDiffAmount As Double = 0.0

                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    ddelAddAmt = dtdel.Rows(0)("Amount")
                            '    dDiffAmount = dOPBAmt - ddelAddAmt
                            '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                            'Else
                            dr("OrignalCost") = dOPBAmt
                            'End If
                            'dr("OrignalCost") = dDiffAmount

                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If

                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim salvalue As Double = 0.0
                            salvalue = dr("SalvageValue")

                            If dr("OrignalCost") <> 0 Then
                                dr("AddtnAmt") = dr("OrignalCost") - dr("SalvageValue")
                            Else
                                dr("AddtnAmt") = 0
                            End If

                            'If iNoOfDays > 0 Then
                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
                                If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
                                    If Val(dr("AssetAge")) <> 0 Then
                                        Dim a As Integer = dr("AssetAge")
                                        dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / a)) * 100)
                                    Else
                                        dr("DepreciationRate") = 0
                                    End If
                                Else
                                    dr("DepreciationRate") = 0
                                End If
                            Else
                                dr("DepreciationRate") = 0
                            End If

                            If dr("AddtnAmt") <> 0 Then
                                dr("OPBForYR") = 0
                                If iDuration = 3 Then
                                    If iNoOfDays > 31 Then
                                        iNoOfDays = iNoOfDays
                                    Else
                                        iNoOfDays = iNoOfDays + 1
                                    End If
                                Else
                                    If fDate = 1 Then
                                        iNoOfDays = iNoOfDays
                                    Else
                                        iNoOfDays = iNoOfDays + 1
                                    End If
                                End If

                                'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                                'If dtdel.Rows.Count > 0 Then
                                '    ddelOPBAmt = dtdel.Rows(0)("Amount")
                                '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                                'Else
                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("AddtnAmt") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
                                'End If
                            Else
                                dr("OPBForYR") = 0
                                dr("DepreciationforFY") = 0
                            End If

                            'Else
                            '    dr("DepreciationRate") = 0
                            '    dr("DepreciationforFY") = 0
                            'End If


                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            'If dtdel.Rows.Count > 0 Then
                            '    dr("wrtnvalue") = 0
                            'Else
                            dr("wrtnvalue") = dr("OrignalCost") - dr("DepreciationforFY")

                            Dim wdv As Double = 0.0



                            If iNoOfDays > 0 Then
                                wdv = dr("wrtnvalue")
                            Else
                                If iDuration = 3 Then
                                    wdv = dr("wrtnvalue")
                                Else
                                    wdv = 0
                                End If
                            End If


                            If salvalue < wdv Then
                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OrignalCost") - dr("DepreciationforFY"))).ToString("#,##0")
                            Else
                                '    dr("DepreciationforFY") = 0
                                '    dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("SalvageValue"))).ToString("#,##0")
                                'End If
                                Dim dAmount As Double = 0.0
                                dAmount = dr("AddtnAmt") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    If iNoOfDays > 0 Then
                                        dr("DepreciationforFY") = dAmount
                                        dr("wrtnvalue") = dr("AddtnAmt") - dr("DepreciationforFY")
                                    Else
                                        dr("DepreciationforFY") = 0
                                        dr("wrtnvalue") = dr("SalvageValue")
                                    End If
                                End If
                            End If

                            dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            If dtdel.Rows.Count > 0 Then
                                ddelOPBAmt = dtdel.Rows(0)("Amount")
                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
                                dr("wrtnvalue") = 0
                            End If

                        End If
m:                  End If

                    'End If

                    dt.Rows.Add(dr)
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadDepreciationCompWDV(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal NoOfDays As Integer, ByVal TNoOfDays As Integer, ByVal iDuration As Integer,
    '                                          sStartDt As Date, sEndDate As Date, ByVal iCustID As Integer, ByVal iMethod As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1, dt2 As New DataTable
    '    Dim ds1 As New DataSet
    '    Dim dr As DataRow, dr2 As DataRow
    '    Dim dDeprec As Double = 0
    '    Dim iNoOfDays As Integer = 0
    '    Dim dtAsstDetails As New DataTable
    '    Dim dOPBAmt As Double = 0.0
    '    Dim ddelOPBAmt As Double = 0.0
    '    Dim ddelAddAmt As Double = 0.0
    '    Dim dOPBPreviousAmt As Double = 0.0
    '    Dim dtdel As New DataTable

    '    Dim iFLCount As Integer = 0
    '    Dim dPreviousOPBAmt As Double = 0.0
    '    Dim dAddtnAmt As Double = 0.0
    '    Dim ddate As Date
    '    Try
    '        dt.Columns.Add("AssetClassID")
    '        dt.Columns.Add("AssetID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        ' dt.Columns.Add("AssetLocationCode")
    '        dt.Columns.Add("Location")
    '        dt.Columns.Add("Division")
    '        dt.Columns.Add("Department")
    '        dt.Columns.Add("Bay")
    '        dt.Columns.Add("LocationID")
    '        dt.Columns.Add("DivisionID")
    '        dt.Columns.Add("DepartmentID")
    '        dt.Columns.Add("BayID")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("TrType")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("Item")
    '        dt.Columns.Add("OrignalCost")
    '        dt.Columns.Add("Rsdulvalue")
    '        dt.Columns.Add("SalvageValue")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("DepreciationRate")
    '        dt.Columns.Add("AddtnAmt")
    '        dt.Columns.Add("OPBForYR")
    '        dt.Columns.Add("DepreciationforFY")
    '        dt.Columns.Add("wrtnvalue")

    '        sSql = "Select distinct(AFAA_TrType),AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustID & " and AFAA_YearID <=" & iYearId & " order by AFAA_ItemType "
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            '  For i = 0 To dt1.Rows.Count - 1
    '            For i = 305 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & "")

    '                dr("LocationID") = dt1.Rows(i)("AFAA_Location")
    '                dr("DivisionID") = dt1.Rows(i)("AFAA_Division")
    '                dr("DepartmentID") = dt1.Rows(i)("AFAA_Department")
    '                dr("BayID") = dt1.Rows(i)("AFAA_Bay")

    '                'dr("AssetLocationCode") = dt1.Rows(i)("AFAA_ItemType")
    '                dr("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Location") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
    '                dr("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Division") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
    '                dr("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Department") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")
    '                dr("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Bay") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & "")

    '                dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustID)
    '                If dtAsstDetails.Rows.Count > 0 Then
    '                    dr("AssetCode") = dtAsstDetails.Rows(0)("AssetCode")
    '                    dr("AssetAge") = dtAsstDetails.Rows(0)("AssetAge")
    '                    ddate = dtAsstDetails.Rows(0)("PurchaseDate")
    '                Else
    '                    dr("AssetCode") = ""
    '                    dr("AssetAge") = 0
    '                    dr("PurchaseDate") = "01-01-1900"
    '                End If

    '                If ddate > sStartDt Then
    '                    If ddate <= sEndDate Then
    '                        iNoOfDays = DateDiff(DateInterval.Day, ddate, sEndDate)
    '                    ElseIf ddate > sEndDate Then
    '                        iNoOfDays = 0
    '                    End If
    '                Else
    '                    iNoOfDays = NoOfDays
    '                End If
    '                dr("NoOfDays") = iNoOfDays

    '                dr("PurchaseDate") = objFAS.FormatDtForRDBMS(ddate, "D")

    '                If dr("PurchaseDate") = "01/01/1900" Then
    '                    iNoOfDays = 0
    '                    dr("NoOfDays") = 0
    '                    dr("PurchaseDate") = ""
    '                End If

    '                dr("Item") = objDBL.SQLExecuteScalar(sNameSpace, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_ID= " & dt1.Rows(i)("AFAA_ItemType") & " and AFAM_CustId=" & iCustID & " and AFAM_Location=" & dt1.Rows(i)("AFAA_Location") & " and AFAM_Division=" & dt1.Rows(i)("AFAA_Division") & " and AFAM_Department=" & dt1.Rows(i)("AFAA_Department") & " and AFAM_Bay=" & dt1.Rows(i)("AFAA_Bay") & "")
    '                Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CustId=" & iCustID & "")

    '                iFLCount = GetPreviousYrFreezeLedgerCount(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

    '                If iFLCount > 0 Then  'Old Asset

    '                    If dt1.Rows(i)("AFAA_TrType") = 1 Then

    '                        dr("TrType") = 1

    '                        dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

    '                        dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")

    '                        'dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                        'Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                        '    dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
    '                        '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        'Else
    '                        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0.00")
    '                        'End If

    '                        If ResidualValue <> 0 Then
    '                            dr("Rsdulvalue") = ResidualValue
    '                        Else
    '                            dr("Rsdulvalue") = 0
    '                        End If

    '                        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                        Dim salValue As Double = 0.0
    '                        salValue = dr("SalvageValue")

    '                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                            If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                                If Val(dr("AssetAge")) <> 0 Then
    '                                    Dim w As Integer = dr("AssetAge")
    '                                    dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / w)) * 100)
    '                                Else
    '                                    dr("DepreciationRate") = 0
    '                                End If
    '                            Else
    '                                dr("DepreciationRate") = 0
    '                            End If
    '                        Else
    '                            dr("DepreciationRate") = 0
    '                        End If

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                        '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                        'Else
    '                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                        'End If

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    dr("wrtnvalue") = 0
    '                        'Else
    '                        dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                        Dim wdv As Double = 0.0
    '                        wdv = dr("wrtnvalue")

    '                        If salValue < wdv Then
    '                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                        Else
    '                            '   dr("DepreciationforFY") = 0
    '                            '  dr("wrtnvalue") = dr("SalvageValue")
    '                            Dim dAmount As Double = 0.0
    '                            dAmount = dr("OPBForYR") - dr("SalvageValue")
    '                            If dAmount > 0 Then
    '                                dr("DepreciationforFY") = dAmount
    '                                dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                            Else
    '                                dr("DepreciationforFY") = 0
    '                                dr("wrtnvalue") = dr("SalvageValue")
    '                            End If
    '                        End If

    '                        dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                            dr("wrtnvalue") = 0
    '                        End If
    '                    Else

    '                        dr("TrType") = 2

    '                        dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

    '                        dr("AddtnAmt") = ""

    '                        'Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    ddelAddAmt = dtdel.Rows(0)("Amount")
    '                        '    dDiffAmount = dOPBAmt - ddelAddAmt
    '                        '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        'Else
    '                        dr("OrignalCost") = dOPBAmt
    '                        'End If

    '                        dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")

    '                        If ResidualValue <> 0 Then
    '                            dr("Rsdulvalue") = ResidualValue
    '                        Else
    '                            dr("Rsdulvalue") = 0
    '                        End If
    '                        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                        Dim salvalue As Double = 0.0
    '                        salvalue = dr("SalvageValue")

    '                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                            If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                                If Val(dr("AssetAge")) <> 0 Then
    '                                    Dim w As Integer = dr("AssetAge")
    '                                    dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / w)) * 100)
    '                                Else
    '                                    dr("DepreciationRate") = 0
    '                                End If
    '                            Else
    '                                dr("DepreciationRate") = 0
    '                            End If
    '                        Else
    '                            dr("DepreciationRate") = 0
    '                        End If


    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                        '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                        'Else
    '                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                        'End If


    '                        dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                        Dim wdv As Double = 0.0
    '                        wdv = dr("wrtnvalue")

    '                        If salvalue < wdv Then
    '                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                        Else
    '                            Dim dAmount As Double = 0.0
    '                            dAmount = dr("OPBForYR") - dr("SalvageValue")
    '                            If dAmount > 0 Then
    '                                dr("DepreciationforFY") = dAmount
    '                                dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                            Else
    '                                dr("DepreciationforFY") = 0
    '                                dr("wrtnvalue") = dr("SalvageValue")
    '                            End If
    '                        End If
    '                    End If
    '                    dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                    If dtdel.Rows.Count > 0 Then
    '                        ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                        dr("wrtnvalue") = 0
    '                    End If

    '                Else 'New Asset
    '                    Dim w As Integer = dr("AssetAge")
    '                    'dr("OPBForYR") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0.00")
    '                    '  dRow("Debit") = Convert.ToDecimal(dtGL.Rows(i)("Opn_DebitAmt").ToString()).ToString("#,##0.00")

    '                    dr("OPBForYR") = Convert.ToDecimal(dt1.Rows(i)("AFAA_FYAmount").ToString()).ToString("#,##0.00")

    '                    If dt1.Rows(i)("AFAA_TrType") = 1 Then

    '                        dr("TrType") = "1"

    '                        dr("AddtnAmt") = ""

    '                        Dim salValue As Double = 0.0
    '                        Dim dDiffAmount As Double = 0.0
    '                        'Dim dtdel As New DataTable

    '                        'If dtdel.Rows(i)("AFAD_AssetDeletionType") = 1 Then

    '                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then

    '                            'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                            'If dtdel.Rows.Count > 0 Then
    '                            '    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                            '    dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
    '                            '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                            'Else
    '                            dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                            'End If

    '                            If ResidualValue <> 0 Then
    '                                dr("Rsdulvalue") = ResidualValue
    '                            Else
    '                                dr("Rsdulvalue") = 0
    '                            End If
    '                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                            salValue = dr("SalvageValue")
    '                        Else
    '                            dr("SalvageValue") = 0
    '                            dr("OrignalCost") = 0
    '                        End If
    '                        'End If

    '                        'If dtdel.Rows(i)("AFAD_AssetDeletionType") = 2 Then

    '                        '    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                        '        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")

    '                        '        If ResidualValue <> 0 Then
    '                        '            dr("Rsdulvalue") = ResidualValue
    '                        '        Else
    '                        '            dr("Rsdulvalue") = 0
    '                        '        End If
    '                        '        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                        '        salValue = dr("SalvageValue")
    '                        '    Else
    '                        '        dr("SalvageValue") = 0
    '                        '        dr("OrignalCost") = 0
    '                        '    End If

    '                        'End If
    '                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                            If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                                If Val(dr("AssetAge")) <> 0 Then
    '                                    Dim a As Integer = dr("AssetAge")
    '                                    dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / a)) * 100)
    '                                Else
    '                                    dr("DepreciationRate") = 0
    '                                End If
    '                            Else
    '                                dr("DepreciationRate") = 0
    '                            End If
    '                        Else
    '                            dr("DepreciationRate") = 0
    '                        End If

    '                        If dr("OrignalCost") <> 0 Then
    '                            If dr("OPBForYR") <> 0 Then

    '                                dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                                If dtdel.Rows.Count > 0 Then
    '                                    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                                    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")

    '                                Else
    '                                    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                                End If
    '                            Else
    '                                dr("OPBForYR") = 0
    '                                dr("DepreciationforFY") = 0
    '                            End If
    '                        Else
    '                            dr("OPBForYR") = 0
    '                            dr("DepreciationforFY") = 0
    '                        End If

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    dr("wrtnvalue") = 0
    '                        'Else
    '                        If dtdel.Rows.Count > 0 Then
    '                            dr("wrtnvalue") = 0
    '                        Else
    '                            dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                            Dim wdv As Double = 0.0
    '                            wdv = dr("wrtnvalue")

    '                            If salValue < wdv Then
    '                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                            Else
    '                                Dim dAmount As Double = 0.0
    '                                dAmount = dr("OPBForYR") - dr("SalvageValue")
    '                                If dAmount > 0 Then
    '                                    dr("DepreciationforFY") = dAmount
    '                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
    '                                Else


    '                                    dr("DepreciationforFY") = 0
    '                                    dr("wrtnvalue") = dr("SalvageValue")
    '                                End If
    '                            End If
    '                        End If
    '                        'End If

    '                        dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                            dr("wrtnvalue") = 0
    '                        End If

    '                    ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                        dr("TrType") = 2

    '                        dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        'Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    ddelAddAmt = dtdel.Rows(0)("Amount")
    '                        '    dDiffAmount = dOPBAmt - ddelAddAmt
    '                        '    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        'Else
    '                        dr("OrignalCost") = dOPBAmt
    '                        'End If
    '                        'dr("OrignalCost") = dDiffAmount

    '                        If ResidualValue <> 0 Then
    '                            dr("Rsdulvalue") = ResidualValue
    '                        Else
    '                            dr("Rsdulvalue") = 0
    '                        End If

    '                        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                        Dim salvalue As Double = 0.0
    '                        salvalue = dr("SalvageValue")

    '                        If dr("OrignalCost") <> 0 Then
    '                            dr("AddtnAmt") = dr("OrignalCost") - dr("SalvageValue")
    '                        Else
    '                            dr("AddtnAmt") = 0
    '                        End If

    '                        If iNoOfDays > 0 Then
    '                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                                If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                                    If Val(dr("AssetAge")) <> 0 Then
    '                                        Dim a As Integer = dr("AssetAge")
    '                                        dr("DepreciationRate") = String.Format("{0:0.00}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / a)) * 100)
    '                                    Else
    '                                        dr("DepreciationRate") = 0
    '                                    End If
    '                                Else
    '                                    dr("DepreciationRate") = 0
    '                                End If
    '                            Else
    '                                dr("DepreciationRate") = 0
    '                            End If
    '                        End If

    '                        If dr("AddtnAmt") <> 0 Then
    '                                'dr("OPBForYR") = 0
    '                                'iNoOfDays = iNoOfDays + 1
    '                                Dim ToDate As Date
    '                                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From Year_Master Where YMS_YEARID=" & iYearId & "")
    '                                Dim Adays As Integer

    '                                If iDuration = 0 Then
    '                                    Adays = DateDiff(DateInterval.Day, ddate, ToDate)
    '                                    Adays = Adays + 1
    '                                    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("AddtnAmt") * dr("DepreciationRate")) / 100) * (Adays / TNoOfDays))).ToString("#,##0")
    '                                Else
    '                                    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("AddtnAmt") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                                End If
    '                                'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                                'If dtdel.Rows.Count > 0 Then
    '                                '    ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                                '    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                                'Else


    '                                'End If
    '                            Else
    '                                dr("OPBForYR") = 0
    '                                dr("DepreciationforFY") = 0
    '                            End If

    '                        'Else
    '                        '    dr("DepreciationRate") = 0
    '                        '    dr("DepreciationforFY") = 0
    '                        'End If


    '                        'dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        'If dtdel.Rows.Count > 0 Then
    '                        '    dr("wrtnvalue") = 0
    '                        'Else
    '                        dr("wrtnvalue") = dr("OrignalCost") - dr("DepreciationforFY")

    '                        Dim wdv As Double = 0.0
    '                        wdv = dr("wrtnvalue")

    '                        If salvalue < wdv Then
    '                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OrignalCost") - dr("DepreciationforFY"))).ToString("#,##0")
    '                        Else
    '                            '    dr("DepreciationforFY") = 0
    '                            '    dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("SalvageValue"))).ToString("#,##0")
    '                            'End If
    '                            Dim dAmount As Double = 0.0
    '                            dAmount = dr("AddtnAmt") - dr("SalvageValue")
    '                            If dAmount > 0 Then
    '                                dr("DepreciationforFY") = dAmount
    '                                dr("wrtnvalue") = dr("AddtnAmt") - dr("DepreciationforFY")
    '                            Else
    '                                dr("DepreciationforFY") = 0
    '                                dr("wrtnvalue") = dr("SalvageValue")
    '                            End If
    '                        End If

    '                        dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustID, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(ddelOPBAmt)).ToString("#,##0")
    '                            dr("wrtnvalue") = 0
    '                        End If

    '                    End If
    '                End If
    '                'End If

    '                dt.Rows.Add(dr)
    '            Next
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function LoadDepreciationITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal dEnddate As Date)
    '    Dim sSql As String = ""
    '    Dim dt, dt1, dt2 As New DataTable
    '    Dim ds1 As New DataSet
    '    Dim dr As DataRow, dr2 As DataRow
    '    Dim dDeprec As Double = 0
    '    Dim iNoOfDays As Integer = 0
    '    Dim dtAsstDetails As New DataTable
    '    Dim dOPBAmt As Double = 0.0
    '    Dim ddate As Date
    '    Dim sStartDt As Date
    '    Dim sEndDate As Date
    '    Dim dOPBPreviousAmt As Double = 0.0

    '    Dim iFLCount As Integer = 0
    '    Dim dPreviousOPBAmt As Double = 0.0
    '    Dim dAddtnAmt As Double = 0.0

    '    Dim ddelOPBAmt As Double = 0.0
    '    Dim ddelAddAmt As Double = 0.0
    '    Dim rateOfPer As Double = 0.0
    '    Try
    '        dt.Columns.Add("AssetClassID")
    '        dt.Columns.Add("AssetID")
    '        dt.Columns.Add("TrType")
    '        dt.Columns.Add("Days")
    '        dt.Columns.Add("ClassofAsset")
    '        dt.Columns.Add("OriginalCost")
    '        dt.Columns.Add("RateofDep")
    '        dt.Columns.Add("WDVOpeningValue")
    '        dt.Columns.Add("AdditionDuringtheYear")
    '        dt.Columns.Add("TotalWDV")
    '        dt.Columns.Add("Depfortheperiod")
    '        dt.Columns.Add("WDVClosingValue")

    '        sSql = "Select distinct(AFAA_TrType),AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType,AFAA_Location,AFAA_Division,
    '                    AFAA_Department,AFAA_Bay from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & " order by AFAA_ItemType"
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
    '                dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")

    '                rateOfPer = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "  And AM_CompID=" & iCompID & "")
    '                dr("RateofDep") = rateOfPer

    '                dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustId)
    '                If dtAsstDetails.Rows.Count > 0 Then
    '                    ddate = dtAsstDetails.Rows(0)("PurchaseDate")
    '                Else
    '                    ddate = "01/01/1900"
    '                End If

    '                iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
    '                If iNoOfDays < 180 Then
    '                    rateOfPer = rateOfPer
    '                    dr("Days") = "L"
    '                ElseIf iNoOfDays >= 180 Then
    '                    rateOfPer = rateOfPer / 2
    '                    dr("Days") = "M"
    '                End If
    '                dr("TotalWDV") = 0

    '                iFLCount = GetPreviousYrFLedgerCountITAct(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                If iFLCount > 0 Then  'Old Asset 'Opening Balance

    '                    If dt1.Rows(i)("AFAA_TrType") = 1 Then

    '                        dr("TrType") = 1

    '                        dt2 = GetFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        dr("WDVOpeningValue") = dt2.Rows(0)("ADITAct_WrittenDownValue")

    '                        'dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                        Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                            dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
    '                            dr("OriginalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        Else
    '                            dr("OriginalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                        End If

    '                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("WDVOpeningValue") * rateOfPer) / 100))).ToString("#,##0")

    '                        dr("WDVClosingValue") = dr("WDVOpeningValue") - dr("Depfortheperiod")

    '                    Else 'Old 'Addition

    '                        dr("TrType") = 2

    '                        dt2 = GetFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        dr("AdditionDuringtheYear") = ""

    '                        Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        dOPBAmt = GetAmountIT(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelAddAmt = dtdel.Rows(0)("Amount")
    '                            dDiffAmount = dOPBAmt - ddelAddAmt
    '                            dr("OriginalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        Else
    '                            dr("OriginalCost") = dOPBAmt
    '                        End If

    '                        dr("WDVOpeningValue") = dt2.Rows(0)("ADITAct_WrittenDownValue")


    '                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("WDVOpeningValue") * rateOfPer) / 100))).ToString("#,##0")

    '                        dr("WDVClosingValue") = dr("WDVOpeningValue") - dr("Depfortheperiod")

    '                    End If

    '                Else 'New Asset 'Opening Balance

    '                    dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0")

    '                    If dt1.Rows(i)("AFAA_TrType") = 1 Then

    '                        dr("TrType") = "1"

    '                        dr("AdditionDuringtheYear") = ""

    '                        Dim salValue As Double = 0.0
    '                        Dim dDiffAmount As Double = 0.0
    '                        Dim dtdel As New DataTable

    '                        If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then

    '                            dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                            If dtdel.Rows.Count > 0 Then
    '                                ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                                dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
    '                                dr("OriginalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                            Else
    '                                dr("OriginalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                            End If
    '                        Else

    '                            dr("OriginalCost") = 0
    '                        End If
    '                        'End If

    '                        'If dtdel.Rows(i)("AFAD_AssetDeletionType") = 2 Then

    '                        '    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                        '        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")

    '                        '        If ResidualValue <> 0 Then
    '                        '            dr("Rsdulvalue") = ResidualValue
    '                        '        Else
    '                        '            dr("Rsdulvalue") = 0
    '                        '        End If
    '                        '        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                        '        salValue = dr("SalvageValue")
    '                        '    Else
    '                        '        dr("SalvageValue") = 0
    '                        '        dr("OrignalCost") = 0
    '                        '    End If

    '                        'End If
    '                        If dr("OriginalCost") <> 0 Then
    '                            If dr("WDVOpeningValue") <> 0 Then
    '                                dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("WDVOpeningValue") * rateOfPer) / 100))).ToString("#,##0")
    '                            Else
    '                                dr("WDVOpeningValue") = 0
    '                                dr("Depfortheperiod") = 0
    '                            End If
    '                        Else
    '                            dr("WDVOpeningValue") = 0
    '                            dr("Depfortheperiod") = 0
    '                        End If


    '                        dr("WDVClosingValue") = dr("WDVOpeningValue") - dr("Depfortheperiod")


    '                    ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then 'new Addition

    '                        dr("TrType") = 2

    '                        dOPBAmt = GetAmountIT(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelAddAmt = dtdel.Rows(0)("Amount")
    '                            dDiffAmount = dOPBAmt - ddelAddAmt
    '                            dr("OriginalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        Else
    '                            dr("OriginalCost") = dOPBAmt
    '                        End If
    '                        'dr("OrignalCost") = dDiffAmount


    '                        If dr("OriginalCost") <> 0 Then
    '                            dr("AdditionDuringtheYear") = dr("OriginalCost")
    '                        Else
    '                            dr("AdditionDuringtheYear") = 0
    '                        End If

    '                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("AdditionDuringtheYear") * rateOfPer) / 100))).ToString("#,##0")

    '                        '    If dr("AddtnAmt") <> 0 Then
    '                        '        dr("OPBForYR") = 0
    '                        '        iNoOfDays = iNoOfDays + 1
    '                        '        '     dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("AddtnAmt") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                        '    Else
    '                        '        dr("OPBForYR") = 0
    '                        '        dr("DepreciationforFY") = 0
    '                        '    End If

    '                        'Else
    '                        '    dr("DepreciationRate") = 0
    '                        '    dr("DepreciationforFY") = 0
    '                        'End If

    '                        dr("WDVClosingValue") = dr("OriginalCost") - dr("Depfortheperiod")
    '                    End If
    '                End If
    '                dt.Rows.Add(dr)
    '            Next
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function LoadDepreciationITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal dEnddate As Date)
    '    Dim sSql As String = ""
    '    Dim dt, dt1, dt2 As New DataTable
    '    Dim ds1 As New DataSet
    '    Dim dr As DataRow, dr2 As DataRow
    '    Dim dDeprec As Double = 0
    '    Dim iNoOfDays As Integer = 0
    '    Dim dtAsstDetails As New DataTable
    '    Dim dOPBAmt As Double = 0.0
    '    Dim ddate As Date
    '    Dim depforopeningVlaue As Double = 0.0
    '    Dim depforAddition As Double = 0.0

    '    Dim dOPBPreviousAmt As Double = 0.0

    '    Dim iFLCount As Integer = 0
    '    Dim dPreviousOPBAmt As Double = 0.0
    '    Dim dAddtnAmt As Double = 0.0

    '    Dim ddelOPBAmt As Double = 0.0
    '    Dim ddelAddAmt As Double = 0.0
    '    Dim rateOfPer As Double = 0.0
    '    Dim WDVOpeningValue As Double = 0.0
    '    Dim dAdditionAmount As Double = 0.0
    '    Dim opbamount As Double = 0.0
    '    Try
    '        dt.Columns.Add("AssetClassID")
    '        dt.Columns.Add("AssetID")
    '        dt.Columns.Add("TrType")
    '        dt.Columns.Add("Days")
    '        dt.Columns.Add("ClassofAsset")
    '        dt.Columns.Add("OriginalCost")
    '        dt.Columns.Add("RateofDep")
    '        dt.Columns.Add("WDVOpeningValue")
    '        dt.Columns.Add("AdditionDuringtheYear")
    '        'dt.Columns.Add("TotalWDV")
    '        dt.Columns.Add("Depfortheperiod")
    '        dt.Columns.Add("WDVClosingValue")

    '        sSql = "Select distinct(AFAA_TrType),AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType,AFAA_Location,AFAA_Division,
    '                    AFAA_Department,AFAA_Bay from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & " and AFAA_YearID=" & iYearId & " and AFAA_TrType=2 order by AFAA_ItemType"
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
    '                dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")

    '                rateOfPer = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "  And AM_CompID=" & iCompID & "")
    '                dr("RateofDep") = rateOfPer

    '                dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustId)
    '                If dtAsstDetails.Rows.Count > 0 Then
    '                    ddate = dtAsstDetails.Rows(0)("PurchaseDate")
    '                Else
    '                    ddate = "01/01/1900"
    '                End If

    '                iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
    '                If iNoOfDays <= 180 Then
    '                    rateOfPer = rateOfPer / 2
    '                    dr("Days") = "L"
    '                ElseIf iNoOfDays > 180 Then
    '                    rateOfPer = rateOfPer
    '                    dr("Days") = "M"
    '                End If
    '                'dr("TotalWDV") = 0

    '                iFLCount = GetPreviousYrFLedgerCountITAct(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                If iFLCount > 0 Then  'Old Asset Opening Balance
    '                    'If dt1.Rows(i)("AFAA_TrType") = 1 Then

    '                    dr("TrType") = "1"

    '                    dt2 = GetFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                    WDVOpeningValue = dt2.Rows(0)("ADITAct_WrittenDownValue")

    '                    Dim dtdel As New DataTable
    '                    Dim dDiffAmount As Double = 0.0

    '                    dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iYearId)
    '                    If dtdel.Rows.Count > 0 Then
    '                        ddelOPBAmt = dtdel.Rows(0)("Amount")
    '                        dDiffAmount = WDVOpeningValue - ddelOPBAmt
    '                        dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        opbamount = dr("WDVOpeningValue")
    '                    Else
    '                        dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(WDVOpeningValue)).ToString("#,##0")
    '                        opbamount = dr("WDVOpeningValue")
    '                    End If

    '                    dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("WDVOpeningValue") * rateOfPer) / 100))).ToString("#,##0")

    '                    dr("WDVClosingValue") = dr("WDVOpeningValue") - dr("Depfortheperiod")

    '                    'ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                    '    '    dr("TrType") = "2"

    '                    '    dAdditionAmount = GetAdditionAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), 0, iYearId, iCustId, 0, 0, 0, 0)

    '                    '    'dAddtnAmt = dt2.Rows(0)("ADITAct_WrittenDownValue")
    '                    '    If dAdditionAmount <> 0 Then
    '                    '        dr("AdditionDuringtheYear") = Convert.ToDecimal(Math.Round(dAdditionAmount)).ToString("#,##0")
    '                    '        dAddtnAmt = dr("AdditionDuringtheYear")
    '                    '    Else
    '                    '        dAddtnAmt = 0
    '                    '        dr("AdditionDuringtheYear") = 0
    '                    '    End If

    '                    '    dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("AdditionDuringtheYear") * rateOfPer) / 100))).ToString("#,##0")

    '                    '    dr("WDVClosingValue") = dr("AdditionDuringtheYear") - dr("Depfortheperiod")
    '                    'End If

    '                    'dr("Depfortheperiod") = ((opbamount + dAddtnAmt) / rateOfPer) / 100
    '                    'dr("WDVClosingValue") = 0
    '                    dt.Rows.Add(dr)
    '                End If 'New Asset Opening Balance


    '                If dt1.Rows(i)("AFAA_TrType") = 1 Then
    '                    dr = dt.NewRow

    '                    dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                    dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
    '                    dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")

    '                    rateOfPer = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "  And AM_CompID=" & iCompID & "")
    '                    dr("RateofDep") = rateOfPer

    '                    dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustId)
    '                    If dtAsstDetails.Rows.Count > 0 Then
    '                        ddate = dtAsstDetails.Rows(0)("PurchaseDate")
    '                    Else
    '                        ddate = "01/01/1900"
    '                    End If

    '                    iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
    '                    If iNoOfDays <= 180 Then
    '                        rateOfPer = rateOfPer / 2
    '                        dr("Days") = "L"
    '                    ElseIf iNoOfDays > 180 Then
    '                        rateOfPer = rateOfPer
    '                        dr("Days") = "M"
    '                    End If

    '                    dr("TrType") = "1"

    '                        Dim dtdel As New DataTable
    '                        Dim dDiffAmount As Double = 0.0

    '                        dOPBAmt = objDBL.SQLGetDescription(sNameSpace, "Select AM_WDVITAct From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "  And AM_CompID=" & iCompID & "")

    '                        dtdel = GetDelAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iYearId)
    '                        If dtdel.Rows.Count > 0 Then
    '                            ddelAddAmt = dtdel.Rows(0)("Amount")
    '                            dDiffAmount = dOPBAmt - ddelAddAmt
    '                            dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
    '                        Else
    '                            dr("WDVOpeningValue") = dOPBAmt
    '                        End If

    '                        dr("AdditionDuringtheYear") = ""

    '                        dr("OriginalCost") = 0

    '                        If dr("WDVOpeningValue") <> 0 Then
    '                            dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("WDVOpeningValue") * rateOfPer) / 100))).ToString("#,##0")
    '                        Else
    '                            dr("Depfortheperiod") = 0
    '                        End If

    '                        dr("WDVClosingValue") = dr("WDVOpeningValue") - dr("Depfortheperiod")

    '                    ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then 'new Addition
    '                    dr = dt.NewRow

    '                    dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                    dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")
    '                    dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")

    '                    rateOfPer = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "  And AM_CompID=" & iCompID & "")
    '                    dr("RateofDep") = rateOfPer

    '                    dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustId)
    '                    If dtAsstDetails.Rows.Count > 0 Then
    '                        ddate = dtAsstDetails.Rows(0)("PurchaseDate")
    '                    Else
    '                        ddate = "01/01/1900"
    '                    End If

    '                    iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
    '                    If iNoOfDays <= 180 Then
    '                        rateOfPer = rateOfPer / 2
    '                        dr("Days") = "L"
    '                    ElseIf iNoOfDays > 180 Then
    '                        rateOfPer = rateOfPer
    '                        dr("Days") = "M"
    '                    End If

    '                    dr("TrType") = 2

    '                        dr("WDVOpeningValue") = 0

    '                        dAddtnAmt = GetAmountIT(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))

    '                        dr("AdditionDuringtheYear") = dAddtnAmt

    '                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(((dr("AdditionDuringtheYear") * rateOfPer) / 100))).ToString("#,##0")

    '                        dr("WDVClosingValue") = dr("AdditionDuringtheYear") - dr("Depfortheperiod")
    '                    End If



    '                    dt.Rows.Add(dr)
    '            Next
    '        End If

    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadDepreciationITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer, ByVal dEnddate As Date)
        Dim sSql As String = ""
        Dim dt, dt1, dtAddDetails As New DataTable
        Dim ds1 As New DataSet
        Dim dr As DataRow, dr2 As DataRow

        Dim iNoOfDays As Integer = 0
        Dim ddate As Date
        Dim iPreviousYearid As Integer
        Dim iCount As Integer
        Dim ddelAmount As Double = 0.0
        Dim dWDVOpeningAmount As Double = 0.0
        Dim dOpeningValue As Double = 0.0
        Dim dWDVClosingAmount As Double = 0.0
        Dim rateOfPer As Double = 0.0
        Dim dMoreThan180days As Double = 0.0
        Dim dLessThan180days As Double = 0.0
        Dim ddepMoreThan180days As Double = 0.0
        Dim ddepLessThan180days As Double = 0.0
        Dim MtotaldepAmount As Double = 0.0
        Dim LtotaldepaMOUNT As Double = 0.0
        Dim dDepfortheperiod As Double = 0.0
        Dim dTotal As Double = 0.0
        Dim iInitDep As Integer = 0
        Dim dInitDepAmt As Double = 0
        Dim dPrevInitDepAmt As Double = 0
        Dim iInitDepTot As Double = 0.0
        Dim iNextYrCarry As Double = 0.0
        If iYearId <> 0 Then
            iPreviousYearid = iYearId - 1
        End If
        Try
            dt.Columns.Add("AssetClassID")
            dt.Columns.Add("ClassofAsset")
            dt.Columns.Add("RateofDep")
            dt.Columns.Add("BfrQtrAmount")
            dt.Columns.Add("BfrQtrDep")
            dt.Columns.Add("AftQtrAmount")
            dt.Columns.Add("AftQtrDep")
            dt.Columns.Add("DelAmount")
            dt.Columns.Add("WDVOpeningValue")
            dt.Columns.Add("WDVOpeningDepreciation")
            dt.Columns.Add("AdditionDuringtheYear")
            dt.Columns.Add("AdditionDepreciation")
            dt.Columns.Add("Depfortheperiod")
            dt.Columns.Add("InitDepAmt")
            dt.Columns.Add("PrevInitDepAmt")
            dt.Columns.Add("WDVClosingValue")
            dt.Columns.Add("NextYrCarry")


            sSql = "select count(ADITAct_ID) as id from Acc_AssetDepITAct where ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iPreviousYearid & " and ADITAct_CompID=" & iCompID & ""
            iCount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            If iCount = 0 Then
                sSql = "select AM_ID,AM_Description,AM_WDVITAct,AM_ITRate from Acc_AssetMaster Where AM_CompID=" & iCompID & " and AM_LevelCode=2 and AM_CustId=" & iCustId & ""
                dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt1.Rows.Count > 0 Then
                    For i = 0 To dt1.Rows.Count - 1
                        dr = dt.NewRow
                        dr("AssetClassID") = dt1.Rows(i)("AM_ID")
                        dr("ClassofAsset") = dt1.Rows(i)("AM_Description")
                        ddelAmount = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(AFAD_SalesPrice),0) From Acc_FixedAssetDeletion Where AFAD_AssetClass=" & dt1.Rows(i)("AM_ID") & " and AFAD_CompID=" & iCompID & " and AFAD_CustomerName=" & iCustId & " and AFAD_YearID=" & iYearId & "")
                        dOpeningValue = dt1.Rows(i)("AM_WDVITAct")
                        If ddelAmount <> 0 Then
                            dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dOpeningValue - ddelAmount)).ToString("#,##0")
                        Else
                            dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dOpeningValue)).ToString("#,##0")
                        End If
                        dr("DelAmount") = Convert.ToDecimal(Math.Round(ddelAmount)).ToString("#,##0")
                        dr("RateofDep") = dt1.Rows(i)("AM_ITRate")
                        rateOfPer = dr("RateofDep")
                        dDepfortheperiod = (dr("WDVOpeningValue") * dr("RateofDep")) / 100 'NO NEED
                        dWDVClosingAmount = dr("WDVOpeningValue") - dDepfortheperiod  'NO NEED
                        sSql = "select b.FAAD_ItemType,a.AFAM_CommissionDate,isnull(sum(b.FAAD_AssetValue),0) as FAAD_AssetValue, isnull(FAAD_InitDep,0) as FAAD_InitDep from Acc_FixedAssetMaster a"
                        sSql = sSql & " left join  Acc_FixedAssetAdditionDetails b on a.AFAM_ID=b.FAAD_ItemType where  b.FAAD_ItemType<>'' and FAAD_YearID=" & iYearId & " and FAAD_Delflag = 'A'"
                        sSql = sSql & " and FAAD_CustId=" & iCustId & " and FAAD_Status<>'D' and FAAD_CompID=" & iCompID & " and FAAD_AssetType=" & dt1.Rows(i)("AM_ID") & " group by FAAD_ItemType,AFAM_CommissionDate,FAAD_InitDep"
                        dtAddDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        If dtAddDetails.Rows.Count > 0 Then
                            For j = 0 To dtAddDetails.Rows.Count - 1
                                ddate = dtAddDetails.Rows(j)("AFAM_CommissionDate")
                                iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
                                If iNoOfDays <= 180 Then
                                    Dim FAAD_AssetValue As Double = 0.0
                                    FAAD_AssetValue = dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    dLessThan180days = dLessThan180days + dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    If dtAddDetails.Rows(j)("FAAD_InitDep") = 1 Then
                                        dInitDepAmt = (FAAD_AssetValue * (10)) / 100
                                        dr("InitDepAmt") = Convert.ToDecimal(dInitDepAmt).ToString("#,##0.00")
                                        iInitDepTot = iInitDepTot + dInitDepAmt
                                        iNextYrCarry = dInitDepAmt
                                    Else

                                    End If
                                    ddepLessThan180days = Convert.ToDecimal((FAAD_AssetValue * (rateOfPer / 2) / 100)).ToString("#,##0.00")
                                    LtotaldepaMOUNT = LtotaldepaMOUNT + ddepLessThan180days
                                    ddepLessThan180days = 0
                                ElseIf iNoOfDays > 180 Then
                                    Dim FAAD_AssetValue As Double = 0.0
                                    FAAD_AssetValue = dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    dMoreThan180days = dMoreThan180days + dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    If dtAddDetails.Rows(j)("FAAD_InitDep") = 1 Then
                                        dInitDepAmt = (FAAD_AssetValue * (20)) / 100
                                        dr("InitDepAmt") = Convert.ToDecimal(dInitDepAmt).ToString("#,##0.00")
                                        iInitDepTot = iInitDepTot + dInitDepAmt

                                    Else
                                        dr("InitDepAmt") = 0.00
                                    End If
                                    ddepMoreThan180days = (FAAD_AssetValue * (rateOfPer) / 100)
                                    MtotaldepAmount = MtotaldepAmount + ddepMoreThan180days
                                    ddepMoreThan180days = 0
                                End If
                            Next
                        End If

                        dr("BfrQtrAmount") = dLessThan180days
                        dr("BfrQtrDep") = LtotaldepaMOUNT
                        dr("AftQtrAmount") = dMoreThan180days
                        dr("AftQtrDep") = MtotaldepAmount
                        dr("WDVOpeningDepreciation") = dDepfortheperiod
                        dr("AdditionDuringtheYear") = Convert.ToDecimal(Math.Round(dLessThan180days + dMoreThan180days)).ToString("#,##0")
                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(dDepfortheperiod + LtotaldepaMOUNT + MtotaldepAmount + iInitDepTot)).ToString("#,##0")
                        dTotal = dLessThan180days + dMoreThan180days + dr("WDVOpeningValue")
                        dr("WDVClosingValue") = Convert.ToDecimal(Math.Round(dTotal - dr("Depfortheperiod"))).ToString("#,##0")
                        dr("NextYrCarry") = iNextYrCarry
                        dLessThan180days = 0 : LtotaldepaMOUNT = 0 : dMoreThan180days = 0 : MtotaldepAmount = 0 : dInitDepAmt = 0 : dPrevInitDepAmt = 0
                        dInitDepAmt = 0 : iNextYrCarry = 0 : iInitDepTot = 0
                        dt.Rows.Add(dr)
                    Next
                End If
            Else
                sSql = "select AM_ID,AM_Description,AM_WDVITAct,AM_ITRate from Acc_AssetMaster Where AM_CompID=" & iCompID & " and AM_LevelCode=2 and AM_CustId=" & iCustId & ""
                dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                If dt1.Rows.Count > 0 Then
                    For i = 0 To dt1.Rows.Count - 1
                        dr = dt.NewRow
                        dr("AssetClassID") = dt1.Rows(i)("AM_ID")
                        dr("ClassofAsset") = dt1.Rows(i)("AM_Description")
                        dWDVOpeningAmount = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADITAct_WrittenDownValue),0) From Acc_AssetDepITAct Where ADITAct_AssetClassID=" & dt1.Rows(i)("AM_ID") & " and  ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iPreviousYearid & " and ADITAct_CompID=" & iCompID & "")
                        If dWDVOpeningAmount = 0 Then
                            dOpeningValue = dt1.Rows(i)("AM_WDVITAct")
                        Else
                            dOpeningValue = dWDVOpeningAmount
                        End If
                        ddelAmount = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(AFAD_SalesPrice),0) From Acc_FixedAssetDeletion Where AFAD_AssetClass=" & dt1.Rows(i)("AM_ID") & " and AFAD_CompID=" & iCompID & " and AFAD_CustomerName=" & iCustId & " and AFAD_YearID=" & iYearId & "")
                        If ddelAmount <> 0 Then
                            dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dOpeningValue - ddelAmount)).ToString("#,##0")
                        Else
                            dr("WDVOpeningValue") = Convert.ToDecimal(Math.Round(dOpeningValue)).ToString("#,##0")
                        End If
                        dr("DelAmount") = Convert.ToDecimal(Math.Round(ddelAmount)).ToString("#,##0") ' NO NEEED
                        dr("RateofDep") = dt1.Rows(i)("AM_ITRate")
                        rateOfPer = dr("RateofDep")
                        dPrevInitDepAmt = objDBL.SQLGetDescription(sNameSpace, "Select isnull(sum(ADITAct_InitAmt),0) From Acc_AssetDepITAct Where ADITAct_AssetClassID=" & dt1.Rows(i)("AM_ID") & " and  ADITAct_CustId=" & iCustId & " and ADITAct_YearID=" & iPreviousYearid & " and ADITAct_CompID=" & iCompID & "")
                        dDepfortheperiod = (dr("WDVOpeningValue") * dr("RateofDep")) / 100  'NO NEED 
                        dWDVClosingAmount = dr("WDVOpeningValue") - dDepfortheperiod 'nO NEED
                        sSql = "select b.FAAD_ItemType,a.AFAM_CommissionDate,isnull(sum(b.FAAD_AssetValue),0) as FAAD_AssetValue,isnull(FAAD_InitDep,0) as FAAD_InitDep from Acc_FixedAssetMaster a"
                        sSql = sSql & " left join  Acc_FixedAssetAdditionDetails b on a.AFAM_ID=b.FAAD_ItemType where  b.FAAD_ItemType<>'' and FAAD_YearID=" & iYearId & " and FAAD_Delflag <> 'D'"
                        sSql = sSql & " and FAAD_CustId=" & iCustId & " and FAAD_Status<>'D' and FAAD_CompID=" & iCompID & " and FAAD_AssetType=" & dt1.Rows(i)("AM_ID") & " group by FAAD_ItemType,AFAM_CommissionDate,FAAD_InitDep"
                        dtAddDetails = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                        If dtAddDetails.Rows.Count > 0 Then
                            For j = 0 To dtAddDetails.Rows.Count - 1
                                ddate = dtAddDetails.Rows(j)("AFAM_CommissionDate")
                                iNoOfDays = DateDiff(DateInterval.Day, ddate, dEnddate)
                                If iNoOfDays <= 180 Then
                                    'rateOfPer = rateOfPer / 2
                                    Dim FAAD_AssetValue As Double = 0.0
                                    FAAD_AssetValue = dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    dLessThan180days = dLessThan180days + dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    If dtAddDetails.Rows(j)("FAAD_InitDep") = 1 Then
                                        dInitDepAmt = (FAAD_AssetValue * (10)) / 100
                                        iInitDepTot = iInitDepTot + dInitDepAmt
                                        iNextYrCarry = iNextYrCarry + dInitDepAmt
                                    Else

                                    End If
                                    ddepLessThan180days = (FAAD_AssetValue * (rateOfPer / 2) / 100)
                                    LtotaldepaMOUNT = LtotaldepaMOUNT + ddepLessThan180days
                                    ddepLessThan180days = 0
                                ElseIf iNoOfDays > 180 Then
                                    Dim FAAD_AssetValue As Double = 0.0
                                    FAAD_AssetValue = dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    dMoreThan180days = dMoreThan180days + dtAddDetails.Rows(j)("FAAD_AssetValue")
                                    If dtAddDetails.Rows(j)("FAAD_InitDep") = 1 Then
                                        dInitDepAmt = (FAAD_AssetValue * (20)) / 100
                                        iInitDepTot = iInitDepTot + dInitDepAmt
                                    Else

                                    End If
                                    ddepMoreThan180days = (FAAD_AssetValue * rateOfPer / 100)
                                    MtotaldepAmount = MtotaldepAmount + ddepMoreThan180days
                                    ddepMoreThan180days = 0
                                End If
                            Next
                        End If
                        dr("NextYrCarry") = iNextYrCarry
                        dr("BfrQtrAmount") = dLessThan180days
                        dr("BfrQtrDep") = LtotaldepaMOUNT
                        dr("AftQtrAmount") = dMoreThan180days
                        dr("AftQtrDep") = MtotaldepAmount
                        dr("InitDepAmt") = iInitDepTot
                        dr("PrevInitDepAmt") = dPrevInitDepAmt
                        dr("WDVOpeningDepreciation") = dDepfortheperiod
                        dr("AdditionDuringtheYear") = Convert.ToDecimal(Math.Round(dLessThan180days + dMoreThan180days)).ToString("#,##0")
                        dr("Depfortheperiod") = Convert.ToDecimal(Math.Round(dDepfortheperiod + LtotaldepaMOUNT + MtotaldepAmount + iInitDepTot + dPrevInitDepAmt)).ToString("#,##0")
                        dTotal = dLessThan180days + dMoreThan180days + dr("WDVOpeningValue")
                        dr("WDVClosingValue") = Convert.ToDecimal(Math.Round(dTotal - dr("Depfortheperiod"))).ToString("#,##0")
                        'dr("WDVClosingValue") = Convert.ToDecimal(Math.Round(dr("WDVOpeningValue") - dr("Depfortheperiod"))).ToString("#,##0")
                        dLessThan180days = 0 : LtotaldepaMOUNT = 0 : dMoreThan180days = 0 : MtotaldepAmount = 0 : dInitDepAmt = 0 : dPrevInitDepAmt = 0
                        iInitDepTot = 0 : iNextYrCarry = 0
                        dInitDepAmt = 0
                        dt.Rows.Add(dr)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadDepreciationCompWDV(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal NoOfDays As Integer, ByVal TNoOfDays As Integer, ByVal iDuration As Integer,
    '                                          sStartDt As Date, sEndDate As Date) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1, dt2 As New DataTable
    '    Dim ds1 As New DataSet
    '    Dim dr As DataRow, dr2 As DataRow
    '    Dim dDeprec As Double = 0
    '    Dim iNoOfDays As Integer = 0
    '    Dim dtAsstDetails As New DataTable
    '    Dim dOPBAmt As Double = 0.0
    '    Dim dOPBPreviousAmt As Double = 0.0

    '    Dim iFLCount As Integer = 0
    '    Dim dPreviousOPBAmt As Double = 0.0
    '    Dim dAddtnAmt As Double = 0.0
    '    Try
    '        dt.Columns.Add("AssetClassID")
    '        dt.Columns.Add("AssetID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("TrType")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("Item")
    '        dt.Columns.Add("OrignalCost")
    '        dt.Columns.Add("Rsdulvalue")
    '        dt.Columns.Add("SalvageValue")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("DepreciationRate")
    '        dt.Columns.Add("AddtnAmt")
    '        dt.Columns.Add("OPBForYR")
    '        dt.Columns.Add("DepreciationforFY")
    '        dt.Columns.Add("wrtnvalue")

    '        sSql = "Select distinct(AFAA_TrType),AFAA_ID,AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " order by AFAA_AssetType;"
    '        sSql = sSql & "select max(AFAA_id) as assetcount, sum(afaa_fyamount) as afaa_fyamount,AFAA_AssetType, 0 as tempcol"
    '        sSql = sSql & " from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " group by AFAA_AssetType order by AFAA_AssetType"
    '        ds1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
    '        dt1 = ds1.Tables(0) : dt2 = ds1.Tables(1)
    '        If dt1.Rows.Count > 0 Then
    '            Dim k As Integer = 0
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")

    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & "")
    '                '  dr("AssetCode") = objDBL.SQLGetDescription(sNameSpace, "Select AFAM_AssetCode From Acc_FixedAssetMaster Where AFAM_AssetType=" & dt1.Rows(i)("AFAA_AssetType") & " and AFAM_CompID=" & iCompID & "")
    '                '  dr("AssetAge") = objDBL.SQLGetDescription(sNameSpace, "Select AFAM_AssetAge From Acc_FixedAssetMaster Where AFAM_AssetType=" & dt1.Rows(i)("AFAA_AssetType") & " and AFAM_CompID=" & iCompID & "")
    '                '  dr("PurchaseDate") = objDBL.SQLGetDescription(sNameSpace, "Select AFAM_CommissionDate From Acc_FixedAssetMaster Where AFAM_AssetType=" & dt1.Rows(i)("AFAA_AssetType") & " and AFAM_CompID=" & iCompID & "")
    '                dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemCode").ToString(), iCompID)
    '                If dtAsstDetails.Rows.Count > 0 Then
    '                    dr("AssetCode") = dtAsstDetails.Rows(0)("AssetCode")
    '                    dr("AssetAge") = dtAsstDetails.Rows(0)("AssetAge")
    '                    dr("PurchaseDate") = dtAsstDetails.Rows(0)("PurchaseDate")
    '                Else
    '                    dr("AssetCode") = ""
    '                    dr("AssetAge") = ""
    '                    dr("PurchaseDate") = "01-01-1900"
    '                End If

    '                'Dim sStartDt1 As Date = Format(sStartDt, "dd/MM/yyyy")
    '                'Dim sEndDate1 As Date = Format(sEndDate, "dd/MM/yyyy")
    '                If dr("PurchaseDate") > sStartDt Then
    '                    If dr("PurchaseDate") <= sEndDate Then
    '                        iNoOfDays = DateDiff(DateInterval.Day, dr("PurchaseDate"), sEndDate)
    '                    ElseIf dr("PurchaseDate") > sEndDate Then
    '                        iNoOfDays = 0
    '                    End If
    '                Else
    '                    iNoOfDays = NoOfDays
    '                End If
    '                dr("NoOfDays") = iNoOfDays
    '                If dr("PurchaseDate") = "01-01-1900" Then
    '                    iNoOfDays = 0
    '                    dr("NoOfDays") = 0
    '                    dr("PurchaseDate") = ""
    '                End If
    '                dr("Item") = objDBL.SQLExecuteScalar(sNameSpace, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_ID= " & dt1.Rows(i)("AFAA_ItemType") & "")

    '                Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "")

    '                iFLCount = GetPreviousYrFreezeLedgerCount(sNameSpace, iCompID, iYearId)
    '                If iFLCount > 0 Then
    '                    dOPBAmt = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_ID"), iYearId)
    '                    dr("OPBForYR") = dOPBAmt
    '                Else
    '                    dOPBAmt = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0")
    '                End If

    '                If dt1.Rows(i)("AFAA_TrType") = 1 Then
    '                    dr("TrType") = "Opening Balance"
    '                    dr("AddtnAmt") = ""
    '                    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                        If ResidualValue <> 0 Then
    '                            dr("Rsdulvalue") = ResidualValue
    '                        Else
    '                            dr("Rsdulvalue") = 0
    '                        End If
    '                        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                    Else
    '                        dr("SalvageValue") = 0
    '                        dr("OrignalCost") = 0
    '                    End If

    '                ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                    dr("TrType") = "Addition"
    '                    dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"))
    '                    dr("OrignalCost") = dOPBAmt
    '                    If ResidualValue <> 0 Then
    '                        dr("Rsdulvalue") = ResidualValue
    '                    Else
    '                        dr("Rsdulvalue") = 0
    '                    End If

    '                    dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                    dAddtnAmt = dr("OrignalCost") - dr("SalvageValue")
    '                    dr("AddtnAmt") = dAddtnAmt
    '                Else
    '                    dr("TrType") = ""
    '                End If

    '                'If ResidualValue <> 0 Then
    '                '    dr("Rsdulvalue") = ResidualValue
    '                '    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                '        If dr("OrignalCost") <> 0 Then
    '                '            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                '        Else
    '                '            dr("SalvageValue") = 0
    '                '        End If
    '                '    Else
    '                '        dr("SalvageValue") = 0
    '                '        dr("OrignalCost") = 0
    '                '    End If
    '                'Else
    '                '    dr("Rsdulvalue") = 0
    '                '    dr("SalvageValue") = 0
    '                'End If

    '                If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                    If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                        If Val(dr("AssetAge")) <> 0 Then
    '                            Dim w As Integer = dr("AssetAge")
    '                            dr("DepreciationRate") = String.Format("{0:0.0000}", (1 - (dr("SalvageValue") / dr("OrignalCost")) ^ (1 / w)) * 100)
    '                        Else
    '                            dr("DepreciationRate") = 0
    '                        End If
    '                    Else
    '                        dr("DepreciationRate") = 0
    '                    End If
    '                Else
    '                    dr("DepreciationRate") = 0
    '                End If


    '                If IsDBNull(dOPBAmt) = False Then
    '                    'dr("OPBForYR") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0")
    '                    If iFLCount = 0 Then
    '                        dr("OPBForYR") = Convert.ToDecimal(Math.Round(dOPBAmt)).ToString("#,##0")
    '                    End If

    '                    If dr("OPBForYR") <> 0 Then
    '                        If dt1.Rows(i)("AFAA_TrType") = 1 Then
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dr("OPBForYR") * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                        Else
    '                            dr("OPBForYR") = 0
    '                            iNoOfDays = iNoOfDays + 1
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(((dAddtnAmt * dr("DepreciationRate")) / 100) * (iNoOfDays / TNoOfDays))).ToString("#,##0")
    '                        End If
    '                        dDeprec = dDeprec + dr("DepreciationforFY")
    '                        Dim sasdd As Integer = dt1.Rows.Count
    '                        Dim m As Integer = i + 1
    '                        If m = dt2.Rows(k)("assetcount") Or i = sasdd - 1 Then
    '                            dr2 = dt2.NewRow
    '                            dt2.Rows(k)("tempcol") = dDeprec
    '                            k = k + 1
    '                            dDeprec = 0
    '                        End If
    '                    Else
    '                        dr("OPBForYR") = 0
    '                        dr("DepreciationforFY") = 0
    '                    End If
    '                Else
    '                    dr("OPBForYR") = 0
    '                    dr("DepreciationforFY") = 0
    '                End If
    '                Dim damount As Double = 0.0
    '                If iNoOfDays <> 0 Then
    '                    If dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                        damount = Convert.ToDecimal(Math.Round(dAddtnAmt - dr("DepreciationforFY"))).ToString("#,##0")
    '                    Else
    '                        damount = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                    End If

    '                    If dr("SalvageValue") < damount Then
    '                        dr("wrtnvalue") = Convert.ToDecimal(Math.Round(damount)).ToString("#,##0")
    '                    Else
    '                        ''  ' For after salvage value crossed
    '                        Dim dAmount2 As Double = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("SalvageValue"))).ToString("#,##0")
    '                        If dAmount2 < 0 Then
    '                            dr("DepreciationforFY") = 0
    '                            dr("wrtnvalue") = dr("SalvageValue")
    '                        Else
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(dAmount2)).ToString("#,##0")
    '                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                        End If
    '                    End If
    '                Else
    '                    dr("DepreciationforFY") = 0
    '                    dr("OPBForYR") = 0
    '                    dr("wrtnvalue") = 0
    '                End If
    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        'If dt2.Rows.Count > 0 Then
    '        '    Dim sflag As Integer = 0
    '        '    For j = 0 To dt2.Rows.Count - 1
    '        '        dr = dt.NewRow
    '        '        dr("DepreciationRate") = <b>Total</b>
    '        '        dr("OPBForYR") = "<b>" & Convert.ToDecimal(dt2.Rows(j)("afaa_fyamount")).ToString("#,##0") & "</b>"
    '        '        dr("DepreciationforFY") = "<b>" & Convert.ToDecimal(dt2.Rows(j)("tempcol")).ToString("#,##0") & "</b>"
    '        '        Dim dWDV As Double = dt2.Rows(j)("afaa_fyamount") - dt2.Rows(j)("tempcol")
    '        '        dr("wrtnvalue") = "<b>" & Convert.ToDecimal(dWDV).ToString("#,##0") & "</b>"
    '        '        dt.Rows.InsertAt(dr, dt2.Rows(j)("assetcount") + sflag)
    '        '        sflag = sflag + 1
    '        '    Next
    '        'End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function LoadDepreciationCompSLM(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal NoOfDays As Integer, ByVal TNoOfDays As Integer, ByVal iDuration As Integer,
                                              sStartDt As Date, sEndDate As Date, ByVal iCustId As Integer, ByVal iMethod As Integer) As DataTable
        Dim dt, dt1, dt2 As New DataTable
        Dim sSql As String = ""

        Dim ds1 As New DataSet
        Dim dr As DataRow, dr2 As DataRow
        Dim dDeprec As Double = 0
        Dim iNoOfDays As Integer = 0
        Dim dtAsstDetails As New DataTable
        Dim dOPBAmt As Double = 0.0
        Dim dOPBPreviousAmt As Double = 0.0

        Dim iFLCount As Integer = 0
        Dim iFLDCount As Integer = 0
        Dim dPreviousOPBAmt As Double = 0.0
        Dim dAddtnAmt As Double = 0.0
        Dim ddate As Date
        Try
            dt.Columns.Add("AssetClassID")
            dt.Columns.Add("AssetID")
            dt.Columns.Add("Assettype")
            dt.Columns.Add("AssetCode")
            'dt.Columns.Add("AssetLocationCode")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("LocationID")
            dt.Columns.Add("DivisionID")
            dt.Columns.Add("DepartmentID")
            dt.Columns.Add("BayID")
            dt.Columns.Add("PurchaseDate")
            dt.Columns.Add("TrType")
            dt.Columns.Add("NoOfDays")
            dt.Columns.Add("Item")
            dt.Columns.Add("OrignalCost")
            dt.Columns.Add("Rsdulvalue")
            dt.Columns.Add("SalvageValue")
            dt.Columns.Add("AssetAge")
            dt.Columns.Add("DepreciationRate")
            dt.Columns.Add("AddtnAmt")
            dt.Columns.Add("OPBForYR")
            dt.Columns.Add("DepreciationforFY")
            dt.Columns.Add("wrtnvalue")

            sSql = "Select distinct(AFAA_TrType),AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType,AFAA_Location,AFAA_Division,AFAA_Department,AFAA_Bay from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " and AFAA_CustId=" & iCustId & " and AFAA_YearID <=" & iYearId & " and AFAA_Delflag <> 'D' order by AFAA_ItemType"
            dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow

                    dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
                    dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")

                    dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustId & "")
                    dr("Item") = objDBL.SQLExecuteScalar(sNameSpace, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_ID= " & dt1.Rows(i)("AFAA_ItemType") & " and AFAM_CustId=" & iCustId & "")

                    dr("LocationID") = dt1.Rows(i)("AFAA_Location")
                    dr("DivisionID") = dt1.Rows(i)("AFAA_Division")
                    dr("DepartmentID") = dt1.Rows(i)("AFAA_Department")
                    dr("BayID") = dt1.Rows(i)("AFAA_Bay")

                    dr("Location") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Location") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustId & "")
                    dr("Division") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Division") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustId & "")
                    dr("Department") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Department") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustId & "")
                    dr("Bay") = objDBL.SQLGetDescription(sNameSpace, "Select LS_Description From Acc_AssetLocationSetup Where LS_ID=" & dt1.Rows(i)("AFAA_Bay") & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustId & "")

                    dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemType").ToString(), iCompID, iCustId)
                    If dtAsstDetails.Rows.Count > 0 Then
                        dr("AssetCode") = dtAsstDetails.Rows(0)("AssetCode")
                        dr("AssetAge") = dtAsstDetails.Rows(0)("AssetAge")
                        ddate = dtAsstDetails.Rows(0)("PurchaseDate")
                    Else
                        dr("AssetCode") = ""
                        dr("AssetAge") = 0
                        dr("PurchaseDate") = "01-01-1900"
                    End If

                    If ddate > sStartDt Then
                        If ddate <= sEndDate Then
                            iNoOfDays = DateDiff(DateInterval.Day, ddate, sEndDate)
                        ElseIf ddate > sEndDate Then
                            iNoOfDays = 0
                        End If
                    Else
                        iNoOfDays = NoOfDays
                    End If

                    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(ddate, "D")
                    dr("NoOfDays") = iNoOfDays

                    If dr("PurchaseDate") = "01-01-1900" Then
                        iNoOfDays = 0
                        dr("NoOfDays") = 0
                        dr("PurchaseDate") = ""
                    End If

                    Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CustId=" & iCustId & " and AM_CompID=" & iCompID & "")

                    iFLCount = GetPreviousYrFreezeLedgerCount(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                    If iFLCount > 0 Then  'Old Asset

                        If dt1.Rows(i)("AFAA_TrType") = 1 Then

                            dr("TrType") = 1

                            dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                            dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")

                            dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If
                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim salvalue As Double = 0.0
                            salvalue = dr("SalvageValue")

                            dr("DepreciationRate") = ""

                            dr("DepreciationforFY") = dt2.Rows(0)("ADep_DepreciationforFY")

                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Dim wdv As Double = 0.0
                            wdv = dr("wrtnvalue")

                            If salvalue < wdv Then
                            Else
                                Dim dAmount As Double = 0.0
                                dAmount = dr("OPBForYR") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If

                        Else

                            dr("TrType") = 2

                            dt2 = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iYearId, iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"), iMethod)

                            dr("AddtnAmt") = ""
                            dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            dr("OrignalCost") = dOPBAmt

                            dr("OPBForYR") = dt2.Rows(0)("ADep_WrittenDownValue")
                            ' dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If
                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim salvalue As Double = 0.0
                            salvalue = dr("SalvageValue")

                            dr("DepreciationRate") = ""

                            Dim w As Integer = dr("AssetAge")

                            If dr("OPBForYR") <> 0 Then
                                Dim SLMamount As Double = dr("OrignalCost") - dr("SalvageValue")
                                Dim DTotalAmount As Double = SLMamount / w
                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(DTotalAmount)).ToString("#,##0")
                            Else  'Dk @16-03-23 for error when item amt zero
                                dr("DepreciationforFY") = 0
                            End If

                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Dim wdv As Double = 0.0
                            wdv = dr("wrtnvalue")

                            If salvalue < wdv Then
                            Else
                                Dim dAmount As Double = 0.0
                                dAmount = dr("OPBForYR") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If

                        End If

                    Else 'New Asset
                        Dim w As Integer = dr("AssetAge")
                        If dt1.Rows(i)("AFAA_TrType") = 1 Then

                            dr("TrType") = 1

                            dr("AddtnAmt") = ""

                            dr("OPBForYR") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0")

                            Dim salvalue As Double = 0.0
                            Dim dtdel As New DataTable
                            Dim ddelOPBAmt As Double = 0.0
                            Dim dDiffAmount As Double = 0.0

                            If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then

                                dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                                If dtdel.Rows.Count > 0 Then
                                    ddelOPBAmt = dtdel.Rows(0)("Amount")
                                    dDiffAmount = dt1.Rows(i)("AFAA_AssetAmount") - ddelOPBAmt
                                    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                                Else
                                    dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
                                End If

                                If ResidualValue <> 0 Then
                                    dr("Rsdulvalue") = ResidualValue
                                Else
                                    dr("Rsdulvalue") = 0
                                End If
                                dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                                salvalue = dr("SalvageValue")
                            Else
                                dr("SalvageValue") = 0
                                dr("OrignalCost") = 0
                            End If

                            If dr("OrignalCost") <> 0 Then
                                If dr("OPBForYR") <> 0 Then
                                    Dim SLMamount As Double = dr("OrignalCost") - dr("SalvageValue")
                                    Dim DTotalAmount As Double = SLMamount / w
                                    dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(DTotalAmount)).ToString("#,##0")
                                End If
                            Else
                                dr("OPBForYR") = 0
                                dr("DepreciationforFY") = 0
                            End If

                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Dim wdv As Double = 0.0
                            wdv = dr("wrtnvalue")

                            If salvalue < wdv Then
                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
                            Else
                                Dim dAmount As Double = 0.0
                                dAmount = dr("OPBForYR") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("OPBForYR") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If

                        ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
                            dr("TrType") = 2

                            dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            ' dr("OrignalCost") = dOPBAmt

                            Dim dtdel As New DataTable
                            Dim dDiffAmount As Double = 0.0
                            Dim ddelAddAmt As Double = 0.0

                            dtdel = GetDelAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"), iCustId, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                            If dtdel.Rows.Count > 0 Then
                                ddelAddAmt = dtdel.Rows(0)("Amount")
                                dDiffAmount = dOPBAmt - ddelAddAmt
                                dr("OrignalCost") = Convert.ToDecimal(Math.Round(dDiffAmount)).ToString("#,##0")
                            Else
                                dr("OrignalCost") = dOPBAmt
                            End If

                            dr("OPBForYR") = ""

                            If ResidualValue <> 0 Then
                                dr("Rsdulvalue") = ResidualValue
                            Else
                                dr("Rsdulvalue") = 0
                            End If

                            dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
                            Dim SALVALUE As Double = 0.0
                            SALVALUE = dr("SalvageValue")


                            If dr("OrignalCost") <> 0 Then
                                dr("AddtnAmt") = dr("OrignalCost") - dr("SalvageValue")
                            Else
                                dr("AddtnAmt") = 0
                            End If

                            If iNoOfDays > 0 Then
                                Dim SLMamount As Double = dr("AddtnAmt")
                                iNoOfDays = iNoOfDays + 1
                                Dim DTotalAmount As Double = (SLMamount / w) * (iNoOfDays / TNoOfDays)

                                dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(DTotalAmount)).ToString("#,##0")
                            Else
                                dr("DepreciationforFY") = 0
                            End If


                            dr("DepreciationRate") = ""

                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("AddtnAmt") - dr("DepreciationforFY"))).ToString("#,##0")
                            Dim WDV As Double = 0.0
                            WDV = dr("wrtnvalue")

                            If SALVALUE < WDV Then
                                dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("AddtnAmt") - dr("DepreciationforFY"))).ToString("#,##0")
                            Else
                                ''  ' For after salvage value crossed
                                Dim dAmount As Double = 0.0
                                dAmount = dr("AddtnAmt") - dr("SalvageValue")
                                If dAmount > 0 Then
                                    dr("DepreciationforFY") = dAmount
                                    dr("wrtnvalue") = dr("AddtnAmt") - dr("DepreciationforFY")
                                Else
                                    dr("DepreciationforFY") = 0
                                    dr("wrtnvalue") = dr("SalvageValue")
                                End If
                            End If
                        End If


                    End If
                    dt.Rows.Add(dr)
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadDepreciationComSLM(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal NoOfDays As Integer, ByVal TNoOfDays As Integer, ByVal iDuration As Integer,
    '                                          sStartDt As Date, sEndDate As Date) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1, dt2 As New DataTable
    '    Dim ds1 As New DataSet
    '    Dim dr As DataRow, dr2 As DataRow
    '    Dim dDeprec As Double = 0
    '    Dim iNoOfDays As Integer = 0
    '    Dim dtAsstDetails As New DataTable
    '    Dim dOPBAmt As Double = 0.0
    '    Dim dOPBPreviousAmt As Double = 0.0

    '    Dim iFLCount As Integer = 0
    '    Dim iFLDCount As Integer = 0
    '    Dim dPreviousOPBAmt As Double = 0.0
    '    Dim dAddtnAmt As Double = 0.0
    '    Try
    '        dt.Columns.Add("AssetClassID")
    '        dt.Columns.Add("AssetID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("TrType")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("Item")
    '        dt.Columns.Add("OrignalCost")
    '        dt.Columns.Add("Rsdulvalue")
    '        dt.Columns.Add("SalvageValue")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("DepreciationRate")
    '        dt.Columns.Add("AddtnAmt")
    '        dt.Columns.Add("OPBForYR")
    '        dt.Columns.Add("DepreciationforFY")
    '        dt.Columns.Add("wrtnvalue")

    '        sSql = "Select distinct(AFAA_TrType),AFAA_ID,AFAA_ItemDescription,AFAA_FYAmount,AFAA_AssetType,AFAA_AssetAmount,AFAA_ItemCode,AFAA_ItemType from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " order by AFAA_AssetType;"
    '        sSql = sSql & "select max(AFAA_id) as assetcount, sum(afaa_fyamount) as afaa_fyamount,AFAA_AssetType, 0 as tempcol"
    '        sSql = sSql & " from Acc_FixedAssetAdditionDel Where AFAA_CompID=" & iCompID & " group by AFAA_AssetType order by AFAA_AssetType"
    '        ds1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql)
    '        dt1 = ds1.Tables(0) : dt2 = ds1.Tables(1)
    '        If dt1.Rows.Count > 0 Then
    '            Dim k As Integer = 0

    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetClassID") = dt1.Rows(i)("AFAA_AssetType")
    '                dr("AssetID") = dt1.Rows(i)("AFAA_ItemType")

    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & " and AM_CompID=" & iCompID & "")

    '                dtAsstDetails = LoadAssetDetails(sNameSpace, dt1.Rows(i)("AFAA_ItemCode").ToString(), iCompID)
    '                If dtAsstDetails.Rows.Count > 0 Then
    '                    dr("AssetCode") = dtAsstDetails.Rows(0)("AssetCode")
    '                    dr("AssetAge") = dtAsstDetails.Rows(0)("AssetAge")
    '                    dr("PurchaseDate") = dtAsstDetails.Rows(0)("PurchaseDate")
    '                Else
    '                    dr("AssetCode") = ""
    '                    dr("AssetAge") = ""
    '                    dr("PurchaseDate") = "01-01-1900"
    '                End If

    '                If dr("PurchaseDate") > sStartDt Then
    '                    If dr("PurchaseDate") <= sEndDate Then
    '                        iNoOfDays = DateDiff(DateInterval.Day, dr("PurchaseDate"), sEndDate)
    '                    ElseIf dr("PurchaseDate") > sEndDate Then
    '                        iNoOfDays = 0
    '                    End If
    '                Else
    '                    iNoOfDays = NoOfDays
    '                End If
    '                dr("NoOfDays") = iNoOfDays

    '                If dr("PurchaseDate") = "01-01-1900" Then
    '                    iNoOfDays = 0
    '                    dr("NoOfDays") = 0
    '                    dr("PurchaseDate") = ""
    '                End If
    '                dr("Item") = objDBL.SQLExecuteScalar(sNameSpace, "select AFAM_ItemDescription from Acc_FixedAssetMaster where AFAM_ID= " & dt1.Rows(i)("AFAA_ItemType") & "")

    '                Dim ResidualValue As Double = objDBL.SQLGetDescription(sNameSpace, "Select AM_ResidualValue From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAA_AssetType") & "")

    '                iFLCount = GetPreviousYrFreezeLedgerCount(sNameSpace, iCompID, iYearId)
    '                If iFLCount > 0 Then
    '                    dr("OPBForYR") = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_ID"), iYearId)
    '                Else
    '                    dr("OPBForYR") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_FYAmount"))).ToString("#,##0")
    '                End If

    '                If dt1.Rows(i)("AFAA_TrType") = 1 Then
    '                    dr("TrType") = "Opening Balance"
    '                    dr("AddtnAmt") = ""
    '                    If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                        dr("OrignalCost") = Convert.ToDecimal(Math.Round(dt1.Rows(i)("AFAA_AssetAmount"))).ToString("#,##0")
    '                        If ResidualValue <> 0 Then
    '                            dr("Rsdulvalue") = ResidualValue
    '                        Else
    '                            dr("Rsdulvalue") = 0
    '                        End If
    '                        dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")
    '                    Else
    '                        dr("SalvageValue") = 0
    '                        dr("OrignalCost") = 0
    '                    End If

    '                ElseIf dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                    dr("TrType") = "Addition"
    '                    dOPBAmt = GetAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), dt1.Rows(i)("AFAA_ItemType"))
    '                    dr("OrignalCost") = dOPBAmt
    '                    If ResidualValue <> 0 Then
    '                        dr("Rsdulvalue") = ResidualValue
    '                    Else
    '                        dr("Rsdulvalue") = 0
    '                    End If

    '                    dr("SalvageValue") = Convert.ToDecimal(Math.Round(((ResidualValue * dr("OrignalCost")) / 100))).ToString("#,##0")

    '                    If iFLCount > 0 Then
    '                        dr("AddtnAmt") = GetFYAmount(sNameSpace, iCompID, dt1.Rows(i)("AFAA_ID"), iYearId)
    '                    Else
    '                        dAddtnAmt = dr("OrignalCost") - dr("SalvageValue")
    '                        dr("AddtnAmt") = dAddtnAmt

    '                    End If
    '                Else
    '                    dr("TrType") = ""
    '                End If
    '                Dim w As Integer
    '                If IsDBNull(dt1.Rows(i)("AFAA_AssetAmount")) = False Then
    '                    If Val(dr("OrignalCost")) <> 0 And Val(dr("SalvageValue")) <> 0 Then
    '                        If Val(dr("AssetAge")) <> 0 Then
    '                            w = dr("AssetAge")
    '                            dr("DepreciationRate") = 0
    '                        Else
    '                            dr("DepreciationRate") = 0
    '                        End If
    '                    Else
    '                        dr("DepreciationRate") = 0
    '                    End If
    '                Else
    '                    dr("DepreciationRate") = 0
    '                End If


    '                If dr("OPBForYR") <> 0 Then
    '                    If dt1.Rows(i)("AFAA_TrType") = 1 Then
    '                        Dim SLMamount As Double = dr("OPBForYR") - dr("SalvageValue")
    '                        Dim DTotalAmount As Double = SLMamount / w
    '                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(DTotalAmount)).ToString("#,##0")
    '                    Else
    '                        dr("OPBForYR") = 0
    '                        Dim SLMamount As Double = dr("AddtnAmt")
    '                        Dim DTotalAmount As Double = SLMamount / w
    '                        dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(DTotalAmount)).ToString("#,##0")
    '                    End If

    '                Else
    '                    dr("OPBForYR") = 0
    '                    dr("DepreciationforFY") = 0
    '                End If

    '                Dim damount As Double = 0.0
    '                If iNoOfDays <> 0 Then
    '                    If dt1.Rows(i)("AFAA_TrType") = 2 Then
    '                        damount = Convert.ToDecimal(Math.Round(dAddtnAmt - dr("DepreciationforFY"))).ToString("#,##0")
    '                    Else
    '                        damount = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                    End If

    '                    If dr("SalvageValue") < damount Then
    '                        dr("wrtnvalue") = Convert.ToDecimal(Math.Round(damount)).ToString("#,##0")
    '                    Else
    '                        ''  ' For after salvage value crossed
    '                        Dim dAmount2 As Double = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("SalvageValue"))).ToString("#,##0")
    '                        If dAmount2 < 0 Then
    '                            ' dr("DepreciationforFY") = 0
    '                            dr("wrtnvalue") = dr("SalvageValue")
    '                        Else
    '                            dr("DepreciationforFY") = Convert.ToDecimal(Math.Round(dAmount2)).ToString("#,##0")
    '                            dr("wrtnvalue") = Convert.ToDecimal(Math.Round(dr("OPBForYR") - dr("DepreciationforFY"))).ToString("#,##0")
    '                        End If
    '                    End If
    '                Else
    '                    dr("DepreciationforFY") = 0
    '                    dr("OPBForYR") = 0
    '                    dr("wrtnvalue") = 0
    '                End If
    '                dt.Rows.Add(dr)
    '            Next
    '        End If

    '        'If dt2.Rows.Count > 0 Then
    '        '    Dim sflag As Integer = 0
    '        '    For j = 0 To dt2.Rows.Count - 1
    '        '        dr = dt.NewRow
    '        '        dr("DepreciationRate") = <b>Total</b>
    '        '        dr("OPBForYR") = "<b>" & Convert.ToDecimal(dt2.Rows(j)("afaa_fyamount")).ToString("#,##0") & "</b>"
    '        '        dr("DepreciationforFY") = "<b>" & Convert.ToDecimal(dt2.Rows(j)("tempcol")).ToString("#,##0") & "</b>"
    '        '        Dim dWDV As Double = dt2.Rows(j)("afaa_fyamount") - dt2.Rows(j)("tempcol")
    '        '        dr("wrtnvalue") = "<b>" & Convert.ToDecimal(dWDV).ToString("#,##0") & "</b>"
    '        '        dt.Rows.InsertAt(dr, dt2.Rows(j)("assetcount") + sflag)
    '        '        sflag = sflag + 1
    '        '    Next
    '        'End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function GetFYAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer, ByVal iMethod As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Dim Amount As New DataTable
        Try
            If iYearId > 0 Then
                iPreviousYearID = iYearId - 1
            End If
            sSql = "select ADep_WrittenDownValue,ADep_DepreciationforFY from Acc_AssetDepreciation "
            sSql = sSql & " where  ADep_YearID=" & iPreviousYearID & " and ADep_CompID=" & iCompID & " and ADep_AssetID=" & iAssetClassId & " and ADep_Item=" & iAssetId & " and ADep_CustId=" & iCustID & " and ADep_Location=" & iLocationID & " and ADep_Division=" & iDivisionID & " and ADep_Department=" & iDepartmentID & " and ADep_Bay=" & iBayID & " and ADep_Method=" & iMethod & ""
            Amount = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return Amount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFYAmountITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Dim Amount As New DataTable
        Try
            If iYearId > 0 Then
                iPreviousYearID = iYearId - 1
            End If
            sSql = "select isnull(sum(ADITAct_WrittenDownValue),0) as ADITAct_WrittenDownValue from Acc_AssetDepITAct "
            sSql = sSql & " where ADITAct_YearID=" & iPreviousYearID & " and ADITAct_CompID=" & iCompID & " and ADITAct_AssetClassID=" & iAssetClassId & "  and ADITAct_CustId=" & iCustID & " and ADITAct_YearID=" & iPreviousYearID & ""
            Amount = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return Amount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPreviousFYAmountITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer

        Try
            If iYearId <> 0 Then
                iPreviousYearID = iYearId - 1
            End If

            sSql = "select isnull(sum(ADITAct_WrittenDownValue),0) as WDVOpeningValue from Acc_AssetDepITAct "
            sSql = sSql & " where ADITAct_YearID=" & iPreviousYearID & " and ADITAct_CompID=" & iCompID & " and ADITAct_AssetClassID=" & iAssetClassId & " and ADITAct_CustId=" & iCustID & ""
            GetPreviousFYAmountITAct = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetPreviousFYAmountITAct
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAdditionAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iYearId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer

        Try

            sSql = "select isnull(sum(FAAD_AssetValue),0) as AdditionAmount from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_YearID=" & iYearId & " and FAAD_AssetType=" & iAssetClassId & " and FAAD_CustId=" & iCustID & " and FAAD_CompID=" & iCompID & ""
            GetAdditionAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetAdditionAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPreviousYrFreezeLedgerCount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer, ByVal iMethod As Integer) As Integer
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Try
            If iYearID > 0 Then
                iPreviousYearID = iYearID - 1
            End If
            sSql = "Select count(ADep_ID) From Acc_AssetDepreciation Where ADep_CompID=" & iCompID & " And ADep_YearID=" & iPreviousYearID & " and ADep_AssetID=" & iAssetClassId & " and ADep_Item=" & iAsset & " and ADep_CustId=" & iCustID & " and ADep_Location=" & iLocationID & " and ADep_Division=" & iDivisionID & " and ADep_Department=" & iDepartmentID & " and ADep_Bay=" & iBayID & " and ADep_Method=" & iMethod & " "
            GetPreviousYrFreezeLedgerCount = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPreviousYrFreezeLedgerCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPreviousYrFLedgerCountITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As Integer
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Try
            If iYearID > 0 Then
                iPreviousYearID = iYearID - 1
            End If
            sSql = "Select count(ADITAct_ID) From Acc_AssetDepITAct Where ADITAct_CompID=" & iCompID & " And ADITAct_YearID=" & iPreviousYearID & " and ADITAct_AssetClassID=" & iAssetClassId & " and ADITAct_CustId=" & iCustID & ""
            GetPreviousYrFLedgerCountITAct = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return GetPreviousYrFLedgerCountITAct
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function FLedgerCountITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAssetClassId As Integer, ByVal iAsset As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As Integer
        Dim sSql As String = ""
        Dim iPreviousYearID As Integer
        Try
            If iYearID > 0 Then
                iPreviousYearID = iYearID - 1
            End If
            sSql = "Select count(ADITAct_ID) From Acc_AssetDepITAct Where ADITAct_CompID=" & iCompID & " And ADITAct_YearID=" & iPreviousYearID & " and ADITAct_AssetClassID=" & iAssetClassId & " and ADITAct_CustId=" & iCustID & ""
            FLedgerCountITAct = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return FLedgerCountITAct
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select isnull(sum(FAAD_AssetValue),0) from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_CompID=" & iCompID & " and FAAD_AssetType=" & iAssetClassId & " and FAAD_ItemType=" & iAssetId & " and FAAD_Status<>'D' and FAAD_CustId=" & iCustID & " and FAAD_Location=" & iLocationID & " and FAAD_Division=" & iDivisionID & " and FAAD_Department=" & iDepartmentID & " and FAAD_Bay=" & iBayID & ""
            GetAmount = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetAmount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAmountIT(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As String
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select isnull(sum(FAAD_AssetValue),0) from Acc_FixedAssetAdditionDetails "
            sSql = sSql & " where FAAD_CompID=" & iCompID & " and FAAD_AssetType=" & iAssetClassId & " and FAAD_ItemType=" & iAssetId & " and FAAD_Status<>'D' and FAAD_CustId=" & iCustID & ""
            GetAmountIT = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetAmountIT
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDelAmount(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select AFAD_AssetDeletionType,isnull(AFAD_DelDeprec,0) as Amount from Acc_FixedAssetDeletion "
            sSql = sSql & " where AFAD_CompID=" & iCompID & " and AFAD_AssetClass=" & iAssetClassId & " and AFAD_Asset=" & iAssetId & "  and AFAD_CustomerName=" & iCustID & " and AFAD_Location=" & iLocationID & " and AFAD_Division=" & iDivisionID & " and AFAD_Department=" & iDepartmentID & " and AFAD_Bay=" & iBayID & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetDelAmountITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer, ByVal iYearid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select isnull(sum(AFAD_SalesPrice),0) as Amount from Acc_FixedAssetDeletion "
            sSql = sSql & " where  AFAD_CompID=" & iCompID & " and AFAD_AssetClass=" & iAssetClassId & " and AFAD_CustomerName=" & iCustID & " and AFAD_YearID=" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function getDeactivateId(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAssetClassId As Integer, ByVal iAssetId As Integer, ByVal iCustID As Integer, ByVal iLocationID As Integer, ByVal iDivisionID As Integer, ByVal iDepartmentID As Integer, ByVal iBayID As Integer, ByVal iYearid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select AFAA_ID from Acc_FixedAssetAdditionDel where AFAA_Delflag ='D' "
            sSql = sSql & " where  AFAD_CompID=" & iCompID & " and AFAD_AssetClass=" & iAssetClassId & " and AFAD_CustomerName=" & iCustID & " and AFAD_YearID=" & iYearid & ""
            dt = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadItRateComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearid As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1 As New DataTable
    '    Dim dr As DataRow
    '    Try

    '        dt.Columns.Add("AssetMasterPKID")
    '        dt.Columns.Add("AssetTypeID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        dt.Columns.Add("AssetDescription")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("Quantity")
    '        dt.Columns.Add("ItRate")
    '        dt.Columns.Add("OrignalCoast")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("DepYear")
    '        dt.Columns.Add("YTDDep")
    '        dt.Columns.Add("wrtnvalue")
    '        dt.Columns.Add("Rsdulvalue")
    '        sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & " and AFAM_YearID='" & iYearid & "' order by AFAM_AssetType asc"
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
    '                dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")

    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and AM_CompID=" & iCompID & "")
    '                'objDBL.SQLGetDescription(sNameSpace, "Select Mas_desc From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_CompID=" & iCompID & " ")
    '                dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
    '                dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
    '                If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
    '                    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
    '                End If
    '                dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

    '                'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")

    '                dr("ItRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_Itrate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & "  And AM_CompID=" & iCompID & "")

    '                dr("OrignalCoast") = objDBL.SQLGetDescription(sNameSpace, "Select  AFAA_AssetAmount From Acc_FixedAssetAdditionDel Where AFAA_AssetType=" & dt1.Rows(i)("AFAM_AssetType") & "  And AFAA_CompID=" & iCompID & "")
    '                dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
    '                dr("NoOfDays") = ""
    '                dr("DepYear") = ""
    '                dr("YTDDep") = ""
    '                dr("wrtnvalue") = ""
    '                dr("Rsdulvalue") = ""
    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function CalculateDepreciationComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sErrortext As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1 As New DataTable
    '    Dim dr As DataRow
    '    Dim ToDate As Date
    '    Try
    '        dt.Columns.Add("AssetMasterPKID")
    '        dt.Columns.Add("AssetTypeID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        dt.Columns.Add("AssetDescription")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("Quantity")
    '        dt.Columns.Add("DepreciationRate")
    '        dt.Columns.Add("OrignalCoast")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("DepYear")
    '        dt.Columns.Add("YTDDep")
    '        dt.Columns.Add("wrtnvalue")
    '        dt.Columns.Add("Rsdulvalue")

    '        sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & "  order by AFAM_AssetType asc"
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
    '                dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")
    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster  Where  AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and AM_CompID=" & iCompID & "")
    '                dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
    '                dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
    '                If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
    '                    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
    '                End If
    '                dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

    '                'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")


    '                dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_Deprate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & " And AM_CompID=" & iCompID & "")

    '                dr("OrignalCoast") = objDBL.SQLGetDescription(sNameSpace, "Select  AFAA_AssetAmount From Acc_FixedAssetAdditionDel Where AFAA_AssetType=" & dt1.Rows(i)("AFAM_AssetType") & "  And AFAA_CompID=" & iCompID & "")
    '                dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
    '                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iYearID & "")


    '                Dim Fromdate As Date
    '                Fromdate = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")

    '                Dim diff As Integer = DateDiffComputation(sNameSpace, iCompID, Fromdate, ToDate)

    '                dr("NoOfDays") = diff
    '                dr("Rsdulvalue") = dr("OrignalCoast") * 5 / 100
    '                dr("DepYear") = String.Format("{0:0.00}", Convert.ToDecimal(((dr("DepreciationRate") * (dr("OrignalCoast") - dr("Rsdulvalue")) / 100) * dr("NoOfDays") / 365)))
    '                dr("YTDDep") = String.Format("{0:0.00}", Convert.ToDecimal(dr("DepYear")))
    '                dr("wrtnvalue") = String.Format("{0:0.00}", Convert.ToDecimal(dr("OrignalCoast") - dr("YTDDep")))

    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Function CalculateItRateComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sErrortext As String) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt, dt1 As New DataTable
    '    Dim dr As DataRow
    '    Dim ToDate As Date
    '    Try
    '        dt.Columns.Add("AssetMasterPKID")
    '        dt.Columns.Add("AssetTypeID")
    '        dt.Columns.Add("Assettype")
    '        dt.Columns.Add("AssetCode")
    '        dt.Columns.Add("AssetDescription")
    '        dt.Columns.Add("PurchaseDate")
    '        dt.Columns.Add("Quantity")
    '        dt.Columns.Add("ItRate")
    '        dt.Columns.Add("OrignalCoast")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("NoOfDays")
    '        dt.Columns.Add("DepYear")
    '        dt.Columns.Add("YTDDep")
    '        dt.Columns.Add("wrtnvalue")
    '        dt.Columns.Add("Rsdulvalue")

    '        sSql = "Select * From Acc_FixedAssetMaster Where AFAM_CompID=" & iCompID & "   order by AFAM_AssetType asc"
    '        dt1 = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        If dt1.Rows.Count > 0 Then
    '            For i = 0 To dt1.Rows.Count - 1
    '                dr = dt.NewRow

    '                dr("AssetMasterPKID") = dt1.Rows(i)("AFAM_ID")
    '                dr("AssetTypeID") = dt1.Rows(i)("AFAM_AssetType")
    '                dr("Assettype") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster  Where  AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & " and AM_CompID=" & iCompID & "")
    '                dr("AssetCode") = dt1.Rows(i)("AFAM_AssetCode")
    '                dr("AssetDescription") = dt1.Rows(i)("AFAM_Description")
    '                If IsDBNull(dt1.Rows(i)("AFAM_PurchaseDate")) = False Then
    '                    dr("PurchaseDate") = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")
    '                End If
    '                dr("Quantity") = dt1.Rows(i)("AFAM_Quantity")

    '                'dr("DepreciationRate") = objDBL.SQLGetDescription(sNameSpace, "Select Mas_DepRate From ACC_General_Master Where Mas_id=" & dt1.Rows(i)("AFAM_AssetType") & " And Mas_Master In (Select Mas_ID From Acc_Master_Type Where Mas_Type='Asset Type') and Mas_CompID=" & iCompID & " ")


    '                dr("ItRate") = objDBL.SQLGetDescription(sNameSpace, "Select  AM_ITRate From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AFAM_AssetType") & " And AM_CompID=" & iCompID & "")

    '                dr("OrignalCoast") = objDBL.SQLGetDescription(sNameSpace, "Select  AFAA_AssetAmount From Acc_FixedAssetAdditionDel Where AFAA_AssetType=" & dt1.Rows(i)("AFAM_AssetType") & "  And AFAA_CompID=" & iCompID & "")
    '                dr("AssetAge") = dt1.Rows(i)("AFAM_AssetAge")
    '                ToDate = objDBL.SQLGetDescription(sNameSpace, "Select YMS_TODATE From ACC_Year_Master Where YMS_ID=" & iYearID & "")


    '                Dim Fromdate As Date
    '                Fromdate = objFAS.FormatDtForRDBMS(dt1.Rows(i)("AFAM_PurchaseDate"), "D")

    '                Dim diff As Integer = DateDiffComputation(sNameSpace, iCompID, Fromdate, ToDate)

    '                dr("NoOfDays") = diff

    '                dr("DepYear") = String.Format("{0:0.00}", Convert.ToDecimal(((dr("ItRate") * dr("OrignalCoast") / 100) * dr("NoOfDays") / 365)))
    '                dr("YTDDep") = String.Format("{0:0.00}", Convert.ToDecimal(dr("DepYear")))
    '                dr("wrtnvalue") = String.Format("{0:0.00}", Convert.ToDecimal(dr("OrignalCoast") - dr("YTDDep")))
    '                dr("Rsdulvalue") = dr("OrignalCoast") * 5 / 100
    '                dt.Rows.Add(dr)
    '            Next
    '        End If
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function DateDiffComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal dFrmDt As Date, ByVal dTodt As Date) As Integer
        Dim sSql As String = ""
        Try
            sSql = "SELECT DATEDIFF(day, '" & objFAS.FormatDtForRDBMS(dFrmDt, "CT") & "','" & objFAS.FormatDtForRDBMS(dTodt, "CT") & "')"
            DateDiffComputation = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return DateDiffComputation
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As Integer, ByVal iCustID As Integer) As Integer
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select LS_ID from Acc_AssetLocationSetup where LS_ID=" & sLocation & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
            LoadLocation = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return LoadLocation
        Catch ex As Exception
        End Try
    End Function
    'Public Function LoadDivision(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As Integer, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_ID=" & sLocation & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadDivision = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadDivision
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Public Function LoadDepartment(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As Integer, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_ID='" & sLocation & "' and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadDepartment = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadDepartment
    '    Catch ex As Exception
    '    End Try
    'End Function
    'Public Function LoadBay(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sLocation As Integer, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try
    '        sSql = "select LS_ID from Acc_AssetLocationSetup where LS_ID=" & sLocation & " and LS_CompID=" & iCompID & " and LS_CustId=" & iCustID & ""
    '        LoadBay = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
    '        Return LoadBay
    '    Catch ex As Exception
    '    End Try
    'End Function
    Public Function SaveDepreciationComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objDepComp As ClsDepreciationComputation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_AssetID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_AssetID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Item", OleDb.OleDbType.VarChar, 250)
            ObjParam(iParamCount).Value = objDepComp.sADep_Item
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_RateofDep", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADep_RateofDep
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_OPBForYR ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADep_OPBForYR
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_DepreciationforFY", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADep_DepreciationforFY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_WrittenDownValue", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADep_WrittenDownValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ClosingDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADep_ClosingDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADep_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADep_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADep_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_DelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objDepComp.sADep_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objDepComp.sADep_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Location", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_Location
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Division", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_Division
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Department", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_Department
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Bay", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_Bay
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_TransType", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_TransType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Method", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADep_Method
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDepComp.ADep_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADep_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDepComp.ADep_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AsetDepreciation", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDepreciationITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objDepComp As ClsDepreciationComputation) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(26) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_AssetClassID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_AssetClassID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_RateofDep", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_RateofDep
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1


            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_OPBForYR ", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_OPBForYR
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_DepreciationforFY", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_DepreciationforFY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_WrittenDownValue", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_WrittenDownValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_BfrQtrAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_BfrQtrAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_BfrQtrDep", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_BfrQtrDep
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_AftQtrAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_AftQtrAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_AftQtrDep", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_AftQtrDep
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_DelAmount", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_DelAmount
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objDepComp.dADITAct_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_DelFlag", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDepComp.sADITAct_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_Status", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objDepComp.sADITAct_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objDepComp.iADITAct_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objDepComp.ADITAct_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objDepComp.ADITAct_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@ADITAct_InitAmt", OleDb.OleDbType.Double)
            ObjParam(iParamCount).Value = objDepComp.ADITAct_InitAmt
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetDepITAct", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function SaveIncomeTaxComputation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objItComp As ClsDepreciationComputation) As Array
    '    Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(25) {}
    '    Dim iParamCount As Integer
    '    Dim Arr(1) As String
    '    Try

    '        iParamCount = 0
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_ID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Asset_MasterID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_Asset_MasterID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AssetID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_AssetID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Description", OleDb.OleDbType.VarChar, 500)
    '        ObjParam(iParamCount).Value = objItComp.AIT_Description
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AssetAge", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_AssetAge
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Quantity", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_Quantity
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CommissionDate", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objItComp.AIT_CommissionDate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_PurchaseAmount", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_PurchaseAmount
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IncomeTax_rate", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_IncomeTax_rate
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_NoOfDays", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_NoOfDays
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IncomeTaxfor_theyear", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_IncomeTaxfor_theyear
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_YTD", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_YTD
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1
    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_WDV", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_WDV
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ResidualValue", OleDb.OleDbType.Decimal, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_ResidualValue
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CreatedBy", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_CreatedBy
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CreatedOn", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objItComp.AIT_CreatedOn
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_UpdatedBy", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_UpdatedBy
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_UpdatedOn", OleDb.OleDbType.Date)
    '        ObjParam(iParamCount).Value = objItComp.AIT_UpdatedOn
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_DelFlag", OleDb.OleDbType.Char, 1)
    '        ObjParam(iParamCount).Value = objItComp.AIT_DelFlag
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Status", OleDb.OleDbType.VarChar, 2)
    '        ObjParam(iParamCount).Value = objItComp.AIT_Status
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_YearID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_YearID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CompID", OleDb.OleDbType.Integer, 4)
    '        ObjParam(iParamCount).Value = objItComp.AIT_CompID
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Opeartion", OleDb.OleDbType.VarChar, 1)
    '        ObjParam(iParamCount).Value = objItComp.AIT_Opeartion
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IPAddress", OleDb.OleDbType.VarChar, 25)
    '        ObjParam(iParamCount).Value = objItComp.AIT_IPAddress
    '        ObjParam(iParamCount).Direction = ParameterDirection.Input
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        iParamCount += 1

    '        ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
    '        ObjParam(iParamCount).Direction = ParameterDirection.Output
    '        Arr(0) = "@iUpdateOrSave"
    '        Arr(1) = "@iOper"

    '        Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetIncomeTaxRate", 1, Arr, ObjParam)
    '        Return Arr

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function checkingmasterid(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal objDepComp As ClsDepreciationComputation) As String
        Dim AssetID As New Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            sSql = "Select ADep_Asset_MasterID From Acc_AsetDepreciation Where ADep_CompID=" & iCompID & " and  ADep_AssetID='" & AssetID & "'"
            checkingmasterid = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return checkingmasterid
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkAssetID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal objDepComp As ClsDepreciationComputation) As String
        Dim sSql As String = ""
        Dim AssetID As New Integer
        Dim retunID As String = ""
        Try

            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Fixed assets'))"
            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            sSql = "select ADep_AssetID from Acc_AsetDepreciation where ADep_AssetID=" & AssetID & " and ADep_CompID=" & iCompID & ""
            checkAssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)
            Return checkAssetID

            'sSql = "select ADep_AssetID from Acc_AsetDepreciation where ADep_AssetID=" & AssetID & " and ADep_CompID=" & iCompID & ""
            'retunID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            'sSql = "select AFAM_AssetCode from Acc_FixedAssetMaster where AFAM_AssetType=" & retunID & " and AFAM_CompID=" & iCompID & ""
            'checkAssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function FixedAssetSetting(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iddlMethod As Integer)
        Dim sSql As String = ""
        Dim iHeadID As Integer = 0
        Dim bcheck As Boolean

        Try
            bcheck = objDBL.SQLCheckForRecord(sNameSpace, "Select * From Application_Settings where AS_CompID='" & iCompID & "' ")
            If bcheck = True Then
                sSql = "" : sSql = "update Application_Settings set AS_DepMethod=" & iddlMethod & " where AS_CompID='" & iCompID & "'"
            Else
            End If
            'sSql = "" : sSql = "insert into Application_Settings (AS_DepMethod,AS_CompID) values( " & iddlMethod & ",'" & iCompID & "')"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function LoadFixedAsesetSetting(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustID As Integer) As Integer
    '    Dim sSql As String = ""
    '    Dim ds As New DataSet
    '    Try
    '        sSql = "" : sSql = "select CUST_DEPMETHOD from  SAD_CUSTOMER_MASTER where CUST_CompID='" & iCompID & "' and CUST_ID=" & iCustID & ""
    '        LoadFixedAsesetSetting = objDBL.SQLGetDescription(sNameSpace, sSql)
    '        Return LoadFixedAsesetSetting
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Public Function LoadAssetTotal(ByVal sNameSpace As String) As DataTable
        Dim ssql As String
        Dim dtTotal As DataTable, dt As New DataTable, dttotalOP As New DataTable
        Dim dr As DataRow, drow As DataRow
        Dim totalGLDebit As Double = 0, totalGLDebittemp As Double = 0
        Dim totalGLCredit As Double = 0, totalSGLCredittemp As Double = 0

        Try

            dt.Columns.Add("OPBForYR")
            dt.Columns.Add("DepreciationforFY")
            dt.Columns.Add("wrtnvalue")

            ssql = "select count(AFAA_FYAmount) as AFAA_FYAmount,sum(AFAA_DepreAmount) as AFAA_DepreAmount  from Acc_FixedAssetAdditionDel"
            ssql = ssql & " order by AFAA_AssetType"
            dtTotal = objDBL.SQLExecuteDataTable(sNameSpace, ssql)

            For i = 0 To dtTotal.Rows.Count - 1
                dr = dt.NewRow()

                If IsDBNull(dtTotal.Rows(i)("AFAA_FYAmount")) = False Then
                    dr("OPBForYR") = dtTotal.Rows(i)("AFAA_FYAmount")
                Else
                    dr("OPBForYR") = 0
                End If

                If IsDBNull(dtTotal.Rows(i)("AFAA_DepreAmount")) = False Then
                    dr("DepreciationforFY") = dtTotal.Rows(i)("AFAA_DepreAmount")
                Else
                    dr("DepreciationforFY") = 0
                End If

                If IsDBNull(dtTotal.Rows(i)("AFAA_DepreAmount")) = False Then
                    dr("wrtnvalue") = dtTotal.Rows(i)("AFAA_DepreAmount")
                Else
                    dr("wrtnvalue") = 0
                End If

                dt.Rows.Add(dr)
            Next

            dt.Rows.Add(dr)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveOpBal(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iMethod As Integer, ByVal iCUstId As Integer)
        Dim sSql As String = ""
        Try
            '    sSql = "Truncate Table ACC_Opening_Balance"
            sSql = "delete from  Acc_AssetDepreciation where ADep_YearID=" & iYearId & " and ADep_Method=" & iMethod & " and  ADep_CompID=" & iCompID & " and ADep_CustId=" & iCUstId & "  "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function RemoveITAct(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCustId As Integer)
        Dim sSql As String = ""
        Try
            '    sSql = "Truncate Table ACC_Opening_Balance"
            sSql = "delete from  Acc_AssetDepITAct where ADITAct_YearID=" & iYearId & " and ADITAct_CompID=" & iCompID & " and ADITAct_CustId =" & iCustId & " "
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function DbExport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal dt As DataTable, ByVal iCusid As Integer,
    '                         ByVal objDepComp As ClsDepreciationComputation, ByVal iUserID As Integer, ByVal sIPAddress As String) As DataTable
    '    Dim Arr As Array
    '    Dim dtr As New DataTable
    '    Dim OPBForYR As Double = 0.0
    '    Dim Addamount As Double = 0.0
    '    Try
    '        If dt.Rows.Count > 0 Then

    '            objDepComp.RemoveITAct(sNameSpace, iCompID, iYearId)

    '            For i = 0 To dt.Rows.Count - 1

    '                objDepComp.iADITAct_ID = 0
    '                objDepComp.iADITAct_AssetClassID = dt.Rows(i)("AssetClassID")
    '                '  objDepComp.iADITAct_AssetID = dt.Rows(i)("AssetID")
    '                objDepComp.dADITAct_RateofDep = dt.Rows(i)("RateofDep")

    '                If IsDBNull(dt.Rows(i)("WDVOpeningValue")) = False Then
    '                    OPBForYR = dt.Rows(i)("WDVOpeningValue")
    '                Else
    '                    OPBForYR = 0
    '                End If

    '                If Val(OPBForYR) = 0 Then
    '                    If IsDBNull(dt.Rows(i)("AdditionDuringtheYear")) = False Then
    '                        Addamount = dt.Rows(i)("AdditionDuringtheYear")
    '                        objDepComp.dADITAct_OPBForYR = Addamount
    '                    Else
    '                        objDepComp.dADITAct_OPBForYR = 0
    '                    End If

    '                Else
    '                    If OPBForYR <> 0 Then
    '                        objDepComp.dADITAct_OPBForYR = OPBForYR
    '                    End If
    '                End If
    '                'If IsDBNull(dt.Rows(i)("WDVOpeningValue")) = False Then
    '                '    objDepComp.dADITAct_OPBForYR = dt.Rows(i)("WDVOpeningValue")
    '                'Else
    '                '    objDepComp.dADITAct_OPBForYR = 0
    '                'End If

    '                objDepComp.dADITAct_OriginalCost = 0
    '                objDepComp.dADITAct_DepreciationforFY = dt.Rows(i)("Depfortheperiod")
    '                objDepComp.dADITAct_WrittenDownValue = dt.Rows(i)("WDVClosingValue")
    '                objDepComp.dADITAct_ClosingDate = Date.Today
    '                objDepComp.iADITAct_CreatedBy = iUserID
    '                objDepComp.dADITAct_CreatedOn = DateTime.Today
    '                objDepComp.iADITAct_UpdatedBy = iUserID
    '                objDepComp.dADITAct_UpdatedOn = DateTime.Today
    '                objDepComp.iADITAct_ApprovedBy = iUserID
    '                objDepComp.dADITAct_ApprovedOn = DateTime.Today
    '                objDepComp.sADITAct_DelFlag = "X"
    '                objDepComp.sADITAct_Status = "W"
    '                objDepComp.iADITAct_YearID = iYearId
    '                objDepComp.iADITAct_CompID = iCompID
    '                objDepComp.iADITAct_CustId = iCusid
    '                objDepComp.iADITAct_Location = 0
    '                objDepComp.iADITAct_Division = 0
    '                objDepComp.iADITAct_Department = 0
    '                objDepComp.iADITAct_Bay = 0
    '                objDepComp.iADITAct_TransType = dt.Rows(i)("TrType")
    '                objDepComp.iADITAct_Opeartion = "C"
    '                objDepComp.sADITAct_IPAddress = sIPAddress
    '                objDepComp.sADITAct_Days = dt.Rows(i)("Days")
    '                Arr = SaveDepreciationITAct(sNameSpace, iCompID, iYearId, objDepComp)
    '            Next
    '        End If

    '        dtr = Fetchintdata(sNameSpace, iCompID, iYearId, iCusid)

    '        Return dtr

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function Fetchintdata(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal iCusid As Integer) As DataTable
        Dim dt, dt1 As New DataTable
        Dim sSql As String = ""
        Dim dr As DataRow, drow As DataRow
        Dim dOPValue As Double = 0.0
        Dim dAddAmount As Double = 0.0
        Dim ipreviousYearid As Integer = 0
        Dim GetPreviousFYAmountIT As Double = 0.0
        Dim iFLCount As Integer = 0
        Dim dAdditionAmount As Double = 0.0

        Try

            dt.Columns.Add("AssetClassID")
            dt.Columns.Add("AssetID")
            dt.Columns.Add("TrType")
            dt.Columns.Add("Days")
            dt.Columns.Add("ClassofAsset")
            dt.Columns.Add("OriginalCost")
            dt.Columns.Add("RateofDep")
            dt.Columns.Add("WDVOpeningValue")
            dt.Columns.Add("AdditionDuringtheYear")
            'dt.Columns.Add("TotalWDV")
            dt.Columns.Add("Depfortheperiod")
            dt.Columns.Add("WDVClosingValue")

            If iYearId <> 0 Then
                ipreviousYearid = iYearId - 1
            End If

            'sSql = "select ADITAct_AssetClassID as AssetClassID,ADITAct_RateofDep as RateofDep,"
            'sSql = sSql & " sum(ADITAct_OPBForYR) as WDVOpeningValue,"
            'sSql = sSql & " sum(ADITAct_DepreciationforFY) as Depfortheperiod,"
            'sSql = sSql & " sum(ADITAct_WrittenDownValue) as WDVClosingValue"
            'sSql = sSql & " from Acc_AssetDepITAct where ADITAct_YearID=" & ipreviousYearid & " and ADITAct_CustId=" & iCusid & ""
            'sSql = sSql & "  group by ADITAct_AssetClassID,ADITAct_RateofDep"
            'dt1 = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            'If dt1.Rows.Count > 0 Then

            '    For i = 0 To dt1.Rows.Count - 1
            '        dr = dt.NewRow()

            '        If IsDBNull(dt1.Rows(i)("AssetClassID")) = False Then
            '            dr("AssetClassID") = dt1.Rows(i)("AssetClassID")

            '        Else
            '            dr("AssetClassID") = 0
            '        End If

            '        dr("AssetID") = 0

            '        'If IsDBNull(dt.Rows(i)("TrType")) = False Then
            '        '    dr("TrType") = dt.Rows(i)("TrType")
            '        'Else
            '        dr("TrType") = 0
            '        dr("Days") = ""
            '        'End If

            '        If IsDBNull(dt1.Rows(i)("AssetClassID")) = False Then
            '            'dr("ClassofAsset") = dt1.Rows(i)("AssetClassID")
            '            dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AssetClassID") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCusid & "")
            '        Else
            '            dr("ClassofAsset") = 0
            '        End If

            '        'If IsDBNull(dt1.Rows(i)("OriginalCost")) = False Then
            '        '    dr("OriginalCost") = dt1.Rows(i)("OriginalCost")
            '        'Else
            '        dr("OriginalCost") = 0
            '        'End If

            '        If IsDBNull(dt1.Rows(i)("RateofDep")) = False Then
            '            dr("RateofDep") = dt1.Rows(i)("RateofDep")
            '        Else
            '            dr("RateofDep") = 0
            '        End If

            '        If IsDBNull(dt1.Rows(i)("WDVOpeningValue")) = False Then
            '            dr("WDVOpeningValue") = dt1.Rows(i)("WDVOpeningValue")
            '            dOPValue = dt1.Rows(i)("WDVOpeningValue")
            '        Else
            '            dOPValue = 0
            '            dr("WDVOpeningValue") = 0
            '        End If


            '        dr("AdditionDuringtheYear") = GetPreviousFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), 0, iYearId, iCusid, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
            '        dAddAmount = dt1.Rows(i)("AdditionDuringtheYear")


            '        dr("TotalWDV") = dOPValue + dAddAmount

            '        If IsDBNull(dt1.Rows(i)("Depfortheperiod")) = False Then
            '            dr("Depfortheperiod") = dt1.Rows(i)("Depfortheperiod")
            '        Else
            '            dr("Depfortheperiod") = 0
            '        End If

            '        If IsDBNull(dt1.Rows(i)("WDVClosingValue")) = False Then
            '            dr("WDVClosingValue") = dt1.Rows(i)("WDVClosingValue")
            '        Else
            '            dr("WDVClosingValue") = 0
            '        End If

            '        dt.Rows.Add(dr)
            '    Next
            'End If


            'If dt1.Rows.Count = 0 Then
            sSql = "select ADITAct_AssetClassID as AssetClassID,ADITAct_RateofDep as RateofDep,"
            sSql = sSql & " sum(case when ADITAct_TransType =1 then ADITAct_OPBForYR else 0 end ) as WDVOpeningValue,"
            sSql = sSql & " sum(case when ADITAct_TransType =2 then ADITAct_OPBForYR else 0 end ) as AdditionDuringtheYear,"
            sSql = sSql & " sum(ADITAct_DepreciationforFY) as Depfortheperiod,"
            sSql = sSql & " sum(ADITAct_WrittenDownValue) as WDVClosingValue"
            sSql = sSql & " from Acc_AssetDepITAct where ADITAct_YearID=" & iYearId & " and ADITAct_CustId=" & iCusid & " and ADITAct_CompID=" & iCompID & " "
            sSql = sSql & "  group by ADITAct_AssetClassID,ADITAct_RateofDep"
            dt1 = objDBL.SQLExecuteDataTable(sNameSpace, sSql)

            If dt1.Rows.Count > 0 Then

                For i = 0 To dt1.Rows.Count - 1
                    dr = dt.NewRow()

                    If IsDBNull(dt1.Rows(i)("AssetClassID")) = False Then
                        dr("AssetClassID") = dt1.Rows(i)("AssetClassID")

                    Else
                        dr("AssetClassID") = 0
                    End If

                    dr("AssetID") = 0

                    'If IsDBNull(dt.Rows(i)("TrType")) = False Then
                    '    dr("TrType") = dt.Rows(i)("TrType")
                    'Else
                    dr("TrType") = 0
                    dr("Days") = ""
                    'End If

                    If IsDBNull(dt1.Rows(i)("AssetClassID")) = False Then
                        'dr("ClassofAsset") = dt1.Rows(i)("AssetClassID")
                        dr("ClassofAsset") = objDBL.SQLGetDescription(sNameSpace, "Select AM_Description From Acc_AssetMaster Where AM_ID=" & dt1.Rows(i)("AssetClassID") & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCusid & "")
                    Else
                        dr("ClassofAsset") = 0
                    End If

                    'If IsDBNull(dt1.Rows(i)("OriginalCost")) = False Then
                    '    dr("OriginalCost") = dt1.Rows(i)("OriginalCost")
                    'Else
                    dr("OriginalCost") = 0
                    'End If

                    If IsDBNull(dt1.Rows(i)("RateofDep")) = False Then
                        dr("RateofDep") = dt1.Rows(i)("RateofDep")
                    Else
                        dr("RateofDep") = 0
                    End If

                    '  GetPreviousFYAmountIT = GetPreviousFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AFAA_AssetType"), 0, iYearId, iCusid, dt1.Rows(i)("AFAA_Location"), dt1.Rows(i)("AFAA_Division"), dt1.Rows(i)("AFAA_Department"), dt1.Rows(i)("AFAA_Bay"))
                    iFLCount = FLedgerCountITAct(sNameSpace, iCompID, iYearId, dt1.Rows(i)("AssetClassID"), 0, iCusid, 0, 0, 0, 0)

                    If iFLCount > 0 Then
                        GetPreviousFYAmountIT = GetPreviousFYAmountITAct(sNameSpace, iCompID, dt1.Rows(i)("AssetClassID"), 0, iYearId, iCusid, 0, 0, 0, 0)
                        If GetPreviousFYAmountIT <> 0 Then
                            dr("WDVOpeningValue") = GetPreviousFYAmountIT
                        Else
                            If IsDBNull(dt1.Rows(i)("WDVOpeningValue")) = False Then
                                dr("WDVOpeningValue") = dt1.Rows(i)("WDVOpeningValue")
                                dOPValue = dt1.Rows(i)("WDVOpeningValue")
                            Else
                                dOPValue = 0
                                dr("WDVOpeningValue") = 0
                            End If
                        End If
                    Else
                        If IsDBNull(dt1.Rows(i)("WDVOpeningValue")) = False Then
                            dr("WDVOpeningValue") = dt1.Rows(i)("WDVOpeningValue")
                            dOPValue = dt1.Rows(i)("WDVOpeningValue")
                        Else
                            dOPValue = 0
                            dr("WDVOpeningValue") = 0
                        End If
                    End If


                    dAdditionAmount = GetAdditionAmount(sNameSpace, iCompID, dt1.Rows(i)("AssetClassID"), 0, iYearId, iCusid, 0, 0, 0, 0)
                    If dAdditionAmount <> 0 Then
                        dr("AdditionDuringtheYear") = dAdditionAmount
                    Else
                        dr("AdditionDuringtheYear") = ""
                    End If

                    ' dr("TotalWDV") = dOPValue + dAddAmount

                    If IsDBNull(dt1.Rows(i)("Depfortheperiod")) = False Then
                        dr("Depfortheperiod") = dt1.Rows(i)("Depfortheperiod")
                    Else
                        dr("Depfortheperiod") = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("WDVClosingValue")) = False Then
                        dr("WDVClosingValue") = dt1.Rows(i)("WDVClosingValue")
                    Else
                        dr("WDVClosingValue") = 0
                    End If

                    dt.Rows.Add(dr)
                Next
            End If

            'End If

            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetCountOfOpenBalAddition(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustID As Integer, ByVal sStatus As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select distinct ADep_TransType, Case when ADep_TransType = 1 then 'Opening Balance' else 'Addition' end as TransactionType, "
            sSql = sSql + "COUNT(ADep_TransType) as Counts from Acc_AssetDepreciation where ADep_CompID=" & iACID & " and "
            sSql = sSql + "ADep_CustId=" & iCustID & " and ADep_YearID=" & iyearId & " group by ADep_TransType"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function getCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iyearId As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select COUNT(*) As Count FROM Acc_FixedAssetAdditionDel WHERE AFAA_CustId = " & iCustID & " And AFAA_YearID = " & iyearId & " AND AFAA_Delflag = 'A' And AFAA_TrType IN (1, 2) GROUP BY AFAA_TrType"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
