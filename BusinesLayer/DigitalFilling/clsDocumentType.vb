Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Structure StrDocType
    Dim iEdpid As Integer
    Dim iDocTypeId As Integer
    Dim sPtype As String
    Dim iGrpId As Int16
    Dim iUsrid As Int16
    Dim iIndex As Int16
    Dim iSearch As Int16
    Dim iMdFType As Int16
    Dim iMdFDoc As Int16
    Dim iDeldoc As Int16
    Dim iOthers As Int16
    Dim sName As String
    Dim sNotes As String
    Dim ScrBy As String
    Dim sCrOn As String
    Dim sGrp As String
    Dim sDesc As String
    Dim sDtype As String
    Dim iGlobal As Integer
    Dim iEDP_CRBY As Integer
    Dim iEDP_UPDATEDBY As Integer
    Dim iEDP_CompId As Integer
    Dim sEDP_IPAddress As String


    Public Property iEDPCRBY() As Integer
        Get
            Return (iEDP_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iEDP_CRBY = Value
        End Set
    End Property
    Public Property iEDPUPDATEDBY() As Integer
        Get
            Return (iEDP_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iEDP_UPDATEDBY = Value
        End Set
    End Property
    Public Property iEDPCompId() As Integer
        Get
            Return (iEDP_CompId)
        End Get
        Set(ByVal Value As Integer)
            iEDP_CompId = Value
        End Set
    End Property
    Public Property sEDPIPAddress() As String
        Get
            Return (sEDP_IPAddress)
        End Get
        Set(ByVal Value As String)
            sEDP_IPAddress = Value
        End Set
    End Property
End Structure
Public Structure strDocType_Details
    Dim iEDD_DOCTYPEID As Integer
    Dim iEDD_DPTRID As Integer
    Dim sEDD_ISREQUIRED As String
    Dim sEDD_Size As String
    Dim sEDD_VALUES As String
    Dim sEDD_VALIDATE As String
    Public Property iDocID() As Integer
        Get
            Return (iEDD_DOCTYPEID)
        End Get
        Set(ByVal Value As Integer)
            iEDD_DOCTYPEID = Value
        End Set
    End Property
    Public Property iDescId() As Integer
        Get
            Return (iEDD_DPTRID)
        End Get
        Set(ByVal Value As Integer)
            iEDD_DPTRID = Value
        End Set
    End Property
    Public Property sMandatory() As String
        Get
            Return (sEDD_ISREQUIRED)
        End Get
        Set(ByVal Value As String)
            sEDD_ISREQUIRED = Value
        End Set
    End Property
    Public Property sSize() As String
        Get
            Return (sEDD_Size)
        End Get
        Set(ByVal Value As String)
            sEDD_Size = Value
        End Set
    End Property
    Public Property sValues() As String
        Get
            Return (sEDD_VALUES)
        End Get
        Set(ByVal Value As String)
            sEDD_VALUES = Value
        End Set
    End Property
    Public Property sValidate() As String
        Get
            Return (sEDD_VALIDATE)
        End Get
        Set(ByVal Value As String)
            sEDD_VALIDATE = Value
        End Set
    End Property
End Structure
Public Structure strEDT_DOCUMENT_TYPE
    Dim iDOT_DOCTYPEID As Integer
    Dim sDOT_DOCNAME As String
    Dim sDOT_NOTE As String
    Dim iDOT_PGROUP As Integer
    Dim iDOT_CRBY As Integer
    Dim iDOT_UPDATEDBY As Integer
    Dim sDOT_operation As String
    Dim iDOT_operationby As Integer
    Dim iDOT_isGlobal As Integer
    Dim iDOT_CompId As Integer
    Dim sDOT_IPAddress As String
    Dim sDelFlag As String
    Dim sDOT_Status As String
    Dim sDOT_DelFlag As String

    Public Property sDOCTYPEStatus() As String
        Get
            Return (sDOT_Status)
        End Get
        Set(ByVal Value As String)
            sDOT_Status = Value
        End Set
    End Property
    Public Property sDOCTYPEFlag() As String
        Get
            Return (sDOT_DelFlag)
        End Get
        Set(ByVal Value As String)
            sDOT_DelFlag = Value
        End Set
    End Property
    Public Property iDOCTYPEID() As Integer
        Get
            Return (iDOT_DOCTYPEID)
        End Get
        Set(ByVal Value As Integer)
            iDOT_DOCTYPEID = Value
        End Set
    End Property
    Public Property sDOCNAME() As String
        Get
            Return (sDOT_DOCNAME)
        End Get
        Set(ByVal Value As String)
            sDOT_DOCNAME = Value
        End Set
    End Property
    Public Property sNOTE() As String
        Get
            Return (sDOT_NOTE)
        End Get
        Set(ByVal Value As String)
            sDOT_NOTE = Value
        End Set
    End Property
    Public Property iPGROUP() As Integer
        Get
            Return (iDOT_PGROUP)
        End Get
        Set(ByVal Value As Integer)
            iDOT_PGROUP = Value
        End Set
    End Property
    Public Property iCRBY() As Integer
        Get
            Return (iDOT_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iDOT_CRBY = Value
        End Set
    End Property
    Public Property iDOTUPDATEDBY() As Integer
        Get
            Return (iDOT_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iDOT_UPDATEDBY = Value
        End Set
    End Property
    Public Property sOperation() As String
        Get
            Return (sDOT_operation)
        End Get
        Set(ByVal Value As String)
            sDOT_operation = Value
        End Set
    End Property
    Public Property iOperationby() As Integer
        Get
            Return (iDOT_operationby)
        End Get
        Set(ByVal Value As Integer)
            iDOT_operationby = Value
        End Set
    End Property
    Public Property iIsGlobal() As Integer
        Get
            Return (iDOT_isGlobal)
        End Get
        Set(ByVal Value As Integer)
            iDOT_isGlobal = Value
        End Set
    End Property
    Public Property iDOTCompId() As Integer
        Get
            Return (iDOT_CompId)
        End Get
        Set(ByVal Value As Integer)
            iDOT_CompId = Value
        End Set
    End Property
    Public Property sDOTIPAddress() As String
        Get
            Return (sDOT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sDOT_IPAddress = Value
        End Set
    End Property
End Structure
Public Structure strEDT_DOCTYPE_LINK
    Dim iEDD_Pk As Integer
    Dim iEDD_DOCTYPEID As Integer
    Dim iEDD_DPTRID As Integer
    Dim sEDD_ISREQUIRED As String
    Dim iEDD_Size As Integer
    Dim sEDD_VALUES As String
    Dim sEDD_VALIDATE As String
    Dim iEDD_CRBY As Integer
    Dim iEDD_UPDATEDBY As Integer
    Dim iEDD_CompId As Integer
    Dim sEDD_IPAddress As String
    Public Property iPkID() As Integer
        Get
            Return (iEDD_Pk)
        End Get
        Set(ByVal Value As Integer)
            iEDD_Pk = Value
        End Set
    End Property
    Public Property iDOCTYPEID() As Integer
        Get
            Return (iEDD_DOCTYPEID)
        End Get
        Set(ByVal Value As Integer)
            iEDD_DOCTYPEID = Value
        End Set
    End Property
    Public Property iDPTRID() As Integer
        Get
            Return (iEDD_DPTRID)
        End Get
        Set(ByVal Value As Integer)
            iEDD_DPTRID = Value
        End Set
    End Property
    Public Property sISREQUIRED() As String
        Get
            Return (sEDD_ISREQUIRED)
        End Get
        Set(ByVal Value As String)
            sEDD_ISREQUIRED = Value
        End Set
    End Property
    Public Property iSize() As Integer
        Get
            Return (iEDD_Size)
        End Get
        Set(ByVal Value As Integer)
            iEDD_Size = Value
        End Set
    End Property
    Public Property sVALUES() As String
        Get
            Return (sEDD_VALUES)
        End Get
        Set(ByVal Value As String)
            sEDD_VALUES = Value
        End Set
    End Property
    Public Property sVALIDATE() As String
        Get
            Return (sEDD_VALIDATE)
        End Get
        Set(ByVal Value As String)
            sEDD_VALIDATE = Value
        End Set
    End Property
    Public Property iEDDCRBY() As Integer
        Get
            Return (iEDD_CRBY)
        End Get
        Set(ByVal Value As Integer)
            iEDD_CRBY = Value
        End Set
    End Property
    Public Property iEDDUPDATEDBY() As Integer
        Get
            Return (iEDD_UPDATEDBY)
        End Get
        Set(ByVal Value As Integer)
            iEDD_UPDATEDBY = Value
        End Set
    End Property
    Public Property iEDDCompId() As Integer
        Get
            Return (iEDD_CompId)
        End Get
        Set(ByVal Value As Integer)
            iEDD_CompId = Value
        End Set
    End Property
    Public Property sEDDIPAddress() As String
        Get
            Return (sEDD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sEDD_IPAddress = Value
        End Set
    End Property
End Structure

Public Class clsDocumentType
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsEDICTGeneral As New clsEDICTGeneral

    Public Function LoadUserOtherDepartment(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As DataTable
        Dim sSql As String
        Try
            'sSql = "Select Org_Node,Org_Name from Sad_Org_Structure Left Join Sad_UsersInOtherDept On SUO_DeptID=Org_Node"
            'sSql = sSql & " where Org_DelFlag='A' And Org_CompID=" & iACID & " And SUO_CompID=" & iACID & " And SUO_UserID=" & iUserID & " And Org_LevelCode=3"

            sSql = "Select distinct Org_Node,Org_Name from Sad_Org_Structure Left Join Sad_UsersInOtherDept On SUO_DeptID=Org_Node"
            sSql = sSql & " where Org_DelFlag='A' And Org_CompID=" & iACID & " And Org_LevelCode=3 order by Org_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDepartment(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Org_Node,Org_Name from Sad_Org_Structure where Org_LevelCode=3 And Org_DelFlag='A' And Org_CompID=" & iACID & " order by Org_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUsersFromDept(ByVal sAC As String, ByVal iDeptID As String) As DataTable
        Dim sSql As String
        Dim dtDesc As New DataTable
        Try
            sSql = "Select Usr_ID,Usr_Fullname From Sad_Userdetails,Sad_UsersInOtherDept where SUO_DeptID='" & iDeptID & "'"
            sSql = sSql & " And SUO_UserID=Usr_ID And Usr_Delflag='A' order by Usr_Fullname"
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDescriptor(ByVal sAC As String) As DataTable
        Dim sSql As String
        Dim dtDesc As New DataTable
        Try
            sSql = "Select DES_ID,DESC_NAME From EDT_DESCRIPTOR where DESC_DelFlag='A' Order by DESC_NAME Asc"
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDesc
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function GetDocTypeDetails(ByVal sAC As String, ByVal iDocId As Integer, ByVal sDept As String, Optional ByVal sUsrName As String = "", Optional ByVal sOrderBy As String = "DOT_DOCNAME", Optional ByVal sOrderType As String = "ASC") As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("DocTypeID")
            dt.Columns.Add("Name")
            dt.Columns.Add("DepartmentID")
            dt.Columns.Add("Department")
            dt.Columns.Add("Note")
            dt.Columns.Add("CrBy")
            dt.Columns.Add("CrOn")
            dt.Columns.Add("Status")
            dt.Columns.Add("IsGlobal")

            sSql = "Select a.DOT_DOCTYPEID,a.DOT_DOCNAME,c.Usr_FullName as DOT_CRBY,a.DOT_NOTE,b.Org_Node As DOT_PGROUPID,b.Org_Name as DOT_PGROUP,a.DOT_CRON,"
            sSql = sSql & " a.DOT_STATUS,DOT_isGlobal,a.DOT_DelFlag From EDT_DOCUMENT_TYPE a,Sad_Org_Structure b,Sad_UserDetails c Where a.DOT_PGROUP=Org_Node and c.Usr_ID= a.DOT_CRBY"
            If iDocId > 0 Then
                sSql = sSql & " And a.DOT_DOCTYPEID=" & iDocId & ""
            End If
            If sDept <> "" Then
                sSql = sSql & " And b.Org_Node In (" & sDept & ")"
            End If
            If sUsrName <> "" Then
                sSql = sSql & " And " & sOrderBy & " Like ('" & sUsrName & "%') Order By " & sOrderBy & " " & sOrderType & ""
            Else
                sSql = sSql & " Order By " & sOrderBy & " " & sOrderType
            End If
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    If IsDBNull(dtdetails.Rows(i)("DOT_DOCTYPEID")) = False Then
                        dRow("DocTypeID") = dtdetails.Rows(i)("DOT_DOCTYPEID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_DOCNAME")) = False Then
                        dRow("Name") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DOT_DOCNAME"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_PGROUPID")) = False Then
                        dRow("DepartmentID") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DOT_PGROUPID"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_PGROUP")) = False Then
                        dRow("Department") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DOT_PGROUP"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_NOTE")) = False Then
                        dRow("Note") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DOT_NOTE"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_CRBY")) = False Then
                        dRow("CrBy") = objclsEDICTGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("DOT_CRBY"))
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_CRON")) = False Then
                        dRow("CrOn") = objclsEDICTGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("DOT_CRON"), "F")
                    End If
                    If dtdetails.Rows(i)("DOT_DelFlag") = "A" Then
                        dRow("Status") = "Activated"
                    ElseIf dtdetails.Rows(i)("DOT_DelFlag") = "D" Then
                        dRow("Status") = "De-Activated"
                    ElseIf dtdetails.Rows(i)("DOT_DelFlag") = "W" Then
                        dRow("Status") = "Waiting for Approval"
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DOT_isGlobal")) = False Then
                        If dtdetails.Rows(i)("DOT_isGlobal") = "0" Then
                            dRow("IsGlobal") = "0"
                        Else
                            dRow("IsGlobal") = "1"
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
    Public Sub DocTypeApproveStatus(ByVal sAC As String, ByVal iCompId As Integer, ByVal iUserID As Integer, ByVal iDocTypeID As Integer, ByVal sType As String)
        Dim sSql As String
        Try
            sSql = "Update EDT_DOCUMENT_TYPE set"
            If sType = "Created" Then
                sSql = sSql & " DOT_DelFlag='A',DOT_STATUS='A',DOT_APPROVEDBY=" & iUserID & ", DOT_APPROVEDON=Getdate()"
            ElseIf sType = "De-Activated" Then
                sSql = sSql & " DOT_DelFlag='D',DOT_STATUS='AD',DOT_DELETEDBY=" & iUserID & ", DOT_DELETEDON=Getdate()"
            ElseIf sType = "Activated" Then
                sSql = sSql & " DOT_DelFlag='A',DOT_STATUS='AR',DOT_RECALLBY=" & iUserID & ", DOT_RECALLON=Getdate()"
            End If
            sSql = sSql & " Where DOT_DOCTYPEID=" & iDocTypeID & " And DOT_CompId=" & iCompId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadDescriptorGrid(ByVal sAC As String, ByVal iDescID As Integer) As DataTable ', ByVal iDocTypeID As Integer
        Dim sSql As String
        Dim dtDesc As New DataTable
        Try
            sSql = "Select DESC_NAME,Dt_Name,Desc_Size From EDT_DESCRIPTOR,EDT_DESC_TYPE where Des_ID=" & iDescID & " And Desc_DataType=DT_ID"
            dtDesc = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dtDesc
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDescDetails(ByVal sAC As String, ByVal iDocId As Integer) As DataTable
        Dim sSql As String
        Dim dtDecs As New DataTable, dtdetails As New DataTable
        Dim dRow As DataRow

        dtDecs.Columns.Add("DescId")
        dtDecs.Columns.Add("Descriptor")
        dtDecs.Columns.Add("DataType")
        dtDecs.Columns.Add("Size")
        dtDecs.Columns.Add("Mandatory")
        dtDecs.Columns.Add("Values")
        dtDecs.Columns.Add("Validator")
        Try
            sSql = "Select EDD_DPTRID,EDD_SIZE,EDD_ISREQUIRED,EDD_VALUES,EDD_Validate,DESC_Name,Dt_Name From EDT_DOCTYPE_LINK"
            sSql = sSql & " Left Join EDT_DESCRIPTOR On DES_ID=EDD_DPTRID Left Join EDT_DESC_TYPE On DT_ID=DESC_DATATYPE Where EDD_DOCTYPEID=" & iDocId & ""
            dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dtDecs.NewRow
                    If IsDBNull(dtdetails.Rows(i)("EDD_DPTRID")) = False Then
                        dRow("DescId") = dtdetails.Rows(i)("EDD_DPTRID")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("DESC_Name")) = False Then
                        dRow("Descriptor") = dtdetails.Rows(i)("DESC_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("Dt_Name")) = False Then
                        dRow("DataType") = dtdetails.Rows(i)("Dt_Name")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_SIZE")) = False Then
                        dRow("Size") = dtdetails.Rows(i)("EDD_SIZE")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_ISREQUIRED")) = False Then
                        If dtdetails.Rows(i)("EDD_ISREQUIRED") = "Q" Then
                            dRow("Mandatory") = "N"
                        Else
                            dRow("Mandatory") = "Y"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_VALUES")) = False Then
                        dRow("Values") = dtdetails.Rows(i)("EDD_VALUES")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("EDD_Validate")) = False Then
                        If dtdetails.Rows(i)("EDD_Validate") = "N" Then
                            dRow("Validator") = "N"
                        Else
                            dRow("Validator") = "Y"
                        End If
                    End If
                    dtDecs.Rows.Add(dRow)
                Next
            End If
            Return dtDecs
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function chkRemFlagDTPerm(ByVal sAC As String, ByVal sUserGrp As String, ByVal iFolID As Integer, ByVal iUserid As Integer, ByVal iGrpID As Integer) As Boolean
        Dim sSql As String
        Try
            If (UCase(sUserGrp) = "USER") Then
                sSql = "Select * from edt_doctype_Permission where EDP_DocTypeID=" & iFolID & " And EDP_UsrID=" & iUserid & "  And EDP_PType='U'"
                Return objDBL.DBCheckForRecord(sAC, sSql)
            ElseIf (UCase(sUserGrp) = "GROUP") Then
                sSql = "Select * from edt_doctype_Permission where EDP_DocTypeID=" & iFolID & " and  EDP_GrpID=" & iGrpID & " and EDP_PType='G'"
                Return objDBL.DBCheckForRecord(sAC, sSql)
            ElseIf (UCase(sUserGrp) = "EVERYONE") Then
                sSql = "Select * from edt_doctype_Permission where EDP_DocTypeID=" & iFolID & " and   EDP_PType='E'"
                Return objDBL.DBCheckForRecord(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadDocumentPermissionDetails(ByVal sAC As String, ByVal iDocId As Integer, ByVal iGrp As Integer, ByVal sPtype As String)
        Dim sSql As String
        Dim ObjStr As New StrDocType
        Dim dt As New DataTable
        Try
            sSql = "Select EDP_PTYPE,EDP_GRPID,EDP_USRID,EDP_INDEX,EDP_SEARCH,EDP_MFY_TYPE,EDP_MFY_DOCUMENT,EDP_DEL_DOCUMENT,EDP_OTHER from EDT_DOCTYPE_PERMISSION"
            sSql = sSql & " where EDP_DOCTYPEID=" & iDocId & " And Edp_Grpid=" & iGrp & " And edp_ptype='" & sPtype & "'"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i)("EDP_GRPID")) = False Then
                        ObjStr.iGrpId = dt.Rows(i)("EDP_GRPID")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_USRID")) = False Then
                        ObjStr.iUsrid = dt.Rows(i)("EDP_USRID")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_INDEX")) = False Then
                        ObjStr.iIndex = dt.Rows(i)("EDP_INDEX")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_SEARCH")) = False Then
                        ObjStr.iSearch = dt.Rows(i)("EDP_SEARCH")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_DEL_DOCUMENT")) = False Then
                        ObjStr.iDeldoc = dt.Rows(i)("EDP_DEL_DOCUMENT")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_MFY_TYPE")) = False Then
                        ObjStr.iMdFDoc = dt.Rows(i)("EDP_MFY_TYPE")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_MFY_DOCUMENT")) = False Then
                        ObjStr.iMdFType = dt.Rows(i)("EDP_MFY_DOCUMENT")
                    End If
                    If IsDBNull(dt.Rows(i)("EDP_OTHER")) = False Then
                        ObjStr.iOthers = dt.Rows(i)("EDP_OTHER")
                    End If
                Next
            End If
            Return ObjStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAvailability(ByVal sAC As String, ByVal sDocTypeName As String, Optional ByVal iDocID As Int16 = 0, Optional ByVal iDeptmentID As Integer = 0) As Boolean
        Dim sSql As String
        Dim iRet As Integer
        Try
            If iDocID <> 0 Then
                sSql = "Select count(*) From EDT_DOCUMENT_TYPE where Dot_Docname='" & sDocTypeName & "' And  dot_doctypeid<>" & iDocID & " and dot_pgroup=" & iDeptmentID & ""
            Else
                sSql = "Select count(*) From EDT_DOCUMENT_TYPE where Dot_Docname='" & sDocTypeName & "' And dot_pgroup=" & iDeptmentID & ""
            End If
            iRet = objDBL.SQLExecuteScalar(sAC, sSql)
            If iRet = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckGrpHead(ByVal sAC As String, ByVal iUserid As Integer, ByVal sCrBy As Integer) As Integer
        Dim sSql As String
        Dim iRet As Integer
        Try
            sSql = "Select SUO_IsDeptHead from Sad_UsersInOtherDept where SUO_DeptID=" & sCrBy & " And SUO_UserID=" & iUserid & ""
            iRet = objDBL.SQLExecuteScalar(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeletePermission(ByVal sAC As String, ByVal iDocID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete from EDT_DOCTYPE_LINK where EDD_DOCTYPEID=" & iDocID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub InsertFlg(ByVal sAC As String, ByVal iDocId As Integer)
        Dim sSQL As String
        Try
            sSQL = "Update edt_doctype_Permission set edp_when='C' where edp_doctypeid=" & iDocId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSQL)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveDocTypeDetails(ByVal sAC As String, ByVal ObjstrEDT_DOCUMENT_TYPE As strEDT_DOCUMENT_TYPE) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_DOCTYPEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iDOCTYPEID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_DOCNAME", OleDb.OleDbType.VarChar, 400)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sDOCNAME
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_NOTE", OleDb.OleDbType.VarChar, 600)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sNOTE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_PGROUP", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iPGROUP
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iDOTUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@dot_operation", OleDb.OleDbType.VarChar, 10)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sOperation
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@dot_operationby", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iOperationby
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_isGlobal", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iIsGlobal
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.iDOTCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sDOTIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_STATUS", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@DOT_DelFlag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEFlag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_DOCUMENT_TYPE", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SavePermissionDetails(ByVal sAC As String, ByVal ObjstrEDT_DOCTYPE_LINK As strEDT_DOCTYPE_LINK) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_Pk", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iPkID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_DOCTYPEID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iDOCTYPEID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_DPTRID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iDPTRID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_ISREQUIRED", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.sISREQUIRED
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_Size", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iSize
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_VALUES", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.sVALUES
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_VALIDATE", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.sVALIDATE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iEDDCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iEDDUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.iEDDCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDD_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjstrEDT_DOCTYPE_LINK.sEDDIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spEDT_DOCTYPE_LINK", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveDocPermissions(ByVal sAC As String, ByVal ObjStr As StrDocType) As Array
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer = 0
        Dim Arr(1) As String
        Try
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_PID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjStr.iEdpid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_DOCTYPEID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjStr.iDocTypeId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_PTYPE", OleDb.OleDbType.VarChar, 1)
            ObjParam(iParamCount).Value = ObjStr.sPtype
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_GRPID", OleDb.OleDbType.SmallInt)
            ObjParam(iParamCount).Value = ObjStr.iGrpId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_USRID", OleDb.OleDbType.SmallInt)
            ObjParam(iParamCount).Value = ObjStr.iUsrid
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_INDEX", OleDb.OleDbType.SmallInt)
            ObjParam(iParamCount).Value = ObjStr.iIndex
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_SEARCH", OleDb.OleDbType.TinyInt)
            ObjParam(iParamCount).Value = ObjStr.iSearch
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_MFY_TYPE", OleDb.OleDbType.TinyInt)
            ObjParam(iParamCount).Value = ObjStr.iMdFType
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_MFY_DOCUMENT", OleDb.OleDbType.TinyInt)
            ObjParam(iParamCount).Value = ObjStr.iMdFDoc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_DEL_DOCUMENT", OleDb.OleDbType.TinyInt)
            ObjParam(iParamCount).Value = ObjStr.iDeldoc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_OTHER", OleDb.OleDbType.TinyInt)
            ObjParam(iParamCount).Value = ObjStr.iOthers
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_CRBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjStr.iEDPCRBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_UPDATEDBY", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjStr.iEDPUPDATEDBY
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_CompId", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = ObjStr.iEDPCompId
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@EDP_IPAddress", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = ObjStr.sEDPIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "InOrUpDOCTYPEPER", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckDoctumentTypeAvailability(ByVal sAC As String, ByVal sDocTypeName As String, ByVal iDeptmentID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where Dot_Docname='" & sDocTypeName & "' And dot_pgroup=" & iDeptmentID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Dim iParGrp As Integer
    Dim dtPerm As DataTable
    Dim sPermLvl As String
    Dim dsMain As DataSet
    Dim sDTPerm As String
    Dim iUsrParGrp As Integer = 0
    Public Function GetFinalDTPermissions(ByVal iDTId As Integer, ByVal iUserId As Int16, ByVal sAC As String, Optional ByVal sPerType As String = "ALL") As Object
        'Dim objDB As New DBGeneral(sConstr, sRDBMS)
        'Dim dsMain As DataSet
        'Dim Ht As Object
        'Dim iParGrp As Integer
        Try
            'First Get the Parent GroupId of the Cabinet
            iParGrp = GetParGrpID(iDTId, sAC)
            iUsrParGrp = GetUserParGrp(sAC, iUserId)
            dtPerm = GetMainPermDS(iDTId, iUserId, iParGrp, sAC)
            If (dtPerm.Rows.Count > 0) Then
                Select Case UCase(sPerType)
                    Case "ALL"
                        Dim Ht As New Hashtable
                        If (sPermLvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Ht.Add("DINDEX", 1)
                                Ht.Add("DSEARCH", 1)
                                Ht.Add("MDOCTYPE", 0)
                                Ht.Add("MDOC", 0)
                                Ht.Add("DDOC", 0)
                                Ht.Add("CDOC", 0)
                                'Ht.Add("CSearch", 0)
                                Ht.Add("Level", sPermLvl)
                            Else
                                Ht.Add("DINDEX", 0)
                                Ht.Add("DSEARCH", 1)
                                Ht.Add("MDOCTYPE", 0)
                                Ht.Add("MDOC", 0)
                                Ht.Add("DDOC", 0)
                                Ht.Add("CDOC", 0)
                                'Ht.Add("CSearch", 0)
                                Ht.Add("Level", sPermLvl)
                            End If

                            Return Ht
                        ElseIf (sPermLvl = "GH") Then
                            Ht.Add("DINDEX", 1)
                            Ht.Add("DSEARCH", 1)
                            Ht.Add("MDOCTYPE", 1)
                            Ht.Add("MDOC", 1)
                            Ht.Add("DDOC", 1)
                            Ht.Add("CDOC", 1)
                            'Ht.Add("CSearch", 0)
                            Ht.Add("Level", sPermLvl)
                            Return Ht
                        Else
                            Ht.Add("DINDEX", dtPerm.Rows(0).Item("EDP_INDEX"))
                            Ht.Add("DSEARCH", dtPerm.Rows(0).Item("EDP_SEARCH"))
                            Ht.Add("MDOCTYPE", dtPerm.Rows(0).Item("EDP_MFY_TYPE"))
                            Ht.Add("MDOC", dtPerm.Rows(0).Item("EDP_MFY_DOCUMENT"))
                            Ht.Add("DDOC", dtPerm.Rows(0).Item("EDP_DEL_DOCUMENT"))
                            Ht.Add("CDOC", dtPerm.Rows(0).Item("EDP_OTHER"))
                            'Ht.Add("CSearch", 0)
                            Ht.Add("Level", sPermLvl)
                            Return Ht

                        End If

                    Case "IND"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            If (iUsrParGrp = iParGrp) Then
                                Return 1
                            Else
                                Return 0
                            End If

                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_INDEX")
                    Case "SRH"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 1
                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_SEARCH")
                    Case "MDT"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_MFY_TYPE")
                    Case "MDC"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 1
                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_MFY_DOCUMENT")
                    Case "DDC"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_DEL_DOCUMENT")
                    Case "CDC"
                        'dsPerm = dsMain
                        If (sPermLvl = "PG") Then
                            Return 0
                        ElseIf (sPermLvl = "GH") Then
                            Return 1
                        End If
                        Return dtPerm.Rows(0).Item("EDP_OTHER")

                End Select
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetParGrpID(ByVal iDTId As Integer, ByVal sAC As String) As Integer
        ' Dim objDB As New DBLayer.DBGeneral(sConStr, sRDBMS)
        Dim strsql As String
        Try
            strsql = "Select Dot_PGroup from edt_document_type where Dot_DocTypeID=" & iDTId & ""
            Return (objDBL.SQLExecuteScalar(sAC, strsql))
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUserParGrp(ByVal sNameSpace As String, ByVal iLogUsrID As Integer) As Integer
        Dim strsql As String
        Try
            strsql = "Select usr_deptid from sad_Userdetails where usr_id=" & iLogUsrID & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, strsql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function GetMainPermDS(ByVal iDtID As Integer, ByVal iUserId As Int16, ByVal iGrpId As Int16, ByVal sAC As String, Optional ByVal ChkType As Integer = 0) As DataTable
        ' Dim objDB As New DBLayer.DBGeneral(sConstr, sRDBMS)
        Dim strsql, sCab As String
        Dim dtPerm As DataTable
        Dim sMem As String
        Dim dsMain1 As DataTable
        Try
            sCab = ""

            sPermLvl = String.Empty
            sMem = GetMemberGroups(sAC, iUserId)
            If (CheckForGrpHead(sAC, iGrpId, iUserId) = 1) Then
                'If 1 = 1 Then
                sPermLvl = "GH"
                strsql = "Select * from edt_Document_Type where Dot_DocTypeID=" & iDtID & ""
                dsMain1 = objDBL.SQLExecuteDataTable(sAC, strsql)
            ElseIf ChkType <> 1 Then
                'If (CheckForGrpMember(iGrpId, iUserId) = True) Then
                'strsql = "select * from edt_cabinet left outer join edt_cabinet_permission on cbn_node=cbp_cabid where CBN_Node=" & iCabId & " and CBN_ParGrp=" & iGrpId & " and CBP_CabId not in ( select distinct(CBP_CabId)  from edt_cabinet_permission where CBP_UsrId=" & iUserId & " or CBP_Grpid in (" & sMem & ") or CBP_PType='E') "
                'If (sCabPerm = String.Empty) Then
                sDTPerm = GetPermDocTypes(iUserId, sMem, sAC)
                'End If
                strsql = " Select *  from edt_Document_Type left outer join edt_doctype_permission on DOt_DocTYpeID=EDP_DocTYpeID where  Dot_PGroup in (" & sMem & ") and Dot_Status='A' and Dot_DocTypeId= " & iDtID & " "
                If Val(sDTPerm) <> 0 Then
                    strsql = strsql & " and (EDP_DocTypeID not in (" & sDTPerm & " ) or EDP_DocTypeID is Null)  "
                End If
                If objDBL.DBCheckForRecord(sAC, strsql) = True Then
                    dsMain1 = objDBL.SQLExecuteDataTable(sAC, strsql)
                    sPermLvl = "PG"
                Else
                    GoTo LP
                End If
            Else
LP:             dsMain1 = BuildPermDataSet(iUserId, iDtID, sMem, sAC)
                Try
                    If dsMain1.Rows.Count <> 0 Then
                        dtPerm = dsMain1
                        dtPerm = GetFinalPermForDS(dtPerm)
                        Return dtPerm
                    Else
                        Dim MyDt As New DataTable
                        Return MyDt
                    End If
                Catch
                End Try
            End If
            Return dsMain1
            'LP:             dsMain = BuildPermDataSet(iUserId, iDtID, sMem, sAC)
            '                Try
            '                    If dsMain.Tables.Count <> 0 Then
            '                        dtPerm = dsMain.Tables(0)
            '                        dtPerm = GetFinalPermForDS(dtPerm)
            '                        Return dtPerm
            '                    Else
            '                        Dim MyDt As New DataTable
            '                        Return MyDt
            '                    End If
            '                Catch
            '                End Try
            '            End If
            '            Return dsMain.Tables(0)

        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetPermDocTypes(ByVal iUserID As String, ByVal sGrpID As String, ByVal sNameSpace As String) As String
        Dim strsql As String
        ' Dim objDB As New DBLayer.DBGeneral(sConStr, sRDBMS)
        Dim Arr() As String
        Dim i As Integer
        Dim sCabId As String = ""
        Dim sRet As String
        Dim sFArr() As String
        Try
            Arr = Split(sGrpID, ",")
            For i = 0 To UBound(Arr)
                strsql = "edt_docType_permission where EDP_GrpId = " & Arr(i) & " and (EDP_UsrId=" & iUserID & " or EDP_UsrId=0)"
                sRet = objDBL.GetAllValues(sNameSpace, "EDP_DocTypeID", strsql)
                'sCabId = sCabId & objDB.GetAllValues("CBP_CabId", strsql)
                If Val(sRet) <> 0 Then
                    'If InStr(sRet, ";") <> 0 Then
                    If Right(sRet, 1) = ";" Then
                        sRet = Left(sRet, Len(sRet) - 1)
                    End If
                    sCabId = sCabId & ";" & sRet & ";"
                End If
            Next
            strsql = "edt_docType_permission where Edp_ptype = 'E'"
            sCabId = sCabId & objDBL.GetAllValues(sNameSpace, "EDP_DocTypeID", strsql)
            sCabId = Replace(sCabId, ";", ",")

            If Len(Trim(sCabId)) = 0 Then
                sCabId = "0"
            End If

            sFArr = Split(sCabId, ",")
            For i = 0 To UBound(sFArr)
                If Val(sFArr(i)) <> 0 Then
                    GetPermDocTypes = GetPermDocTypes & "," & Val(sFArr(i))
                End If
            Next
            If Left(GetPermDocTypes, 1) = "," Then
                GetPermDocTypes = Right(GetPermDocTypes, Len(GetPermDocTypes) - 1)
            End If
            If Right(GetPermDocTypes, 1) = "," Then
                GetPermDocTypes = Left(GetPermDocTypes, Len(GetPermDocTypes) - 1)
            End If
            Return GetPermDocTypes

        Catch ex As Exception
            Throw
        End Try

    End Function
    Private Function BuildPermDataSet(ByVal iUserId As Integer, ByVal iDtId As Integer, ByVal sMem As String, ByVal sNameSpace As String) As Object
        Dim objParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(3) {}
        Dim iCount As Integer
        ' Dim objDB As New DBLayer.DBGeneral(sConStr, sRDBMS)
        Try
            objParam(iCount) = New OleDb.OleDbParameter("@p_UsrId", OleDb.OleDbType.Numeric)
            objParam(iCount).Value = iUserId
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@p_DtId", OleDb.OleDbType.Numeric)
            objParam(iCount).Value = iDtId
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1


            objParam(iCount) = New OleDb.OleDbParameter("@p_Mem", OleDb.OleDbType.VarChar)
            objParam(iCount).Value = sMem
            objParam(iCount).Direction = ParameterDirection.Input
            iCount = iCount + 1

            objParam(iCount) = New OleDb.OleDbParameter("@p_iRetLvl", OleDb.OleDbType.VarChar)
            objParam(iCount).Value = 0
            objParam(iCount).Direction = ParameterDirection.Output
            objParam(iCount).Size = 1
            'If (ChkType = 2) Then

            Dim arr() As Object = objDBL.SPFrLoadingUsingDsParam(sNameSpace, "GetDTPerDetails", 1, "@p_iRetLvl", objParam)
            If IsDBNull(arr(1)) = False Then
                sPermLvl = arr(1)
            Else
                sPermLvl = ""
            End If
            Return arr(0)

            'Else
            'Return (objDB.SPFrLoadingUsingDs("GetPerDetails", objParam))
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function GetFinalPermForDS(ByVal dtCab As DataTable) As DataTable
        'Dim dtPerm As DataTable
        Dim dr As DataRow
        'Dim sGrp As String = ""
        Dim iCDC, iDDC, iMDC, iIND, iSRH, iMDT As Byte
        Try
            For Each dr In dtCab.Rows
                If (UCase(sPermLvl) <> "GH" And UCase(sPermLvl) <> "PG") Then

                    If (dr("EDP_INDEX") = 1) Then
                        iIND = 1
                    End If
                    If (dr("EDP_SEARCH") = 1) Then
                        iSRH = 1
                    End If
                    If (dr("EDP_MFY_TYPE") = 1) Then
                        iMDT = 1
                    End If
                    If (dr("EDP_MFY_DOCUMENT") = 1) Then
                        iMDC = 1
                    End If
                    If (dr("EDP_DEL_DOCUMENT") = 1) Then
                        iDDC = 1
                    End If
                    If (dr("EDP_OTHER") = 1) Then
                        iCDC = 1
                    End If

                    'sGrp = sGrp & "," & dr("CBP_GrpId")
                End If
            Next
            dtCab.BeginInit()

            dtCab.Rows(0).Item("EDP_INDEX") = iIND
            dtCab.Rows(0).Item("EDP_SEARCH") = iSRH
            dtCab.Rows(0).Item("EDP_MFY_TYPE") = iMDT
            dtCab.Rows(0).Item("EDP_MFY_DOCUMENT") = iMDC
            dtCab.Rows(0).Item("EDP_DEL_DOCUMENT") = iDDC
            dtCab.Rows(0).Item("EDP_OTHER") = iCDC
            dtCab.EndInit()
            dtCab.AcceptChanges()
            Return dtCab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetMemberGroups(ByVal sNameSpace As String, ByVal iUsrId As Integer) As String
        Dim strsql As String
        'Dim objDB As New DBGeneral(sConStr, sRDBMS)
        Dim dr As OleDb.OleDbDataReader
        Try
            strsql = "select SUO_DeptID from Sad_UsersInOtherDept where SUO_Userid = " & iUsrId & ""
            dr = objDBL.SQLDataReader(sNameSpace, strsql)
            strsql = ""
            While (dr.Read)
                strsql = strsql & "," & dr("SUO_DeptID")
            End While
            If (strsql.Length > 0) Then
                strsql = strsql.Remove(0, 1)
            Else
                strsql = 0
            End If
            dr.Close()
            Return strsql
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForGrpHead(ByVal sNameSpace As String, ByVal iGrpId As Int16, ByVal iUsrId As Int16) As Integer
        Dim strsql As String

        Try
            'Dim objDB As New DBGeneral(GetKeyValues("ConnectionString"), GetKeyValues("sRDBMS"))
            strsql = "Select Suo_isDeptHead from Sad_UsersInOtherDept where suo_userId=" & iUsrId & " and suo_deptId=" & iGrpId & ""
            Return objDBL.SQLExecuteScalar(sNameSpace, strsql)

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

