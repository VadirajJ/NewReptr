Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class ClsAssetMaster
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objGen As New clsGRACeGeneral

    Private iAM_ID As Integer
    Private sAM_Description As String
    Private sAM_Code As String
    Private iAM_LevelCode As Integer
    Private iAM_ParentID As Integer
    Private dAM_WDVITAct As Double
    Private dAM_ITRate As Double
    Private iAM_ResidualValue As Integer
    Private iAM_CreatedBy As Integer
    Private dAM_CreatedOn As DateTime
    Private iAM_UpdatedBy As Integer
    Private dAM_UpdatedOn As DateTime
    Private sAM_DelFlag As String
    Private sAM_Status As String
    Private iAM_YearID As Integer
    Private iAM_CompID As Integer
    Private sAM_Opeartion As String
    Private sAM_IPAddress As String
    Private iAM_ApprovedBy As Integer
    Private dAM_ApprovedOn As DateTime

    Private iAM_CustId As Integer
    Public Property AM_CustId() As Integer
        Get
            Return (iAM_CustId)
        End Get
        Set(ByVal Value As Integer)
            iAM_CustId = Value
        End Set
    End Property
    Public Property AM_ID() As Integer
        Get
            Return (iAM_ID)
        End Get
        Set(ByVal Value As Integer)
            iAM_ID = Value
        End Set
    End Property
    Public Property AM_Description() As String
        Get
            Return (sAM_Description)
        End Get
        Set(ByVal Value As String)
            sAM_Description = Value
        End Set
    End Property
    Public Property AM_Code() As String
        Get
            Return (sAM_Code)
        End Get
        Set(ByVal Value As String)
            sAM_Code = Value
        End Set
    End Property
    Public Property AM_LevelCode() As Integer
        Get
            Return (iAM_LevelCode)
        End Get
        Set(ByVal Value As Integer)
            iAM_LevelCode = Value
        End Set
    End Property
    Public Property AM_ParentID() As Integer
        Get
            Return (iAM_ParentID)
        End Get
        Set(ByVal Value As Integer)
            iAM_ParentID = Value
        End Set
    End Property

    Public Property AM_CreatedBy() As Integer
        Get
            Return (iAM_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAM_CreatedBy = Value
        End Set
    End Property

    Public Property AM_CreatedOn() As DateTime
        Get
            Return (dAM_CreatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAM_CreatedOn = Value
        End Set
    End Property
    Public Property AM_UpdatedOn() As DateTime
        Get
            Return (dAM_UpdatedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAM_UpdatedOn = Value
        End Set
    End Property
    Public Property AM_UpdatedBy() As Integer
        Get
            Return (iAM_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iAM_UpdatedBy = Value
        End Set
    End Property
    Public Property AM_DelFlag() As String
        Get
            Return (sAM_DelFlag)
        End Get
        Set(ByVal Value As String)
            sAM_DelFlag = Value
        End Set
    End Property
    Public Property AM_Status() As String
        Get
            Return (sAM_Status)
        End Get
        Set(ByVal Value As String)
            sAM_Status = Value
        End Set
    End Property
    Public Property AM_YearID() As Integer
        Get
            Return (iAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            iAM_YearID = Value
        End Set
    End Property
    Public Property AM_CompID() As Integer
        Get
            Return (iAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            iAM_CompID = Value
        End Set
    End Property
    Public Property AM_WDVITAct() As Double
        Get
            Return (dAM_WDVITAct)
        End Get
        Set(ByVal Value As Double)
            dAM_WDVITAct = Value
        End Set
    End Property
    Public Property AM_Opeartion() As String
        Get
            Return (sAM_Opeartion)
        End Get
        Set(ByVal Value As String)
            sAM_Opeartion = Value
        End Set
    End Property
    Public Property AM_IPAddress() As String
        Get
            Return (sAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            sAM_IPAddress = Value
        End Set
    End Property
    Public Property AM_ITRate() As String
        Get
            Return (dAM_ITRate)
        End Get
        Set(ByVal Value As String)
            dAM_ITRate = Value
        End Set
    End Property
    Public Property AM_ResidualValue() As String
        Get
            Return (iAM_ResidualValue)
        End Get
        Set(ByVal Value As String)
            iAM_ResidualValue = Value
        End Set
    End Property
    Public Property AM_ApprovedBy() As Integer
        Get
            Return (iAM_ApprovedBy)
        End Get
        Set(ByVal Value As Integer)
            iAM_ApprovedBy = Value
        End Set
    End Property
    Public Property AM_ApprovedOn() As DateTime
        Get
            Return (dAM_ApprovedOn)
        End Get
        Set(ByVal Value As DateTime)
            dAM_ApprovedOn = Value
        End Set
    End Property
    Public Sub UpdateDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal Deprate As Integer, ByVal ITRate As Integer, ByVal ResidualValue As Integer, ByVal Item As Integer, ByVal iCustId As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_AssetMaster set AM_WDVITAct=" & Deprate & ",AM_ITRate=" & ITRate & ",AM_ResidualValue=" & ResidualValue & " where AM_ID=" & Item & " and AM_CompID=" & iACID & " and AM_CustId=" & iCustId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadHeading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=0 and AM_CompID=" & iCompID & " and AM_CustId=" & iCustid & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadCustomer(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try

            sSql = "Select CUST_ID,CUST_NAME From SAD_CUSTOMER_MASTER Where CUST_DELFLG<>'D' and CUST_CompID=" & iCompID & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub AssMasDeactivate(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer, ByVal sFlag As String, ByVal sIPAddress As String, ByVal iYeraID As Integer, ByVal iCustID As Integer)
        Dim sSql As String = ""
        Try
            sSql = "Update Acc_FixedAssetAdditionDel set "
            If sFlag = "W" Then
                sSql = sSql & "AFAA_Delflag='W',AFAA_ApprovedBy=" & iUserID & ",AFAA_ApprovedOn=Getdate(),"
            ElseIf sFlag = "D" Then
                sSql = sSql & "AFAA_Delflag='D',AFAA_ApprovedBy=" & iUserID & ",AFAA_ApprovedOn=Getdate(),"
            ElseIf sFlag = "A" Then
                sSql = sSql & "AFAA_Delflag='A',AFAA_ApprovedBy=" & iUserID & ",AFAA_ApprovedOn=Getdate(),"
            End If
            sSql = sSql & " AFAA_IPAddress='" & sIPAddress & "' where AFAA_ID=" & iPKID & " and AFAA_CompID=" & iACID & " and AFAA_YearID = " & iYeraID & " and AFAA_CustID = " & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)


            sSql = "Update Acc_FixedAssetAdditionDetails set "
            If sFlag = "W" Then
                sSql = sSql & "FAAD_Delflag='W',"
            ElseIf sFlag = "D" Then
                sSql = sSql & "FAAD_Delflag='D',"
            ElseIf sFlag = "A" Then
                sSql = sSql & "FAAD_Delflag='A',"
            End If
            sSql = sSql & " FAAD_IPAddress='" & sIPAddress & "' where FAAD_MasID=" & iPKID & " and FAAD_CompID=" & iACID & " and FAAD_YearID = " & iYeraID & " and FAAD_CustID = " & iCustID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadLocation(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select LS_ID,LS_Description From Acc_AssetLocationSetup Where LS_LevelCode=0 and LS_CompID=" & iCompID & " and LS_CustId=" & iCustid & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSUbHeading(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iparentid As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iparentid = 0 Then
                sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=1 and AM_CompID=" & iCompID & " and AM_CustId=" & iCustid & ""
            Else
                sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=1 and AM_ParentID=" & iparentid & " and AM_CompID=" & iCompID & " and AM_CustId=" & iCustid & ""
            End If

            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadItems(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iparentid As Integer, ByVal iCustid As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            If iparentid = 0 Then
                sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_LevelCode=2 and AM_CompID=" & iCompID & " and AM_CustId=" & iCustid & ""
            Else
                sSql = "Select AM_ID,AM_Description From Acc_AssetMaster Where AM_ParentID=" & iparentid & " and AM_LevelCode=2 and AM_CompID=" & iCompID & " and AM_CustId=" & iCustid & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function LoadAssets(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Try

    '        sSql = "Select GL_Desc,GL_ID From Chart_Of_Accounts Where GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Non Current Assets'))"
    '        dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function SaveAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iyearid As Integer, ByVal objAsst As ClsAssetMaster) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(22) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Description", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsst.AM_Description
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Code", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objAsst.AM_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_LevelCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_LevelCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ParentID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_ParentID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_WDVITAct", OleDb.OleDbType.Decimal, 4)
            ObjParam(iParamCount).Value = objAsst.AM_WDVITAct
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ITRate", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsst.AM_ITRate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ResidualValue", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAsst.iAM_ResidualValue
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CreatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsst.AM_CreatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_UpdatedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsst.AM_UpdatedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_DelFlag", OleDb.OleDbType.Char, 1)
            ObjParam(iParamCount).Value = objAsst.AM_DelFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Status", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAsst.AM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_CustId", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.AM_CustId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ApprovedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAsst.iAM_ApprovedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_ApprovedOn", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objAsst.dAM_ApprovedOn
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_Opeartion", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = objAsst.AM_Opeartion
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AM_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAsst.AM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_AssetMaster", 1, Arr, ObjParam)
            Return Arr

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function AssetRetrieve(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iAM_AsstType As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            'sSql = "select * from Acc_AssetMaster Where AM_AssetID=" & iAM_AsstType & " And AM_CompID=" & iCompID & " And AM_YearID=" & iYearID & " " ' commented On 14-03-2022
            sSql = "select * from Acc_AssetMaster Where AM_AssetID=" & iAM_AsstType & " And AM_CompID=" & iCompID & " "
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Changeddetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iPKId As String, ByVal iCustId As Integer, ByVal iYearId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Try
            sSql = "select usr_FullName,c.AM_Description as Old,d.AM_Description as New,AFAM_TrAssetAge,AFAM_AssetAge,AFAM_YearID from Acc_FixedAssetMaster a"
            sSql = sSql & " left join Sad_UserDetails b on  b.usr_Id = a.AFAM_TrUpdatedBy"
            sSql = sSql & " left join Acc_AssetMaster c on  c.AM_ID = a.AFAM_TRAssetType"
            sSql = sSql & " left join Acc_AssetMaster d on  d.AM_ID = a.AFAM_AssetType"
            sSql = sSql & " where AFAM_ID = " & iPKId & " and AFAM_CustId=" & iCustId & " and AFAM_YearID = " & iYearId & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iDescID As Integer, ByVal iCustid As Integer) As DataTable
        Dim dt As New DataTable
        Dim sSql As String = ""
        Dim dbcheck As New Boolean
        Try

            sSql = "Select AM_WDVITAct,AM_ITRate,AM_ResidualValue from Acc_AssetMaster where AM_ID =" & iDescID & " and AM_CompID =" & iCompID & " and AM_CustId =" & iCustid & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckDepreciationRate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal sAssettype As String, ByVal dDeprate As Double, ByVal objAsst As ClsAssetMaster) As String
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dcheck As New Boolean
        Dim AssetID As New Integer
        Dim iID As String = ""
        Dim iMax As Integer = 0
        Try

            sSql = "Select GL_ID From Chart_Of_Accounts Where GL_DESC='" & sAssettype & "' And GL_Parent In (Select GL_ID From Chart_Of_Accounts Where GL_Parent In (Select gl_ID From Chart_Of_Accounts Where GL_Desc='Non Current Assets'))"

            AssetID = objDBL.SQLExecuteScalarInt(sNameSpace, sSql)

            sSql = "select * from Acc_AssetMaster where AM_Deprate=" & dDeprate & " and AM_AssetID=" & AssetID & ""
            dcheck = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If dcheck = True Then

            Else
                iMax = objDBL.SQLExecuteScalar(sNameSpace, "Select isnull(max(AM_ID)+1,1) from Acc_AssetMaster")
                sSql = "insert into Acc_AssetMaster(AM_ID,AM_AssetID,AM_CreatedBy,AM_CreatedOn,AM_DelFlag ,"
                sSql = sSql & " AM_Status,AM_YearID,AM_CompID,AM_WDVITAct,AM_Opeartion,AM_IPAddress) values (" & iMax & "," & AssetID & ","
                sSql = sSql & " " & objAsst.AM_CreatedBy & ",getdate(),'X','W'," & iYearID & "," & iCompID & "," & dDeprate & ",'C','" & objAsst.AM_IPAddress & "')"
                objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iMasId As Integer, ByVal sStatus As String, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String = "", ssqlgl As String = ""
        Dim iCsmSgl As Integer = 0
        Try
            sSql = "Update Acc_AssetMaster Set "
            If sStatus = "W" Then
                sSql = sSql & " AM_DelFlag='A',AM_Status='A',AM_ApprovedBy= " & iUserID & ",AM_ApprovedOn=GetDate()"
            End If
            sSql = sSql & " Where AM_ID = " & iMasId & " and AM_CompID=" & iCompID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iid As Integer, ByVal iCustid As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = " Select AM_DelFlag from Acc_AssetMaster Where AM_ID = " & iid & " And AM_CompID=" & iCompID & " And AM_CustId=" & iCustid & ""
            GetStatus = objDBL.SQLGetDescription(sNameSpace, sSql)
            Return GetStatus
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAsset(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iAsset As String, ByVal iCustID As Integer, ByVal iparentid As Integer)
        Dim sSql As String
        Dim checkitemcode As Boolean
        Try
            sSql = " Select AM_Description from Acc_AssetMaster Where AM_Description = '" & iAsset & "' And AM_CompID=" & iCompID & " and AM_CustId=" & iCustID & " and AM_ParentID=" & iparentid & ""
            checkitemcode = objDBL.SQLCheckForRecord(sNameSpace, sSql)
            If checkitemcode = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
