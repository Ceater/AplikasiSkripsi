Imports System
Imports System.IO

Public Class KonversiForm
    '====> Scanning Directory Variable <====
    Dim selectionIndex As Integer = 0
    Dim MaxSelection As Integer = 0 'Maximal mata uang didalam folder
    Dim G1(999) As Bitmap 'Tempat diletakkan hasil split
    Dim G2(999) As Bitmap 'Tempat diletakkan hasil split
    Dim G3(999) As Bitmap 'Tempat diletakkan hasil split
    Dim G4(999) As Bitmap 'Tempat diletakkan hasil split
    Dim HasilKonversi1(104) As String 'Tempat diletakkan hasil konversi
    Dim HasilKonversi2(104) As String 'Tempat diletakkan hasil konversi
    Dim HasilKonversi3(104) As String 'Tempat diletakkan hasil konversi
    Dim HasilKonversi4(104) As String 'Tempat diletakkan hasil konversi
    Public Shared BwConvert As New System.ComponentModel.BackgroundWorker
    Dim progress As String = ""

    Public Sub New()
        InitializeComponent()
        BwConvert.WorkerReportsProgress = True
        BwConvert.WorkerSupportsCancellation = True
        AddHandler BwConvert.DoWork, AddressOf BwConvert_DoWork
        AddHandler BwConvert.ProgressChanged, AddressOf BwConvert_ProgressChanged
        AddHandler BwConvert.RunWorkerCompleted, AddressOf BwConvert_RunWorkerCompleted
    End Sub

    Private Sub ConvertForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGambarMataUang()

        If MaxSelection <= -1 Then
            MsgBox("Tidak ada data hasil pre-processing, silahkan lakukan pre-processing terlebih dahulu")
        End If
    End Sub


    '======Coding Button======
    Private Sub First_Click(sender As Object, e As EventArgs) Handles First.Click
        selectionIndex = 1
        CurrLabel.Text = selectionIndex
        gantipreview(selectionIndex)
    End Sub

    Private Sub PreviousImg_Click(sender As Object, e As EventArgs) Handles PreviousImg.Click
        If selectionIndex <> 1 Then
            selectionIndex -= 1
            CurrLabel.Text = selectionIndex
            gantipreview(selectionIndex)
        End If
    End Sub

    Private Sub NextImg_Click(sender As Object, e As EventArgs) Handles NextImg.Click
        If selectionIndex <> MaxSelection Then
            selectionIndex += 1
            CurrLabel.Text = selectionIndex
            gantipreview(selectionIndex)
        End If
    End Sub

    Private Sub Last_Click(sender As Object, e As EventArgs) Handles Last.Click
        selectionIndex = MaxSelection
        CurrLabel.Text = selectionIndex
        gantipreview(selectionIndex)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not BwConvert.IsBusy Then
            progress = "Mengerjakan" & vbCrLf
            ProgressText.Text = progress
            ProgressText.Focus()
            BwConvert.RunWorkerAsync()
            BwConvert.Dispose()
        End If
    End Sub

    '======Background Worker======
    Private Sub BwConvert_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim count As Integer = 0
        For i = 0 To 14
            HasilKonversi1(i) = StartKonversi(G1(i), "1K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 15 To 29
            HasilKonversi1(i) = StartKonversi(G1(i), "2K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 30 To 44
            HasilKonversi1(i) = StartKonversi(G1(i), "5K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 45 To 59
            HasilKonversi1(i) = StartKonversi(G1(i), "10K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 60 To 74
            HasilKonversi1(i) = StartKonversi(G1(i), "20K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 75 To 89
            HasilKonversi1(i) = StartKonversi(G1(i), "50K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 90 To 104
            HasilKonversi1(i) = StartKonversi(G1(i), "100K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next

        For i = 0 To 14
            HasilKonversi2(i) = StartKonversi(G2(i), "1K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 15 To 29
            HasilKonversi2(i) = StartKonversi(G2(i), "2K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 30 To 44
            HasilKonversi2(i) = StartKonversi(G2(i), "5K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 45 To 59
            HasilKonversi2(i) = StartKonversi(G2(i), "10K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 60 To 74
            HasilKonversi2(i) = StartKonversi(G2(i), "20K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 75 To 89
            HasilKonversi2(i) = StartKonversi(G2(i), "50K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 90 To 104
            HasilKonversi2(i) = StartKonversi(G2(i), "100K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next

        For i = 0 To 14
            HasilKonversi3(i) = StartKonversi(G3(i), "1K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 15 To 29
            HasilKonversi3(i) = StartKonversi(G3(i), "2K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 30 To 44
            HasilKonversi3(i) = StartKonversi(G3(i), "5K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 45 To 59
            HasilKonversi3(i) = StartKonversi(G3(i), "10K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 60 To 74
            HasilKonversi3(i) = StartKonversi(G3(i), "20K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 75 To 89
            HasilKonversi3(i) = StartKonversi(G3(i), "50K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 90 To 104
            HasilKonversi3(i) = StartKonversi(G3(i), "100K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next

        For i = 0 To 14
            HasilKonversi4(i) = StartKonversi(G4(i), "1K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 15 To 29
            HasilKonversi4(i) = StartKonversi(G4(i), "2K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 30 To 44
            HasilKonversi4(i) = StartKonversi(G4(i), "5K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 45 To 59
            HasilKonversi4(i) = StartKonversi(G4(i), "10K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 60 To 74
            HasilKonversi4(i) = StartKonversi(G4(i), "20K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 75 To 89
            HasilKonversi4(i) = StartKonversi(G4(i), "50K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        For i = 90 To 104
            HasilKonversi4(i) = StartKonversi(G4(i), "100K", 0)
            count += 1
            BwConvert.ReportProgress(0, count)
        Next
        SaveKonversi(HasilKonversi1, "1")
        SaveKonversi(HasilKonversi2, "2")
        SaveKonversi(HasilKonversi3, "3")
        SaveKonversi(HasilKonversi4, "4")
    End Sub

    Private Sub BwConvert_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        Dim x As String = ""
        x = DirectCast(e.UserState, Integer) & " dari 420"
        ProgressText.Text = progress & x
    End Sub

    Private Sub BwConvert_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        ProgressText.Text &= vbCrLf & "Selesai"
    End Sub

    '======Function / Procedure======
    Sub gantipreview(idx As Integer)
        Preview_1.Image = G1(idx - 1)
        Preview_2.Image = G2(idx - 1)
        Preview_3.Image = G3(idx - 1)
        Preview_4.Image = G4(idx - 1)
        If idx = 0 Then
            idx = 1
        End If
    End Sub

    Sub LoadGambarMataUang() 'Menyimpan semua mata uang dan jumlah pernominal kedalam variable
        Dim tmppath As String = MainPath
        selectionIndex = 0
        tmppath = MainPath & "Output\Split\"
        Dim orderedFiles = New DirectoryInfo(tmppath).GetFiles().OrderBy(Function(x) x.CreationTime)
        Dim a, b, c, d As Integer
        For Each f As System.IO.FileInfo In orderedFiles
            If Split(f.Name, " - ")(1) = "1.png" Then
                G1(a) = New Bitmap(tmppath & f.Name)
                a += 1
            ElseIf Split(f.Name, " - ")(1) = "2.png" Then
                G2(b) = New Bitmap(tmppath & f.Name)
                b += 1
            ElseIf Split(f.Name, " - ")(1) = "3.png" Then
                G3(c) = New Bitmap(tmppath & f.Name)
                c += 1
            ElseIf Split(f.Name, " - ")(1) = "4.png" Then
                G4(d) = New Bitmap(tmppath & f.Name)
                d += 1
            End If
        Next
        MaxSelection = a - 1
        selectionIndex = 1
        MaxLabel.Text = a - 1
        CurrLabel.Text = selectionIndex
        Preview_1.Image = G1(0)
        Preview_2.Image = G2(0)
        Preview_3.Image = G3(0)
        Preview_4.Image = G4(0)
    End Sub

    Sub SaveOption(x1 As Integer, x2 As Integer, x3 As Integer, x4 As Integer, x5 As Integer, x6 As Integer, x7 As Integer)
        Dim objWriter As System.IO.StreamWriter
        Dim fs As FileStream
        System.IO.File.Delete(SettingPath)
        fs = File.Create(SettingPath)
        fs.Dispose()

        Dim line As String = ""
        line = x1 & " " & x2 & " " & x3 & " " & x4 & " " & x5 & " " & x6 & " " & x7
        objWriter = New System.IO.StreamWriter(SettingPath)
        objWriter.Write(line)
        objWriter.Close()
        objWriter.Dispose()
    End Sub
End Class