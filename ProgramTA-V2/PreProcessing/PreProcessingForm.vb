Imports System
Imports System.IO

Public Class PreProcessingForm
    '====> Scanning Directory Variable <====
    Dim selectionIndex As Integer = 0
    Dim MaxSelection As Integer = 0 'Maximal mata uang didalam folder
    Dim isiHasil As Integer = 0 'Maximal mata uang didalam folder
    Dim GAsli(999) As Bitmap 'Tempat untuk memasukkan banyak data gambar 1 mata uang sebelum diolah
    Dim GPre(999) As Bitmap 'Tempat untuk memasukkan banyak data gambar 1 mata uang setelah preprosesing
    Dim GSplit(999) As Bitmap 'Tempat untuk memasukkan banyak data gambar 1 mata uang setelah split
    '====> PreProcessing Variable <====
    Dim ArrCountGbr As New ArrayList 'Menyimpan banyak mata uang untuk setiap nominal
    Dim ArrGambar As New ArrayList 'Menyimpan semua gambar asli
    Public Shared BwPreProcessing As New System.ComponentModel.BackgroundWorker
    '====> General Variable <====
    Dim Progress As String = ""

    Public Sub New()
        InitializeComponent()
        BwPreProcessing.WorkerReportsProgress = True
        BwPreProcessing.WorkerSupportsCancellation = True
        AddHandler BwPreProcessing.DoWork, AddressOf BwPreProcessing_DoWork
        AddHandler BwPreProcessing.ProgressChanged, AddressOf BwPreProcessing_ProgressChanged
        AddHandler BwPreProcessing.RunWorkerCompleted, AddressOf BwPreProcessing_RunWorkerCompleted
    End Sub

    Private Sub PreProcessingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadGambarMataUang()
    End Sub

    Private Sub PreProcessingForm_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        ClearContent()
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
        If selectionIndex <> MaxSelection + 1 Then
            selectionIndex += 1
            CurrLabel.Text = selectionIndex
            gantipreview(selectionIndex)
        End If
    End Sub

    Private Sub Last_Click(sender As Object, e As EventArgs) Handles Last.Click
        selectionIndex = MaxSelection + 1
        CurrLabel.Text = selectionIndex
        gantipreview(selectionIndex)
    End Sub

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        'MsgBox(isiHasil)
        Try
            ClearContent()
            ClearFolder()
        Catch ex As Exception
            ClearContent()
        End Try
        ClearFolder()
        If Not BwPreProcessing.IsBusy Then
            Progress = "Mengerjakan" & vbCrLf
            ProgressText.Text = Progress
            ProgressText.Focus()
            BwPreProcessing.RunWorkerAsync()
            BwPreProcessing.Dispose()
        End If
    End Sub


    '======Ganti Preview======
    Sub gantipreview(idx As Integer)
        Preview.Image = GAsli(idx - 1)
        If idx = 0 Then
            idx = 1
        End If
        Pre1.Image = GPre((idx - 1) * 4)
        Pre2.Image = GPre((idx - 1) * 4 + 1)
        Pre3.Image = GPre((idx - 1) * 4 + 2)
        Pre4.Image = GPre((idx - 1) * 4 + 3)
        Split1.Image = GSplit((idx - 1) * 4)
        Split2.Image = GSplit((idx - 1) * 4 + 1)
        Split3.Image = GSplit((idx - 1) * 4 + 2)
        Split4.Image = GSplit((idx - 1) * 4 + 3)
    End Sub

    Sub LoadGambarMataUang() 'Menyimpan semua mata uang dan jumlah pernominal kedalam variable
        Dim tmppath As String = MainPath
        Dim di As DirectoryInfo
        Dim fiArr As FileInfo()
        Dim fri As FileInfo

        selectionIndex = 0
        tmppath = MainPath & "DataUang\"
        di = New DirectoryInfo(tmppath)
        fiArr = di.GetFiles()
        MaxSelection += fiArr.Count - 1
        For Each fri In fiArr
            GAsli(selectionIndex) = New Bitmap(tmppath & fri.Name)
            selectionIndex += 1
        Next fri

        selectionIndex = 0
        tmppath = MainPath & "Output\Preprocessing\"
        Dim orderedFiles = New DirectoryInfo(tmppath).GetFiles().OrderBy(Function(x) x.CreationTime)
        isiHasil += orderedFiles.Count - 1
        For Each f As System.IO.FileInfo In orderedFiles
            GPre(selectionIndex) = New Bitmap(tmppath & f.Name)
            selectionIndex += 1
        Next

        selectionIndex = 0
        tmppath = MainPath & "Output\Split\"
        orderedFiles = New DirectoryInfo(tmppath).GetFiles().OrderBy(Function(x) x.CreationTime)
        For Each f As System.IO.FileInfo In orderedFiles
            GSplit(selectionIndex) = New Bitmap(tmppath & f.Name)
            selectionIndex += 1
        Next

        selectionIndex = 1
        MaxLabel.Text = MaxSelection + 1
        CurrLabel.Text = selectionIndex
        JFotoLabel.Text = MaxLabel.Text
        HPreLabel.Text = CInt(MaxLabel.Text) * 4
        HSplitLabel.Text = CInt(MaxLabel.Text) * 4
        Preview.Image = GAsli(0)
        Pre1.Image = GPre(0)
        Pre2.Image = GPre(1)
        Pre3.Image = GPre(2)
        Pre4.Image = GPre(3)
        Split1.Image = GSplit(0)
        Split2.Image = GSplit(1)
        Split3.Image = GSplit(2)
        Split4.Image = GSplit(3)
    End Sub

    Sub ReLoadGambarMataUang() 'Menyimpan semua mata uang dan jumlah pernominal kedalam variable
        Dim tmppath As String = MainPath

        selectionIndex = 0
        tmppath = MainPath & "Output\Preprocessing\"
        Dim orderedFiles = New DirectoryInfo(tmppath).GetFiles().OrderBy(Function(x) x.CreationTime)
        For Each f As System.IO.FileInfo In orderedFiles
            GPre(selectionIndex) = New Bitmap(tmppath & f.Name)
            selectionIndex += 1
        Next

        selectionIndex = 0
        tmppath = MainPath & "Output\Split\"
        orderedFiles = New DirectoryInfo(tmppath).GetFiles().OrderBy(Function(x) x.CreationTime)
        For Each f As System.IO.FileInfo In orderedFiles
            GSplit(selectionIndex) = New Bitmap(tmppath & f.Name)
            selectionIndex += 1
        Next

        selectionIndex = 1
        Preview.Image = GAsli(0)
        Pre1.Image = GPre(0)
        Pre2.Image = GPre(1)
        Pre3.Image = GPre(2)
        Pre4.Image = GPre(3)
        Split1.Image = GSplit(0)
        Split2.Image = GSplit(1)
        Split3.Image = GSplit(2)
        Split4.Image = GSplit(3)
    End Sub

    '======Background Worker======
    Private Sub BwPreProcessing_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        'MsgBox(MaxSelection)
        For i = 0 To MaxSelection
            StartPreprocessing(GAsli(i), i)
            BwPreProcessing.ReportProgress(0, i + 1)
        Next
    End Sub

    Private Sub BwPreProcessing_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        Dim x As String = ""
        x = DirectCast(e.UserState, Integer) & " dari 105"
        ProgressText.Text = Progress & x
    End Sub

    Private Sub BwPreProcessing_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        ReLoadGambarMataUang()
        ProgressText.Text &= vbCrLf & "Selesai"
    End Sub


    '======Function / Procedure======
    Sub ClearContent()
        For i = 0 To isiHasil
            GPre(i).Dispose()
            GSplit(i).Dispose()
        Next
        Pre1.Image = Nothing
        Pre2.Image = Nothing
        Pre3.Image = Nothing
        Pre4.Image = Nothing
        Split1.Image = Nothing
        Split2.Image = Nothing
        Split3.Image = Nothing
        Split4.Image = Nothing
    End Sub
End Class