Imports System.IO
Imports System.Net
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Configuration
Module LotusNotes


    Dim docobj As Object


    Sub Main()
        Dim cls As New CConnection
        Dim qry As String
        Dim remark As String = ""
        Dim dt As New DataTable
        Dim periode As String



        qry = ConfigurationSettings.AppSettings("query").ToString

        dt = cls.pb_cust_executeQuery(qry)
        For i As Integer = 0 To dt.Rows.Count - 1
            pv_cust_download(dt.Rows(i).Item("file_name").ToString.ToString, dt.Rows(i).Item("nrp").ToString.ToString, dt.Rows(i).Item("periode").ToString.ToString)
        Next
        ' pv_cust_getList()

    End Sub

    Private Sub pv_cust_download(file_name As String, nrp As String, periode As String)
        Try
            Dim patch_download As String = ConfigurationSettings.AppSettings("patch_download").ToString
            'patch_download = patch_download & "\" & periode

            Dim report As String = "http://10.2.183.120/ReportServer/Pages/ReportViewer.aspx?%2fIPO%2fRpt_Insentif_Produksi_FPTNew&INSENTIF_MONTHLY_PID=" & periode & "&rs:Command=Render&rs%3AFormat=PDF"
            Dim client As New WebClient()
            client.UseDefaultCredentials = True
            client.DownloadFile(New Uri(report.ToString), patch_download & "\" & file_name & ".pdf")
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

End Module
