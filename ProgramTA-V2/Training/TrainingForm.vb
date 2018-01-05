Imports System
Imports System.IO
Imports AForge
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports AForge.Imaging.Filters
Imports AForge.Imaging
Imports AForge.Math.Geometry
Imports System.Drawing.Imaging
Imports System.Drawing.Graphics
Imports System.Math

Public Class TrainingForm
    Public progress, result As String
    Public counter As Integer = 0
    Public Shared BwTrain As New System.ComponentModel.BackgroundWorker
    Friend Shared MyInstance As TrainingForm
    '====> Training Variable <====
    'Dim AllPixel As Double()() = New Double(104)() {} 'Jumlah sample data semua mata uang (7*15)-1
    Dim weight1(30966) As Double 'Jumlah line dinotepad untuk bobot atas kiri
    Dim weight2(30966) As Double 'Jumlah line dinotepad untuk bobot atas kanan
    Dim weight3(30966) As Double 'Jumlah line dinotepad untuk bobot bawah kiri
    Dim weight4(30966) As Double 'Jumlah line dinotepad untuk bobot bawah kanan
    Dim idxbiji As Integer
    Dim training As Boolean
    Dim maxepochs, learnrate, momentum, mse As Double
    Dim kfold()() As Double
    Public akurasiTraining, akurasiTest As Double

    Public Sub New()
        InitializeComponent()
        BwTrain.WorkerReportsProgress = True
        BwTrain.WorkerSupportsCancellation = True
        AddHandler BwTrain.DoWork, AddressOf BwTrain_DoWork
        AddHandler BwTrain.ProgressChanged, AddressOf BwTrain_ProgressChanged
        AddHandler BwTrain.RunWorkerCompleted, AddressOf BwTrain_RunWorkerCompleted
    End Sub

    Private Sub TrainingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MyInstance = Me
    End Sub

    Private Sub TrainButton_Click(sender As Object, e As EventArgs) Handles TrainButton.Click
        maxepochs = TextBox1.Text
        learnrate = TextBox2.Text
        momentum = TextBox3.Text
        mse = TextBox4.Text
        counter = 0
        result = ""
        If Not BwTrain.IsBusy Then
            progress = "Mengerjakan" & vbNewLine
            ProgressBox.Text = progress
            ProgressBox.Focus()
            BwTrain.RunWorkerAsync()
            BwTrain.Dispose()
        End If
    End Sub

    Private Sub BwTrain_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        '====Initialize Variables====
        Dim AllPixel As Double()() = New Double(104)() {} 'Jumlah sample data semua mata uang (7*15)-1
        Dim Classfold As KFold
        Dim foldReady As Double()()()
        Dim tempWeight As Double()()
        training = True
        tempWeight = New Double(4)() {}
        '====Start Bagian 1====
        akurasiTraining = 0
        akurasiTest = 0
        result &= "Memulai Bagian 1" & vbNewLine
        AllPixel = getDataTraining(Konversi1Path)
        Classfold = New KFold(AllPixel, 5, 21) 'Memulai KFold
        foldReady = Classfold.getFold 'Mendapatkan hasil KFold
        For i = 0 To 4
            tempWeight(i) = New Double(30966) {}
            result &= "Fold " & i + 1 & " Bagian 1 => "
            tempWeight(i) = train(foldReady, maxepochs, learnrate, momentum, mse, i)
        Next
        akurasiTraining = akurasiTraining / 5
        akurasiTest = akurasiTest / 5
        result &= "Akurasi Training " & akurasiTraining.ToString("F4") & vbNewLine
        result &= "Akurasi Testing " & akurasiTest.ToString("F4") & vbNewLine
        weight1 = getAvgBobot(tempWeight)
        '====Start Bagian 2====
        akurasiTraining = 0
        akurasiTest = 0
        result &= "Memulai Bagian 2" & vbNewLine
        AllPixel = getDataTraining(Konversi2Path)
        Classfold = New KFold(AllPixel, 5, 21) 'Memulai KFold
        foldReady = Classfold.getFold 'Mendapatkan hasil KFold
        For i = 0 To 4
            tempWeight(i) = New Double(30966) {}
            result &= "Fold " & i + 1 & " Bagian 2 => "
            tempWeight(i) = train(foldReady, maxepochs, learnrate, momentum, mse, i)
        Next
        akurasiTraining = akurasiTraining / 5
        akurasiTest = akurasiTest / 5
        result &= "Akurasi Training " & akurasiTraining.ToString("F4") & vbNewLine
        result &= "Akurasi Testing " & akurasiTest.ToString("F4") & vbNewLine
        weight2 = getAvgBobot(tempWeight)
        '====Start Bagian 3====
        akurasiTraining = 0
        akurasiTest = 0
        result &= "Memulai Bagian 3" & vbNewLine
        AllPixel = getDataTraining(Konversi3Path)
        Classfold = New KFold(AllPixel, 5, 21) 'Memulai KFold
        foldReady = Classfold.getFold 'Mendapatkan hasil KFold
        For i = 0 To 4
            tempWeight(i) = New Double(30966) {}
            result &= "Fold " & i + 1 & " Bagian 3 => "
            tempWeight(i) = train(foldReady, maxepochs, learnrate, momentum, mse, i)
        Next
        akurasiTraining = akurasiTraining / 5
        akurasiTest = akurasiTest / 5
        result &= "Akurasi Training " & akurasiTraining.ToString("F4") & vbNewLine
        result &= "Akurasi Testing " & akurasiTest.ToString("F4") & vbNewLine
        weight3 = getAvgBobot(tempWeight)
        '====Start Bagian 4====
        akurasiTraining = 0
        akurasiTest = 0
        result &= "Memulai Bagian 4" & vbNewLine
        AllPixel = getDataTraining(Konversi4Path)
        Classfold = New KFold(AllPixel, 5, 21) 'Memulai KFold
        foldReady = Classfold.getFold 'Mendapatkan hasil KFold
        For i = 0 To 4
            tempWeight(i) = New Double(30966) {}
            result &= "Fold " & i + 1 & " Bagian 4 => "
            tempWeight(i) = train(foldReady, maxepochs, learnrate, momentum, mse, i)
        Next
        akurasiTraining = akurasiTraining / 5
        akurasiTest = akurasiTest / 5
        result &= "Akurasi Training " & akurasiTraining.ToString("F4") & vbNewLine
        result &= "Akurasi Testing " & akurasiTest.ToString("F4") & vbNewLine
        BwTrain.ReportProgress(0, New ControlWithText(ProgressBox, progress, ResultBox, result))
        weight4 = getAvgBobot(tempWeight)

        writeBobot(weight1, "w1", W1)
        writeBobot(weight2, "w2", W2)
        writeBobot(weight3, "w3", W3)
        writeBobot(weight4, "w4", W4)
    End Sub

    Private Sub BwTrain_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)
        Dim cwt As ControlWithText = CType(e.UserState, ControlWithText)
        cwt.ControlName.Text = cwt.Text
        cwt.ControlName1.Text = cwt.Text1
        ProgressBox.SelectionStart = ProgressBox.Text.Length
        ProgressBox.ScrollToCaret()
        ResultBox.SelectionStart = ResultBox.Text.Length
        ResultBox.ScrollToCaret()
    End Sub

    Private Sub BwTrain_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        ProgressBox.Text = progress
        ProgressBox.SelectionStart = ProgressBox.Text.Length
        ProgressBox.ScrollToCaret()
    End Sub

    Public Structure ControlWithText
        Public ControlName As Control
        Public Text As String
        Public ControlName1 As Control
        Public Text1 As String

        Public Sub New(ByVal ctrl1 As Control, ByVal text1 As String, ByVal ctrl2 As Control, ByVal text2 As String)
            Me.ControlName = ctrl1
            Me.Text = text1
            Me.ControlName1 = ctrl2
            Me.Text1 = text2
        End Sub
    End Structure
End Class
