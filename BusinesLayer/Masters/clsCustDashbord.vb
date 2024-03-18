Imports System
Imports System.Data
Imports DatabaseLayer

Public Class clsCustDashbord
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function LoadCustomerDetails(ByVal sAC As String, Optional ByVal sCustName As String = "", Optional ByVal sAbbreviation As String = "", Optional ByVal sCity As String = "")
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from  SAD_CUSTOMER_MASTER Where Cust_Delflg in ('X','D')"
            If sCustName <> String.Empty Then
                sSql = sSql & " And Cust_Name like '%" & sCustName & "%'"
            End If
            If sAbbreviation <> String.Empty Then
                sSql = sSql & " And Cust_Code like '%" & sAbbreviation & "%'"
            End If
            If sCity <> String.Empty Then
                sSql = sSql & " And Cust_Comm_City like '%" & sCity & "%'"
            End If
            sSql = sSql & " Order by Cust_Name"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCount(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select count(CUST_ID) From SAD_CUSTOMER_MASTER Where CUST_CompID=" & iACID & " and CUST_DELFLG<>'D'"
            GetCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return GetCount
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ApproveCustomerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iCustID As Integer, ByVal sFlag As String, ByVal sIPAddress As String)
        Dim sSql As String = ""
        Try
            sSql = "Update SAD_CUSTOMER_MASTER set "
            If sFlag = "W" Then
                sSql = sSql & "Cust_Status='A',CUST_Delflg='A',Cust_ApprovedBy=" & iUserID & ",Cust_ApprovedOn=Getdate(),"
            ElseIf sFlag = "D" Then
                sSql = sSql & "Cust_Status='AD',CUST_Delflg='D',CUST_DeletedBy=" & iUserID & ",CUST_DeletedOn=Getdate(),"
            ElseIf sFlag = "A" Then
                sSql = sSql & "Cust_Status='AR',CUST_Delflg='A',CUST_RecallBy=" & iUserID & ",CUST_RecallOn=Getdate(),"
            End If
            sSql = sSql & " CUST_IPAddress='" & sIPAddress & "' where Cust_ID=" & iCustID & " and CUST_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function BindCustomerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer)
        Dim dt As New DataTable, dtCust As New DataTable, dtServie As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        'Dim sService As String
        Dim sSql As String = ""
        Try
            dtCust.Columns.Add("SrNo")
            dtCust.Columns.Add("CustID")
            dtCust.Columns.Add("CustomerName")
            dtCust.Columns.Add("ServicesOffered")
            dtCust.Columns.Add("City")
            dtCust.Columns.Add("Status")

            sSql = "Select * from  SAD_CUSTOMER_MASTER where CUST_CompID=" & iACID & "  Order by Cust_Name"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    drow = dtCust.NewRow
                    drow("SrNo") = i + 1

                    Try
                        If IsDBNull(dt.Rows(i)("Cust_Id")) = False Then
                            drow("CustID") = dt.Rows(i)("Cust_Id")
                            'dtServie = BindServicesOfferDetails(sAC, iACID, drow("CustID"), iYearID)
                            drow("ServicesOffered") = BindAllServicesOfferDetails(sAC, iACID, drow("CustID"), dt.Rows(i)("CUST_TASKS"))
                        End If
                    Catch ex As Exception

                    End Try

                    If IsDBNull(dt.Rows(i)("Cust_Name")) = False Then
                        drow("CustomerName") = dt.Rows(i)("Cust_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("Cust_Comm_City")) = False Then
                        drow("City") = dt.Rows(i)("Cust_Comm_City")
                    End If
                    If IsDBNull(dt.Rows(i)("CUST_Delflg")) = False Then
                        If dt.Rows(i)("CUST_Delflg") = "A" Then
                            drow("Status") = "Activated"
                        ElseIf dt.Rows(i)("CUST_Delflg") = "D" Then
                            drow("Status") = "De-Activated"
                        ElseIf dt.Rows(i)("CUST_Delflg") = "W" Then
                            drow("Status") = "Waiting for Approval"
                        End If
                    End If
                    dtCust.Rows.Add(drow)
                Next
            End If
            Return dtCust
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function BindServicesOfferDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim sServiceType As String = ""
        Try
            sSql = "Select Distinct LOE_CustomerId,a.CMM_Desc as ServiceType From SAD_CUST_LOE Left Join Content_Management_Master a On a.CMM_ID=LOE_ServiceTypeId "
            sSql = sSql & " Where LOE_YearId=" & iYearID & " And LOE_CustomerId=" & iCustID & " Order by LOE_CustomerID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Function BindAllServicesOfferDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal CUST_TASKS As String) As String
    '    Dim sSql As String = ""
    '    Dim dt As New DataTable
    '    Dim sServiceType As String = ""
    '    Try
    '        If CUST_TASKS.StartsWith(",") Then
    '            CUST_TASKS = CUST_TASKS.Remove(0, 1)
    '        End If
    '        If CUST_TASKS.EndsWith(",") Then
    '            CUST_TASKS = CUST_TASKS.Remove(Len(CUST_TASKS) - 1, 1)
    '        End If
    '        sSql = "Select STUFF((SELECT DISTINCT ', '+ CAST(CMM_Desc AS VARCHAR(MAX)) FROM Content_Management_Master,SAD_Compliance_Details WHERE (CMM_ID=Comp_Task "
    '        If CUST_TASKS <> "" Then
    '            sSql = sSql & " Or CMM_ID in (" & CUST_TASKS & ")"
    '        End If
    '        sSql = sSql & " ) And Comp_CustID=" & iCustID & " FOR XMl PATH('')),1,1,'')"
    '        If IsDBNull(objDBL.SQLExecuteScalar(sAC, sSql)) = False Then
    '            Return objDBL.SQLExecuteScalar(sAC, sSql)
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function BindAllServicesOfferDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal CUST_TASKS As String) As String
        Try
            CUST_TASKS = CUST_TASKS.Trim(","c)

            Dim sSql As String = "SELECT STRING_AGG(CMM_Desc, ', ') AS CombinedTaskData FROM ("
            sSql = sSql & " SELECT CMM_Desc FROM Content_Management_Master, SAD_Compliance_Details"
            sSql = sSql & " WHERE CMM_ID = Comp_Task AND Comp_CustID = " & iCustID & ""
            If Not String.IsNullOrEmpty(CUST_TASKS) Then
                sSql = sSql & " UNION SELECT CMM_Desc FROM Content_Management_Master WHERE CMM_ID IN (" & CUST_TASKS & ")"
            End If
            sSql = sSql & ") AS SQLQuery"

            Dim result As Object = objDBL.SQLExecuteScalar(sAC, sSql.ToString())
            Return If(Not IsDBNull(result), result.ToString(), "")

        Catch ex As Exception
            Dim sSql As String = "SELECT STUFF((SELECT DISTINCT ', ' + CAST(CMM_Desc AS VARCHAR(MAX)) FROM Content_Management_Master WHERE CMM_ID IN (" & CUST_TASKS & ") FOR XML PATH('')), 1, 2, '')"
            Dim result As Object = objDBL.SQLExecuteScalar(sAC, sSql)
            Return If(Not IsDBNull(result), result.ToString(), "")
        End Try
    End Function
End Class
