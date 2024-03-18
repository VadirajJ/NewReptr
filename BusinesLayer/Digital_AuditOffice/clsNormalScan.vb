Imports System
Imports System.IO
Imports System.Data
Imports DatabaseLayer
Public Class clsNormalScan
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Function BindNormalScannedImages(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sScanPath As String, ByVal sScanImages As String) As DataTable
        Dim strImageFolderDetails() As String, strImageDetails As String
        Dim dRow As DataRow
        Dim dtDisplay As New DataTable
        Dim sAllImages As String = ""
        Try
            dtDisplay.Columns.Add("FolderName")
            dtDisplay.Columns.Add("TotalPages")
            dtDisplay.Columns.Add("CreatedBy")

            For Each Dir As String In Directory.GetDirectories(sScanPath & sScanImages)
                Dim dirImageFolder As New DirectoryInfo(Dir)
                strImageDetails = dirImageFolder.Name
                strImageFolderDetails = strImageDetails.Split("_")
                dRow = dtDisplay.NewRow
                dRow("FolderName") = strImageDetails
                dRow("TotalPages") = Directory.GetFiles(Dir).Length
                dRow("CreatedBy") = objclsGeneralFunctions.GetUserFullNameFromUserID(sAC, iACID, strImageFolderDetails(1))
                dtDisplay.Rows.Add(dRow)
            Next
            Return dtDisplay
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
