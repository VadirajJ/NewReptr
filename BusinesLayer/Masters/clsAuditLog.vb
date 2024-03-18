Imports System
Imports DatabaseLayer
Imports System.Data
Public Class clsAuditLog
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    '    Public Function LoadAuditLogDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sFormName As String, ByVal sFormFullName As String, ByVal iUserID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable
    '        Dim sSql As String
    '        Dim ds As New DataSet
    '        Dim dt As New DataTable
    '        Dim dr As DataRow
    '        Dim i As Integer
    '        Dim fromdate, ToDate As Date

    '        Try
    '            fromdate = Format(dFromDate, "yyyy-MM-dd")
    '            ToDate = Format(dToDate, "yyyy-MM-dd")
    '            dt.Columns.Add("SrNo")
    '            dt.Columns.Add("ModuleOperation")
    '            dt.Columns.Add("Activity")
    '            dt.Columns.Add("User")
    '            dt.Columns.Add("Date")
    '            sSql = "Select ALFO_PKID,ALFO_UserID,ALFO_Date,ALFO_Module,ALFO_Form,ALFO_CompID,ALFO_Event,Usr_FullName From Audit_Log_Form_Operations "
    '            sSql = sSql & " Left Join Sad_userDetails On Usr_ID=ALFO_UserID And Usr_CompID=" & iAcID & ""
    '            sSql = sSql & " Where ALFO_CompID=" & iAcID & "  "
    '            If sFormName <> "" Then
    '                sSql = sSql & " And ALFO_Form ='" & sFormName & "' "
    '            End If
    '            If iUserID <> 0 Then
    '                sSql = sSql & " And ALFO_UserID =" & iUserID & " "
    '            End If
    '            If fromdate <> Nothing And ToDate <> Nothing Then
    '                sSql = sSql & " And  ALFO_Date between  " & objclsGRACeGeneral.FormatDtForRDBMS(fromdate, "Q") & "  And " & objclsGRACeGeneral.FormatDtForRDBMS(ToDate, "Q") & "   "
    '                GoTo Operation
    '            End If
    '            If fromdate <> "" Then
    '                sSql = sSql & " And  ALFO_Date >=   " & objclsGRACeGeneral.FormatDtForRDBMS(fromdate, "Q") & "   "
    '            End If
    '            If ToDate <> "" Then
    '                sSql = sSql & " And  ALFO_Date <=   " & objclsGRACeGeneral.FormatDtForRDBMS(ToDate, "Q") & "   "
    '            End If

    'Operation:  sSql = sSql & " Order By  ALFO_PKID desc"
    '            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
    '            For i = 0 To ds.Tables(0).Rows.Count - 1
    '                dr = dt.NewRow()
    '                dr("SrNo") = i + 1
    '                If IsDBNull(ds.Tables(0).Rows(i)("ALFO_Event")) = False Then
    '                    dr("ModuleOperation") = sFormFullName
    '                End If
    '                If IsDBNull(ds.Tables(0).Rows(i)("ALFO_Event")) = False Then
    '                    If ds.Tables(0).Rows(i)("ALFO_Event") = "Saved" Then
    '                        dr("Activity") = "Creation"
    '                    ElseIf ds.Tables(0).Rows(i)("ALFO_Event") = "PDF" Then
    '                        dr("Activity") = "Report Generated-PDF"
    '                    ElseIf ds.Tables(0).Rows(i)("ALFO_Event") = "Excel" Then
    '                        dr("Activity") = "Report Generated-Excel"
    '                    Else
    '                        dr("Activity") = ds.Tables(0).Rows(i)("ALFO_Event")
    '                    End If
    '                End If
    '                dr("User") = ds.Tables(0).Rows(i)("Usr_FullName")
    '                dr("Date") = ds.Tables(0).Rows(i)("ALFO_Date")
    '                dt.Rows.Add(dr)
    '            Next
    '            Return dt
    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    End Function
    Public Function LoadAuditLogDetails(ByVal sAc As String, ByVal iAcID As Integer, ByVal sFormName As String, ByVal sFormFullName As String, ByVal iUserID As Integer, ByVal dFromDate As Date, ByVal dToDate As Date) As DataTable
        Dim sSql As String
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim i As Integer
        Dim fromdate, ToDate As Date

        Try
            fromdate = Format(dFromDate, "yyyy-MM-dd")
            ToDate = Format(dToDate, "yyyy-MM-dd")
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ModuleOperation")
            dt.Columns.Add("Activity")
            dt.Columns.Add("User")
            dt.Columns.Add("Date")
            dt.Columns.Add("AssignmentNo")
            dt.Columns.Add("TaskName")
            dt.Columns.Add("Customer")
            dt.Columns.Add("Partner")

            sSql = "Select ALFO_PKID,ALFO_UserID,ALFO_Date,ALFO_Module,ALFO_Form,ALFO_CompID,ALFO_Event,Usr_FullName, "
            sSql = sSql & "C.AAS_AssignmentNo as AssignmentNo, cmm_Desc as TaskName, E.CUST_Name as Customer, Usr_FullName as Partner "
            sSql = sSql & "From Audit_Log_Form_Operations A  "
            sSql = sSql & "Left Join Sad_userDetails On Usr_ID=ALFO_UserID And Usr_CompID=" & iAcID & " "
            sSql = sSql & "Left join AuditAssignment_Schedule C on C.AAS_ID = A.ALFO_MasterID "
            sSql = sSql & "Left join Content_Management_Master D on D.cmm_ID = C.AAS_TaskID "
            sSql = sSql & "Left join SAD_CUSTOMER_MASTER E on E.CUST_ID = C.AAS_CustID "
            sSql = sSql & " Where ALFO_CompID=" & iAcID & "  "
            If sFormName <> "" Then
                If (sFormName = "Schedule") Then
                    sSql = sSql & " And ALFO_Form ='Schedule Assignments' "
                Else
                    sSql = sSql & " And ALFO_Form ='" & sFormName & "' "
                End If
            End If
            If iUserID <> 0 Then
                sSql = sSql & " And ALFO_UserID =" & iUserID & " "
            End If
            If fromdate <> Nothing And ToDate <> Nothing Then
                sSql = sSql & " And  ALFO_Date between  " & objclsGRACeGeneral.FormatDtForRDBMS(fromdate, "Q") & "  And " & objclsGRACeGeneral.FormatDtForRDBMS(ToDate, "Q") & "   "
                GoTo Operation
            End If
            If fromdate <> "" Then
                sSql = sSql & " And  ALFO_Date >=   " & objclsGRACeGeneral.FormatDtForRDBMS(fromdate, "Q") & "   "
            End If
            If ToDate <> "" Then
                sSql = sSql & " And  ALFO_Date <=   " & objclsGRACeGeneral.FormatDtForRDBMS(ToDate, "Q") & "   "
            End If

