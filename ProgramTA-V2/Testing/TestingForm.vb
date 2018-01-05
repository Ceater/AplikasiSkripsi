Public Class TestingForm
    Public Progress As String
    Dim Gambar() As Bitmap
    Dim Hasil()() As Double
    Dim weight1() As Double
    Dim weight2() As Double
    Dim weight3() As Double
    Dim weight4() As Double
    Friend Shared MyInstance As TestingForm
    Public Shared BwTest As New System.ComponentModel.BackgroundWorker

    Public Sub New()
        InitializeComponent()
        BwTest.WorkerReportsProgress = True
        BwTest.WorkerSupportsCancellation = True
        AddHandler BwTest.DoWork, AddressOf BwTest_DoWork
        AddHandler BwTest.ProgressChanged, AddressOf BwTest_ProgressChanged
        AddHandler BwTest.RunWorkerCompleted, AddressOf BwTest_RunWorkerCompleted
    End Sub

    Private Sub TestingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyInstance = Me
    End Sub

    Private Sub OpenEdit_MouseHover(sender As Object, e As EventArgs) Handles OpenEdit.MouseHover
        ToolTip1.SetToolTip(Me.Upload, "Buka Editor")
    End Sub

    Private Sub Upload_MouseHover(sender As Object, e As EventArgs) Handles Upload.MouseHover, OpenEdit.MouseHover
        ToolTip1.SetToolTip(Me.Upload, "Upload foto")
    End Sub

    Private Sub Start_MouseHover(sender As Object, e As EventArgs)
        ToolTip1.SetToolTip(Me.Start, "Mainkan Suara")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Upload.Click
        Dim ofd As New OpenFileDialog
        ofd.InitialDirectory = "C:\Users\Johan\Documents\Visual Studio 2015\Projects\ProgramTA-V2\DataPercobaan"
        ofd.FileName = ""
        ofd.Filter = "Gambar (*.png)|*.png|Gambar (*.jpg)|*.jpg|Gambar (*.jpeg)|*.jpeg"
        ofd.FilterIndex = 2
        ofd.Multiselect = False

        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            GUtama.Image = New Bitmap(ofd.FileName)
        End If
    End Sub

    Private Sub Start_Click(sender As Object, e As EventArgs) Handles Start.Click
        If GUtama.Image IsNot Nothing Then
            If Not BwTest.IsBusy Then
                BwTest.RunWorkerAsync()
                BwTest.Dispose()
            End If
        Else
            MsgBox("Gambar belum diambil")
        End If
    End Sub

    Private Sub OpenEdit_Click(sender As Object, e As EventArgs) Handles OpenEdit.Click
        Dim Editor As New EditGambar
        Editor.GUtama.Image = New Bitmap(GUtama.Image)
        Editor.ShowDialog(Me)
        GUtama.Image = Editor.hsl(2)
        Editor.Dispose()
    End Sub

    Private Sub BwTest_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Progress = "Preprocessing Start" & vbNewLine
        BwTest.ReportProgress(0, Progress)
        Gambar = TestingStartPreprocessing(GUtama.Image)
        Pre1.Image = Gambar(0)
        Pre2.Image = Gambar(1)
        Pre3.Image = Gambar(2)
        Pre4.Image = Gambar(3)
        Gambar = TestingSplitGambar(Gambar(3))
        S1.Image = Gambar(0)
        S2.Image = Gambar(1)
        S3.Image = Gambar(2)
        S4.Image = Gambar(3)
        Progress &= "Preprocessing Done" & vbNewLine
        BwTest.ReportProgress(0, Progress)
        weight1 = getBobot(0)
        weight2 = getBobot(1)
        weight3 = getBobot(2)
        weight4 = getBobot(3)
        Progress &= "Mendapatkan Bobot Done" & vbNewLine
        Progress &= "================================================================" & vbNewLine
        BwTest.ReportProgress(0, Progress)
        Hasil = StartTesting(Gambar)
        HasilAkhir(Hasil)
    End Sub

    Private Sub BwTest_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        ProgressBar.Text = DirectCast(e.UserState, String)
        ProgressBar.SelectionStart = ProgressBar.Text.Length
        ProgressBar.ScrollToCaret()
    End Sub

    Private Sub BwTest_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

    End Sub
End Class