Imports DatabaseLayer
Namespace Components
    Public Class ErrorClass
        Private objDBL As New DatabaseLayer.DBHelper
        Public Function GetErrorMessages(ByVal sAccessCode As String, ByVal sError As String) As String
            Dim sSql As String
            Dim dt As New DataTable
            Dim i As Integer
            Try
                sSql = "Select TER_RunTimeError,TER_ErrorReplacemnet From TRACe_Error_Replacement Where TER_Status='A'"
                dt = objDBL.SQLExecuteDataTable(sAccessCode, sSql)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If sError.Contains(dt.Rows(i)("TER_RunTimeError")) = True Then
                            Return dt.Rows(i)("TER_ErrorReplacemnet")
                        End If
                    Next
                    Return "We apologize for any inconvenience this may cause."
                Else
                    Return sError
                End If
            Catch ex As Exception
                Return "We apologize for any inconvenience this may cause."
            End Try
        End Function
    End Class
End Namespace



'CreatedBy
'CreatedOn
'UpdatedBy
'UpdatedOn
'ApprovedBy
'ApprovedOn
'DeletedBy
'DeletedOn
'RecallBy
'RecallOn
'Flag
'Status

'Flag   
'       ---> W - Waiting For Approval'  
'       ---> A - Approved - A, AR' 
'       ---> X - Waiting For Approval(After Delete) - D
'       ---> D - Deleted - AD
'       ---> Y - Waiting For Approval(After Recall) - R

'All Forms
'======
'Status ---> C - Created
'           ---> U - Updated
'           ---> A - Approved
'           ---> D - Deleted
'           ---> AD - Approved(After Deleted)
'           ---> R - Recalled
'           ---> AR - Approved(After Recalled)


'Applicable for Login & User Creation
'           ---> B - Block
'           ---> L - Lock
'           ---> Z - Disable
'           ---> E - Enable
'           ---> UB - UnBlock
'           ---> UL - UnLock
'           --->P - Password Updated


'----------------------------------------------------------------------------
' User Details 
'----------------------------------------------------------------------------
'Usr_Designation = ddlDesignation (* Designation)
'usr_CompanyId = ddlCompanyName(* Company Name)
'Usr_GrpOrUserLvlPerm = ddlPermission(*Permission)
'Usr_LevelGrp = ddlGroup (* Module)
'Usr_Node =  Organisation Level(Zone,Region,Area,Branch)(1,2,3,4)
'Usr_OrgnID = (Zone,Region,Area,Branch)
'Usr_Role = ddlRole(*Role)