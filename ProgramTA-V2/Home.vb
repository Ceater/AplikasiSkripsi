Imports System.IO

Public Class Home
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim f As New PreProcessingForm
        f.MdiParent = Me
        f.StartPosition = FormStartPosition.CenterScreen
        f.Show()
    End Sub

    Private Sub Home_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MainPath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName).FullName).FullName & "\"
        SettingPath = MainPath & "SettingMUang.txt"
        Konversi1Path = MainPath & "Output\Konversi1.txt"
        Konversi2Path = MainPath & "Output\Konversi2.txt"
        Konversi3Path = MainPath & "Output\Konversi3.txt"
        Konversi4Path = MainPath & "Output\Konversi4.txt"
        W1 = MainPath & "Output\w1.txt"
        W2 = MainPath & "Output\w2.txt"
        W3 = MainPath & "Output\w3.txt"
        W4 = MainPath & "Output\w4.txt"

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim result As Integer = MessageBox.Show("Yakin ingin menghapus data?", "Penghapusan Data", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            ClearFolder()
            MsgBox("Penghapusan Folder Selesai")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim f As New KonversiForm
        f.MdiParent = Me
        f.StartPosition = FormStartPosition.CenterScreen
        f.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim f As New TrainingForm
        f.MdiParent = Me
        f.StartPosition = FormStartPosition.CenterScreen
        f.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim f As New TestingForm
        f.MdiParent = Me
        f.StartPosition = FormStartPosition.CenterScreen
        f.Show()
    End Sub
End Class
