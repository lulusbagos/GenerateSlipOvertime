Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Configuration

Public Class CConnection
    Private pv_i_conn As SqlConnection
    Private pv_i_cmd As SqlCommand

    Public Sub New()
        Me.pv_cust_setConnection()
    End Sub

    Private Sub pv_cust_setConnection()
        Dim connectionstring As String = ConfigurationSettings.AppSettings("PERSISConstring").ToString
        pv_i_conn = New SqlConnection(connectionstring)
    End Sub

    Public Function pb_cust_getConnection() As SqlConnection
        Return pv_i_conn
    End Function

    Public Sub pb_cust_closeConnection()
        Me.pv_i_conn.Close()
    End Sub

    Public Function pb_cust_executeNonQuery(ByVal s_str_query As String, ByRef s_str_remark As String) As Boolean
        Dim i_bol_return As Boolean = False
        Try
            pv_i_conn.Open()
            pv_i_cmd = New SqlCommand(s_str_query, pv_i_conn)
            pv_i_cmd.ExecuteNonQuery()
            pv_i_conn.Close()
            pv_i_cmd.Dispose()
            i_bol_return = True
            s_str_remark = "Query Executed"
        Catch ex As Exception
            i_bol_return = False
            s_str_remark = ex.Message.ToString
        End Try
        Return True
    End Function

    Public Function pb_cust_executeQuery(ByVal s_str_query As String) As DataTable
        Dim i_dtab_return As New DataTable
        Dim i_tdap As New SqlDataAdapter

        pv_i_conn.Open()
        pv_i_cmd = New SqlCommand(s_str_query, pv_i_conn)
        i_tdap.SelectCommand = pv_i_cmd
        i_tdap.Fill(i_dtab_return)

        pv_i_conn.Close()
        pv_i_cmd.Dispose()

        Return i_dtab_return
    End Function

    Public Function pb_cust_executeScalar(ByVal s_str_query As String) As String
        Dim i_str_return As String

        pv_i_conn.Open()
        pv_i_cmd = New SqlCommand(s_str_query, pv_i_conn)

        If IsDBNull(pv_i_cmd.ExecuteScalar()) Then
            i_str_return = "0"
        Else
            i_str_return = pv_i_cmd.ExecuteScalar()
        End If

        pv_i_conn.Close()
        pv_i_cmd.Dispose()

        Return i_str_return
    End Function
End Class
