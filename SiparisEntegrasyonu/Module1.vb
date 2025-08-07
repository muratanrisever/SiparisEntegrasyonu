Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json

Module Module1

    Sub Main()
        Console.WriteLine("Sipariş entegrasyonu başlatıldı...")

        Dim client As New HttpClient()
        client.BaseAddress = New Uri("https://localhost:7283/") ' API adresi
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        Dim response = client.GetAsync("api/tumSiparisler").Result

        If response.IsSuccessStatusCode Then
            Dim json = response.Content.ReadAsStringAsync().Result
            Dim siparisler As List(Of SiparisDto) = JsonConvert.DeserializeObject(Of List(Of SiparisDto))(json)

            For Each siparis In siparisler
                If Not SiparisVarMi(siparis.Id) Then
                    SiparisEkle(siparis)
                    Console.WriteLine($"Yeni sipariş eklendi: {siparis.AdiSoyadi}")
                End If
            Next
        Else
            Console.WriteLine("API'den sipariş çekilemedi.")
        End If

        Console.WriteLine("İşlem tamamlandı.")
    End Sub

    Function SiparisVarMi(id As Integer) As Boolean
        Dim cs = ConfigurationManager.ConnectionStrings("MyDbConnection").ConnectionString
        Using conn As New SqlConnection(cs)
            conn.Open()
            Dim cmd As New SqlCommand("SELECT COUNT(*) FROM Siparisler WHERE Id = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim count = Convert.ToInt32(cmd.ExecuteScalar())
            Return count > 0
        End Using
    End Function

    Sub SiparisEkle(siparis As SiparisDto)
        Dim cs = ConfigurationManager.ConnectionStrings("MyDbConnection").ConnectionString
        Using conn As New SqlConnection(cs)
            conn.Open()
            Dim cmd As New SqlCommand("INSERT INTO Siparisler (Id, AdiSoyadi, Urun, Adet, Tarih) VALUES (@id, @adi, @urun, @adet, @tarih)", conn)
            cmd.Parameters.AddWithValue("@id", siparis.Id)
            cmd.Parameters.AddWithValue("@adi", siparis.AdiSoyadi)
            cmd.Parameters.AddWithValue("@urun", siparis.Urun)
            cmd.Parameters.AddWithValue("@adet", siparis.Adet)
            'cmd.Parameters.AddWithValue("@tarih", siparis.Tarih)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Class SiparisDto
        Public Property Id As Integer
        Public Property AdiSoyadi As String
        Public Property Urun As String
        Public Property Adet As Integer
        Public Property Tarih As DateTime
    End Class

End Module