Operation:  sSql = sSql & " Order By  ALFO_PKID desc"
            ds = objDBL.SQLExecuteDataSet(sAc, sSql)
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = dt.NewRow()
                dr("SrNo") = i + 1
                If IsDBNull(ds.Tables(0).Rows(i)("ALFO_Event")) = False Then
                    dr("ModuleOperation") = sFormFullName
                End If
                If IsDBNull(ds.Tables(0).Rows(i)("ALFO_Event")) = False Then
                    If ds.Tables(0).Rows(i)("ALFO_Event") = "Saved" Then
                        dr("Activity") = "Creation"
                    ElseIf ds.Tables(0).Rows(i)("ALFO_Event") = "PDF" Then
                        dr("Activity") = "Report Generated-PDF"
                    ElseIf ds.Tables(0).Rows(i)("ALFO_Event") = "Excel" Then
                        dr("Activity") = "Report Generated-Excel"
                    Else
                        dr("Activity") = ds.Tables(0).Rows(i)("ALFO_Event")
                    End If
                End If
                dr("User") = ds.Tables(0).Rows(i)("Usr_FullName")
                dr("Date") = ds.Tables(0).Rows(i)("ALFO_Date")
                dr("AssignmentNo") = ds.Tables(0).Rows(i)("AssignmentNo")
                dr("TaskName") = ds.Tables(0).Rows(i)("TaskName")
                dr("Customer") = ds.Tables(0).Rows(i)("Customer")
                dr("Partner") = ds.Tables(0).Rows(i)("Partner")
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
