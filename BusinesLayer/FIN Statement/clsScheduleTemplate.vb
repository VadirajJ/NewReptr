Imports System.Configuration
Imports System.Data.SqlClient

Public Class clsScheduleTemplate
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions

    'Heading
    Private ASH_ID As Integer
    Private ASH_Name As String
    Private ASH_DELFLG As String
    Private ASH_CRBY As Integer
    Private ASH_STATUS As String
    Private ASH_UPDATEDBY As Integer
    Private ASH_IPAddress As String
    Private ASH_CompId As Integer
    Private ASH_YEARId As Integer
    Private Sch_Orgtype As Integer
    Private Sch_scheduletype As Integer
    Private ASH_Notes As Integer

    'Sub Heading
    Private ASSH_ID As Integer
    Private ASSH_Name As String
    Private ASSH_HeadingID As Integer
    Private ASSH_DELFLG As String
    Private ASSH_CRBY As Integer
    Private ASSH_STATUS As String
    Private ASSH_UPDATEDBY As Integer
    Private ASSH_IPAddress As String
    Private ASSH_CompId As Integer
    Private ASSH_YEARId As Integer
    Private Assh_Orgtype As Integer
    Private Assh_scheduletype As Integer

    'Items
    Private ASI_ID As Integer
    Private ASI_Name As String
    Private ASI_HeadingID As Integer
    Private ASI_SubHeadingID As Integer
    Private ASI_DELFLG As String
    Private ASI_CRBY As Integer
    Private ASI_STATUS As String
    Private ASI_UPDATEDBY As Integer
    Private ASI_IPAddress As String
    Private ASI_CompId As Integer
    Private ASI_YEARId As Integer
    Private Asi_Orgtype As Integer
    Private Asi_scheduletype As Integer

    'Sub Items
    Private ASSI_ID As Integer
    Private ASSI_Name As String
    Private ASSI_HeadingID As Integer
    Private ASSI_SubHeadingID As Integer
    Private ASSI_ItemsID As Integer
    Private ASSI_DELFLG As String
    Private ASSI_CRBY As Integer
    Private ASSI_STATUS As String
    Private ASSI_UPDATEDBY As Integer
    Private ASSI_IPAddress As String
    Private ASSI_CompId As Integer
    Private ASSI_YEARId As Integer
    Private Assi_Orgtype As Integer
    Private Assi_scheduletype As Integer

    'Scheduletemplate
    Private AST_ID As Integer
    Private AST_Name As String
    Private AST_HeadingID As Integer
    Private AST_SubHeadingID As Integer
    Private AST_ItemsID As Integer
    Private AST_SubItemsID As Integer
    Private AST_AccHeadId As Integer
    Private AST_DELFLG As String
    Private AST_CRBY As Integer
    Private AST_STATUS As String
    Private AST_UPDATEDBY As Integer
    Private AST_IPAddress As String
    Private AST_CompId As Integer
    Private AST_YEARId As Integer
    Private AST_Schedule_type As Integer
    Private AST_Companytype As Integer
    Private AST_Company_limit As Integer
    Private ASSH_Notes As Integer

    Private AGA_ID As Integer
    Private AGA_Description As String
    Private AGA_GLID As Integer
    Private AGA_GLDESC As String
    Private AGA_GrpLevel As Integer
    Private AGA_scheduletype As Integer
    Private AGA_Orgtype As Integer
    Private AGA_Compid As Integer
    Private AGA_Status As String
    Private AGA_Createdby As Integer
    Private AGA_IPaddress As String


    Public Property iAGA_ID() As Integer
        Get
            Return (AGA_ID)
        End Get
        Set(ByVal Value As Integer)
            AGA_ID = Value
        End Set
    End Property

    Public Property sAGA_Description() As String
        Get
            Return (AGA_Description)
        End Get
        Set(ByVal Value As String)
            AGA_Description = Value
        End Set
    End Property



    Public Property iAGA_GLID() As Integer
        Get
            Return (AGA_GLID)
        End Get
        Set(ByVal Value As Integer)
            AGA_GLID = Value
        End Set
    End Property

    Public Property sAGA_GLDESC() As String
        Get
            Return (AGA_GLDESC)
        End Get
        Set(ByVal Value As String)
            AGA_GLDESC = Value
        End Set
    End Property

    Public Property iAGA_GrpLevel() As Integer
        Get
            Return (AGA_GrpLevel)
        End Get
        Set(ByVal Value As Integer)
            AGA_GrpLevel = Value
        End Set
    End Property
    Public Property iAGA_scheduletype() As Integer
        Get
            Return (AGA_scheduletype)
        End Get
        Set(ByVal Value As Integer)
            AGA_scheduletype = Value
        End Set
    End Property

    Public Property iAGA_Orgtype() As Integer
        Get
            Return (AGA_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            AGA_Orgtype = Value
        End Set
    End Property
    Public Property iAGA_Compid() As Integer
        Get
            Return (AGA_Compid)
        End Get
        Set(ByVal Value As Integer)
            AGA_Compid = Value
        End Set
    End Property
    Public Property sAGA_Status() As String
        Get
            Return (AGA_Status)
        End Get
        Set(ByVal Value As String)
            AGA_Status = Value
        End Set
    End Property

    Public Property iAGA_Createdby() As Integer
        Get
            Return (AGA_Createdby)
        End Get
        Set(ByVal Value As Integer)
            AGA_Createdby = Value
        End Set
    End Property
    Public Property sAGA_IPaddress() As String
        Get
            Return (AGA_IPaddress)
        End Get
        Set(ByVal Value As String)
            AGA_IPaddress = Value
        End Set
    End Property

    'Heading
    Public Property iASH_ID() As Integer
        Get
            Return (ASH_ID)
        End Get
        Set(ByVal Value As Integer)
            ASH_ID = Value
        End Set
    End Property

    Public Property sASH_Name() As String
        Get
            Return (ASH_Name)
        End Get
        Set(ByVal Value As String)
            ASH_Name = Value
        End Set
    End Property

    Public Property sASH_DELFLG() As String
        Get
            Return (ASH_DELFLG)
        End Get
        Set(ByVal Value As String)
            ASH_DELFLG = Value
        End Set
    End Property

    Public Property iASH_CRBY() As Integer
        Get
            Return (ASH_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ASH_CRBY = Value
        End Set
    End Property

    Public Property sASH_STATUS() As String
        Get
            Return (ASH_STATUS)
        End Get
        Set(ByVal Value As String)
            ASH_STATUS = Value
        End Set
    End Property

    Public Property iASH_UPDATEDBY() As Integer
        Get
            Return (ASH_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ASH_UPDATEDBY = Value
        End Set
    End Property
    Public Property sASH_IPAddress() As String
        Get
            Return (ASH_IPAddress)
        End Get
        Set(ByVal Value As String)
            ASH_IPAddress = Value
        End Set
    End Property

    Public Property iASH_CompId() As Integer
        Get
            Return (ASH_CompId)
        End Get
        Set(ByVal Value As Integer)
            ASH_CompId = Value
        End Set
    End Property
    Public Property iSch_scheduletype() As Integer
        Get
            Return (Sch_scheduletype)
        End Get
        Set(ByVal Value As Integer)
            Sch_scheduletype = Value
        End Set
    End Property

    Public Property iSch_Orgtype() As Integer
        Get
            Return (Sch_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            Sch_Orgtype = Value
        End Set
    End Property

    Public Property iASH_YEARId() As Integer
        Get
            Return (ASH_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ASH_YEARId = Value
        End Set
    End Property
    Public Property iASH_Notes() As Integer
        Get
            Return (ASH_Notes)
        End Get
        Set(ByVal Value As Integer)
            ASH_Notes = Value
        End Set
    End Property

    'Sub Heading
    Public Property iASSH_ID() As Integer
        Get
            Return (ASSH_ID)
        End Get
        Set(ByVal Value As Integer)
            ASSH_ID = Value
        End Set
    End Property

    Public Property sASSH_Name() As String
        Get
            Return (ASSH_Name)
        End Get
        Set(ByVal Value As String)
            ASSH_Name = Value
        End Set
    End Property

    Public Property iASSH_HeadingID() As Integer
        Get
            Return (ASSH_HeadingID)
        End Get
        Set(ByVal Value As Integer)
            ASSH_HeadingID = Value
        End Set
    End Property
    Public Property sASSH_DELFLG() As String
        Get
            Return (ASSH_DELFLG)
        End Get
        Set(ByVal Value As String)
            ASSH_DELFLG = Value
        End Set
    End Property

    Public Property iASSH_CRBY() As Integer
        Get
            Return (ASSH_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ASSH_CRBY = Value
        End Set
    End Property

    Public Property sASSH_STATUS() As String
        Get
            Return (ASSH_STATUS)
        End Get
        Set(ByVal Value As String)
            ASSH_STATUS = Value
        End Set
    End Property

    Public Property iASSH_UPDATEDBY() As Integer
        Get
            Return (ASSH_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ASSH_UPDATEDBY = Value
        End Set
    End Property
    Public Property sASSH_IPAddress() As String
        Get
            Return (ASSH_IPAddress)
        End Get
        Set(ByVal Value As String)
            ASSH_IPAddress = Value
        End Set
    End Property

    Public Property iASSH_CompId() As Integer
        Get
            Return (ASSH_CompId)
        End Get
        Set(ByVal Value As Integer)
            ASSH_CompId = Value
        End Set
    End Property
    Public Property iASSH_YEARId() As Integer
        Get
            Return (ASSH_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ASSH_YEARId = Value
        End Set
    End Property
    Public Property iAssh_Orgtype() As Integer
        Get
            Return (Assh_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            Assh_Orgtype = Value
        End Set
    End Property
    Public Property iAssh_scheduletype() As Integer
        Get
            Return (Assh_scheduletype)
        End Get
        Set(ByVal Value As Integer)
            Assh_scheduletype = Value
        End Set
    End Property

    'Items
    Public Property iASI_ID() As Integer
        Get
            Return (ASI_ID)
        End Get
        Set(ByVal Value As Integer)
            ASI_ID = Value
        End Set
    End Property

    Public Property sASI_Name() As String
        Get
            Return (ASI_Name)
        End Get
        Set(ByVal Value As String)
            ASI_Name = Value
        End Set
    End Property

    Public Property iASI_HeadingID() As Integer
        Get
            Return (ASI_HeadingID)
        End Get
        Set(ByVal Value As Integer)
            ASI_HeadingID = Value
        End Set
    End Property

    Public Property iASI_SubHeadingID() As Integer
        Get
            Return (ASI_SubHeadingID)
        End Get
        Set(ByVal Value As Integer)
            ASI_SubHeadingID = Value
        End Set
    End Property
    Public Property sASI_DELFLG() As String
        Get
            Return (ASI_DELFLG)
        End Get
        Set(ByVal Value As String)
            ASI_DELFLG = Value
        End Set
    End Property

    Public Property iASI_CRBY() As Integer
        Get
            Return (ASI_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ASI_CRBY = Value
        End Set
    End Property

    Public Property sASI_STATUS() As String
        Get
            Return (ASI_STATUS)
        End Get
        Set(ByVal Value As String)
            ASI_STATUS = Value
        End Set
    End Property

    Public Property iASI_UPDATEDBY() As Integer
        Get
            Return (ASI_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ASI_UPDATEDBY = Value
        End Set
    End Property
    Public Property sASI_IPAddress() As String
        Get
            Return (ASI_IPAddress)
        End Get
        Set(ByVal Value As String)
            ASI_IPAddress = Value
        End Set
    End Property

    Public Property iASI_CompId() As Integer
        Get
            Return (ASI_CompId)
        End Get
        Set(ByVal Value As Integer)
            ASI_CompId = Value
        End Set
    End Property
    Public Property iASI_YEARId() As Integer
        Get
            Return (ASI_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ASI_YEARId = Value
        End Set
    End Property
    Public Property iAsi_scheduletype() As Integer
        Get
            Return (Asi_scheduletype)
        End Get
        Set(ByVal Value As Integer)
            Asi_scheduletype = Value
        End Set
    End Property
    Public Property iAsi_Orgtype() As Integer
        Get
            Return (Asi_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            Asi_Orgtype = Value
        End Set
    End Property

    'Sub Items

    Public Property iASSI_ID() As Integer
        Get
            Return (ASSI_ID)
        End Get
        Set(ByVal Value As Integer)
            ASSI_ID = Value
        End Set
    End Property

    Public Property sASSI_Name() As String
        Get
            Return (ASSI_Name)
        End Get
        Set(ByVal Value As String)
            ASSI_Name = Value
        End Set
    End Property

    Public Property iASSI_HeadingID() As Integer
        Get
            Return (ASSI_HeadingID)
        End Get
        Set(ByVal Value As Integer)
            ASSI_HeadingID = Value
        End Set
    End Property

    Public Property iASSI_SubHeadingID() As Integer
        Get
            Return (ASSI_SubHeadingID)
        End Get
        Set(ByVal Value As Integer)
            ASSI_SubHeadingID = Value
        End Set
    End Property
    Public Property iASSI_ItemsID() As Integer
        Get
            Return (ASSI_ItemsID)
        End Get
        Set(ByVal Value As Integer)
            ASSI_ItemsID = Value
        End Set
    End Property
    Public Property sASSI_DELFLG() As String
        Get
            Return (ASSI_DELFLG)
        End Get
        Set(ByVal Value As String)
            ASSI_DELFLG = Value
        End Set
    End Property

    Public Property iASSI_CRBY() As Integer
        Get
            Return (ASSI_CRBY)
        End Get
        Set(ByVal Value As Integer)
            ASSI_CRBY = Value
        End Set
    End Property

    Public Property sASSI_STATUS() As String
        Get
            Return (ASSI_STATUS)
        End Get
        Set(ByVal Value As String)
            ASSI_STATUS = Value
        End Set
    End Property

    Public Property iASSI_UPDATEDBY() As Integer
        Get
            Return (ASSI_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            ASSI_UPDATEDBY = Value
        End Set
    End Property
    Public Property sASSI_IPAddress() As String
        Get
            Return (ASSI_IPAddress)
        End Get
        Set(ByVal Value As String)
            ASSI_IPAddress = Value
        End Set
    End Property

    Public Property iASSI_CompId() As Integer
        Get
            Return (ASSI_CompId)
        End Get
        Set(ByVal Value As Integer)
            ASSI_CompId = Value
        End Set
    End Property
    Public Property iASSI_YEARId() As Integer
        Get
            Return (ASSI_YEARId)
        End Get
        Set(ByVal Value As Integer)
            ASSI_YEARId = Value
        End Set
    End Property
    Public Property iAssi_Orgtype() As Integer
        Get
            Return (Assi_Orgtype)
        End Get
        Set(ByVal Value As Integer)
            Assi_Orgtype = Value
        End Set
    End Property
    Public Property iAssi_scheduletype() As Integer
        Get
            Return (Assi_scheduletype)
        End Get
        Set(ByVal Value As Integer)
            Assi_scheduletype = Value
        End Set
    End Property

    'scheduleyemplate getset
    Public Property iAST_ID() As Integer
        Get
            Return (AST_ID)
        End Get
        Set(ByVal Value As Integer)
            AST_ID = Value
        End Set
    End Property

    Public Property sAST_Name() As String
        Get
            Return (AST_Name)
        End Get
        Set(ByVal Value As String)
            AST_Name = Value
        End Set
    End Property

    Public Property iAST_HeadingID() As Integer
        Get
            Return (AST_HeadingID)
        End Get
        Set(ByVal Value As Integer)
            AST_HeadingID = Value
        End Set
    End Property

    Public Property iAST_SubHeadingID() As Integer
        Get
            Return (AST_SubHeadingID)
        End Get
        Set(ByVal Value As Integer)
            AST_SubHeadingID = Value
        End Set
    End Property
    Public Property iAST_ItemsID() As Integer
        Get
            Return (AST_ItemsID)
        End Get
        Set(ByVal Value As Integer)
            AST_ItemsID = Value
        End Set
    End Property
    Public Property iAST_subItemsID() As Integer
        Get
            Return (AST_SubItemsID)
        End Get
        Set(ByVal Value As Integer)
            AST_SubItemsID = Value
        End Set
    End Property
    Public Property iAST_AccHeadId() As Integer
        Get
            Return (AST_AccHeadId)
        End Get
        Set(ByVal Value As Integer)
            AST_AccHeadId = Value
        End Set
    End Property
    Public Property sAST_DELFLG() As String
        Get
            Return (AST_DELFLG)
        End Get
        Set(ByVal Value As String)
            AST_DELFLG = Value
        End Set
    End Property

    Public Property iAST_CRBY() As Integer
        Get
            Return (AST_CRBY)
        End Get
        Set(ByVal Value As Integer)
            AST_CRBY = Value
        End Set
    End Property

    Public Property sAST_STATUS() As String
        Get
            Return (AST_STATUS)
        End Get
        Set(ByVal Value As String)
            AST_STATUS = Value
        End Set
    End Property

    Public Property iAST_UPDATEDBY() As Integer
        Get
            Return (AST_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            AST_UPDATEDBY = Value
        End Set
    End Property
    Public Property sAST_IPAddress() As String
        Get
            Return (AST_IPAddress)
        End Get
        Set(ByVal Value As String)
            AST_IPAddress = Value
        End Set
    End Property

    Public Property iAST_CompId() As Integer
        Get
            Return (AST_CompId)
        End Get
        Set(ByVal Value As Integer)
            AST_CompId = Value
        End Set
    End Property
    Public Property iAST_YEARId() As Integer
        Get
            Return (AST_YEARId)
        End Get
        Set(ByVal Value As Integer)
            AST_YEARId = Value
        End Set
    End Property

    Public Property iAST_Companytype() As Integer
        Get
            Return (AST_Companytype)
        End Get
        Set(ByVal Value As Integer)
            AST_Companytype = Value
        End Set
    End Property
    Public Property iAST_Company_limit() As Integer
        Get
            Return (AST_Company_limit)
        End Get
        Set(ByVal Value As Integer)
            AST_Company_limit = Value
        End Set
    End Property
    Public Property iASSH_Notes() As Integer
        Get
            Return (ASSH_Notes)
        End Get
        Set(ByVal Value As Integer)
            ASSH_Notes = Value
        End Set
    End Property

    Public Property iAST_Schedule_type() As Integer
        Get
            Return (AST_Schedule_type)
        End Get
        Set(ByVal Value As Integer)
            AST_Schedule_type = Value
        End Set
    End Property
    'save
    Public Function SaveScheduleHeadingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_Name", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sASH_Name
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_DELFLG", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASH_DELFLG
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_CRBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_CRBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_STATUS", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASH_STATUS
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_UPDATEDBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_UPDATEDBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = objHeading.sASH_IPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_CompId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_CompId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_YEARId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_YEARId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Ash_scheduletype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_scheduletype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Ash_Orgtype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_Orgtype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASH_Notes", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASH_Notes
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_ScheduleHeading", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    'sub section
    Public Function SaveScheduleSubHeadingDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_Name", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sASSH_Name
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_HeadingID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_HeadingID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_DELFLG", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASSH_DELFLG
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_CRBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_CRBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_STATUS", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASSH_STATUS
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_UPDATEDBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_UPDATEDBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = objHeading.sASSH_IPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_CompId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_CompId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_YEARId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_YEARId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSH_Notes", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSH_Notes
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1


                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Assh_scheduletype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_scheduletype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Assh_Orgtype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_Orgtype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_ScheduleSubHeading", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveScheduleItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(11) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASI_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_Name", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sASI_Name
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_DELFLG", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASI_DELFLG
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_CRBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASI_CRBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_STATUS", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASI_STATUS
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = objHeading.sASI_IPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_CompId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASI_CompId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASI_YEARId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = 22
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Asi_scheduletype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_scheduletype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@Asi_Orgtype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_Orgtype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_ScheduleItems", 1, Arr, ObjParam)


            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function SaveScheduleSubItemDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(15) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_Name", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sASSI_Name
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_HeadingID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_HeadingID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_subHeadingID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_SubHeadingID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_ItemsID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_ItemsID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_DELFLG", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASSI_DELFLG
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_CRBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_CRBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_STATUS", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sASSI_STATUS
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_UPDATEDBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_UPDATEDBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = objHeading.sASSI_IPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_CompId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_CompId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSI_YEARId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iASSI_YEARId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSi_scheduletype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_scheduletype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@ASSi_Orgtype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iSch_Orgtype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_ScheduleSubItems", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function SaveScheduleTemplate(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_Name", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sAST_Name
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_HeadingID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_HeadingID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_subHeadingID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_SubHeadingID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_ItemsID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_ItemsID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_subItemsID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_subItemsID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_AccHeadId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_AccHeadId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_DELFLG", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sAST_DELFLG
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_CRBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_CRBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_STATUS", OleDb.OleDbType.VarChar, 2)
                ObjParam(iParamCount).Value = objHeading.sAST_STATUS
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_UPDATEDBY", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_UPDATEDBY
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_IPAddress", OleDb.OleDbType.VarChar, 25)
                ObjParam(iParamCount).Value = objHeading.sAST_IPAddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_CompId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_CompId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_YEARId", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_YEARId
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_Schedule_type", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_Schedule_type
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_Companytype", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_Companytype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AST_Company_limit", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAST_Company_limit
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spACC_ScheduleTemplates", 1, Arr, ObjParam)

            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadHeading(ByVal sAc As String, ByVal iACID As Integer, ByVal iScheduleid As Integer, ByVal iOrgtypeid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("HeadingId")
            dtTab.Columns.Add("HeadingName")

            sSql = "select ASH_Name,ASH_ID from ACC_ScheduleHeading where Ash_Orgtype=" & iOrgtypeid & " And Ash_scheduletype=" & iScheduleid & ""
                sSql = sSql & " Order By ASH_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("HeadingId") = dt.Rows(i)("ASH_ID")
                dRow("HeadingName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ASH_Name"))
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadSUbHeading(ByVal sAc As String, ByVal iACID As Integer, ByVal iScheduleid As Integer, ByVal iOrgtypeid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SubheadingID")
            dtTab.Columns.Add("SubheadingName")
            dtTab.Columns.Add("Notes")

            sSql = "select ASSH_ID, ASSH_Name, ISNull(ASSH_Notes,0) as ASSH_Notes from ACC_ScheduleSubHeading where ASSh_Orgtype=" & iOrgtypeid & " And ASSH_scheduletype=" & iScheduleid & ""
                sSql = sSql & " Order By ASSH_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)

            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SubheadingID") = dt.Rows(i)("ASSH_ID")
                dRow("SubheadingName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ASSH_Name"))
                dRow("Notes") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ASSH_Notes"))
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadItems(ByVal sAc As String, ByVal iACID As Integer, ByVal iScheduleid As Integer, ByVal iOrgtypeid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("Itemsid")
            dtTab.Columns.Add("Itemsname")

            sSql = "select ASI_ID,ASI_Name from ACC_ScheduleItems where ASI_Orgtype=" & iOrgtypeid & " And ASI_scheduletype=" & iScheduleid & ""
                sSql = sSql & " Order By ASI_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)


            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("Itemsid") = dt.Rows(i)("ASI_ID")
                dRow("Itemsname") = (dt.Rows(i)("ASI_Name"))
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadSubItems(ByVal sAc As String, ByVal iACID As Integer, ByVal iScheduleid As Integer, ByVal iOrgtypeid As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("SubitemsiD")
            dtTab.Columns.Add("Subitemsname")

            sSql = "select ASSI_ID,ASSI_Name from ACC_ScheduleSubItems where ASSI_Orgtype=" & iOrgtypeid & " And ASSI_scheduletype=" & iScheduleid & ""
                sSql = sSql & " Order By ASSI_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)


            For i = 0 To dt.Rows.Count - 1
                dRow = dtTab.NewRow()
                dRow("SubitemsiD") = dt.Rows(i)("ASSI_ID")
                dRow("Subitemsname") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ASSI_Name"))
                dtTab.Rows.Add(dRow)
            Next
            Return dtTab
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadSchedulegrid(ByVal sAc As String, ByVal iACID As Integer, ByVal ISchedudeid As Integer, ByVal IcompanyType As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Try

            sSql = " select b.ASH_ID as Headingid,nullif(b.ASH_Name, lag(b.ASH_Name) over (order by ast_id))  as HeadingName,
                    c.ASSH_id SubheadingID,
					nullif(c.ASSH_Name, lag(c.ASSH_Name) over (order by ast_id))  as Subheadingname,
                    d.ASI_id as Itemid,
					nullif(d.ASI_Name, lag(d.ASI_Name) over (order by ast_id))  as itemname,
                    e.ASSI_ID as subitemid,e.ASSI_Name as subitemname,a.AST_AccHeadId as AST_AccHeadId,
                    CASE  WHEN a.AST_AccHeadId = 1 THEN 'CAPITALS AND LIABILITIES' WHEN a.AST_AccHeadId = 2 THEN 'FIXED ASSETS' 
                    WHEN a.AST_AccHeadId = 3 THEN 'No Account Case' END  AS AccHeadName
                    from ACC_ScheduleTemplates a
                    left join ACC_ScheduleHeading b on b.ASH_ID = a.ast_headingid and b.Ash_Orgtype= " & IcompanyType & "
                    left join ACC_ScheduleSubHeading c on c.ASSH_ID = a.ast_subheadingid and c.Assh_Orgtype= " & IcompanyType & "
                    left join  ACC_ScheduleItems d on d.ASI_ID = a.ast_itemid and d.Asi_Orgtype= " & IcompanyType & "
                    left join ACC_ScheduleSubItems e on e.ASSI_ID = a.ast_subitemid and e.Assi_Orgtype= " & IcompanyType & " "
                sSql = sSql & " Where AST_CompId = " & iACID & ""
                If ISchedudeid <> 0 Then
                    sSql = sSql & " And AST_Schedule_type = " & ISchedudeid & ""
                End If
                If IcompanyType <> 0 Then
                    sSql = sSql & " And AST_Companytype=" & IcompanyType & ""
                End If
                sSql = sSql & " order by AST_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadScheduleMaster(ByVal sAc As String, ByVal iACID As Integer, ByVal ISchedudeid As Integer, ByVal IcompanyType As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Try

            sSql = "select AST_Schedule_type,AST_Company_limit,AST_Companytype from ACC_ScheduleTemplates"
                sSql = sSql & " Where AST_Schedule_type= " & ISchedudeid & " And AST_CompId = " & iACID & ""
                If IcompanyType <> 0 Then
                    sSql = sSql + " And AST_Companytype=" & IcompanyType & ""
                End If
                sSql = sSql & " Order By AST_ID"
                dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CheckName_exist(ByVal sAc As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal HName As String, ByVal iselect As Integer, ByVal IScheduleID As Integer, ByVal iOrgtypeID As Integer) As Boolean
        Dim sSql As String = ""
        Dim iret As Boolean
        Try

            If iselect = 1 Then
                    sSql = "Select ASH_Name from ACC_ScheduleHeading"

                    sSql = sSql & " Where ASH_CompId= " & iACID & " And ASH_Name='" & HName & "' And Ash_scheduletype= " & IScheduleID & " And Ash_Orgtype=" & iOrgtypeID & ""
                ElseIf iselect = 2 Then
                    sSql = "select ASSH_Name from ACC_ScheduleSubHeading"
                    sSql = sSql & " Where ASSH_CompId= " & iACID & " And ASSH_Name='" & HName & "' And Assh_scheduletype= " & IScheduleID & " And Assh_Orgtype=" & iOrgtypeID & ""
                ElseIf iselect = 3 Then
                    sSql = "select ASI_Name from ACC_ScheduleItems"
                    sSql = sSql & " Where ASI_CompId= " & iACID & "  And ASI_Name='" & HName & "' And Asi_scheduletype= " & IScheduleID & " And Asi_Orgtype=" & iOrgtypeID & ""
                ElseIf iselect = 4 Then
                    sSql = "select ASSI_Name from ACC_ScheduleSubItems"
                    sSql = sSql & " Where ASSI_CompId= " & iACID & " And ASSI_Name='" & HName & "' And Assi_scheduletype= " & IScheduleID & " And Assi_Orgtype=" & iOrgtypeID & ""
                ElseIf iselect = 5 Then
                    sSql = "select AGA_Description from Acc_GroupingAlias"
                    sSql = sSql & " Where AGA_Compid= " & iACID & " and AGA_Description='" & HName & "' And AGA_scheduletype= " & IScheduleID & " And AGA_Orgtype=" & iOrgtypeID & ""
                End If

                iret = objDBL.SQLCheckForRecord(sAc, sSql)

            Return iret
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Updateheadings(ByVal sAc As String, ByVal iACID As Integer, ByVal IyearID As Integer, ByVal HName As String, ByVal iselect As Integer) As Boolean
        Dim sSql As String = ""
        Dim iret As Boolean
        Dim dt As New DataTable
        Try
            If iselect = 0 Then
                sSql = "select Ash_id from ACC_ScheduleHeading order by ash_id asc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                For i = 1 To dt.Rows.Count
                    sSql = "Update ACC_ScheduleHeading set ASH_Code= 'H00" & dt.Rows(i)("ASH_ID") & "' where Ash_id=" & dt.Rows(i)("ASH_ID") & ""
                    objDBL.SQLExecuteNonQuery(sAc, sSql)
                Next
            ElseIf iselect = 1 Then
                sSql = "select Assh_id from ACC_ScheduleSubHeading order by asSh_id asc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                For i = 1 To dt.Rows.Count
                    sSql = "Update ACC_ScheduleSubHeading set ASSH_Code= 'SH00" & dt.Rows(i)("ASSH_ID") & "' where Assh_id=" & dt.Rows(i)("ASSH_ID") & ""
                    objDBL.SQLExecuteNonQuery(sAc, sSql)
                Next
            ElseIf iselect = 2 Then
                sSql = "select ASI_id from ACC_ScheduleItems order by ASI_id asc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                For i = 1 To dt.Rows.Count
                    sSql = "Update ACC_ScheduleItems set ASI_Code= 'I00" & dt.Rows(i)("ASI_ID") & "' where Asi_id=" & dt.Rows(i)("ASI_ID") & ""
                    objDBL.SQLExecuteNonQuery(sAc, sSql)
                Next
            ElseIf iselect = 3 Then
                sSql = "select ASSI_id from ACC_ScheduleSubItems order by ASSI_id asc"
                dt = objDBL.SQLExecuteDataTable(sAc, sSql)
                For i = 1 To dt.Rows.Count
                    sSql = "Update ACC_ScheduleSubItems set ASSI_Code= 'SI00" & dt.Rows(i)("ASSI_ID") & "' where Assi_id=" & dt.Rows(i)("ASSI_ID") & ""
                    objDBL.SQLExecuteNonQuery(sAc, sSql)
                Next
            End If
            Return iret
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Updateshcduletemplate(ByVal sAc As String, ByVal iACID As Integer) As Boolean
        Dim sSql As String = ""
        Dim Iid As Integer = 0
        Dim dt As New DataTable
        Try
            sSql = ""
            sSql = " select * from ACC_ScheduleTemplates1 Where AST_Schedule_type in (4,3) and  AST_CompId = 1  And AST_Companytype=28"
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            sSql = " select max(AST_ID+1) from ACC_ScheduleTemplates"
            Iid = objDBL.SQLExecuteScalarInt(sAc, sSql)
            For i = 0 To dt.Rows.Count - 1
                Iid = Iid + 1
                sSql = " update ACC_ScheduleTemplates1 set AST_ID=" & Iid & " where AST_ID=" & dt.Rows(i)("AST_ID") & ""
                objDBL.SQLExecuteNonQuery(sAc, sSql)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SaveScheduleHeadingAliasDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal objHeading As clsScheduleTemplate) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try

            iParamCount = 0
                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_ID", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Value = objHeading.iAGA_ID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_Description", OleDb.OleDbType.VarChar, 5000)
                ObjParam(iParamCount).Value = objHeading.sAGA_Description
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_GLID", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_GLID
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_GLDESC", OleDb.OleDbType.VarChar)
                ObjParam(iParamCount).Value = objHeading.sAGA_GLDESC
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_GrpLevel", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_GrpLevel
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_scheduletype", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_scheduletype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_Orgtype", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_Orgtype
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_Compid", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_Compid
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_Status", OleDb.OleDbType.VarChar)
                ObjParam(iParamCount).Value = objHeading.sAGA_Status
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_Createdby", OleDb.OleDbType.Integer, 4)
                ObjParam(iParamCount).Value = objHeading.iAGA_Createdby
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@AGA_IPaddress", OleDb.OleDbType.VarChar)
                ObjParam(iParamCount).Value = objHeading.sAGA_IPaddress
                ObjParam(iParamCount).Direction = ParameterDirection.Input
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                iParamCount += 1

                ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
                ObjParam(iParamCount).Direction = ParameterDirection.Output
                Arr(0) = "@iUpdateOrSave"
                Arr(1) = "@iOper"

                Arr = objDBL.ExecuteSPForInsertARR(sNameSpace, "spAcc_GroupingAlias", 1, Arr, ObjParam)

        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function DeleteAlias(ByVal sAc As String, ByVal iACID As Integer, ByVal lblid As Integer) As Boolean
        Dim sSql As String = ""
        Dim Iid As Integer = 0
        Dim dt As New DataTable
        Try

            sSql = "delete from Acc_GroupingAlias  Where AGA_ID =" & lblid & ""
            objDBL.SQLExecuteNonQuery(sAc, sSql)

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function LoadGridView1grid(ByVal sAc As String, ByVal iACID As Integer, ByVal ISchedudeid As Integer, ByVal IcompanyType As Integer, ByVal lblid As Integer, ByVal iSelectedVal As Integer) As DataTable
        Dim sSql As String
        Dim dtTab As New DataTable, dt As New DataTable
        Try
            sSql = " select AGA_ID as Headingid, AGA_Description as Alias from Acc_GroupingAlias where AGA_GLID=" & lblid & " and AGA_GrpLevel=" & iSelectedVal & " "
            dt = objDBL.SQLExecuteDataSet(sAc, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class
