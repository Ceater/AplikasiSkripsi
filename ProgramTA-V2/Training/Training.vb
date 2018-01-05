Imports System.IO

Module Training
    Dim nn As NeuralNetwork
    Public progress, result As String
    Public counter As Integer = 0
    Dim AllPixel As Double()() = New Double(104)() {}

    Function train(allpixel()()() As Double, maxepochs As Double, learnrate As Double, momentum As Double, mse As Double, testFold As Integer) As Double()
        'Deklarasi jumlah Input, Hidden, dan Output
        Const numInput As Integer = 1024
        Const numHidden As Integer = 30
        Const numOutput As Integer = 7
        nn = New NeuralNetwork(numInput, numHidden, numOutput)

        'Membuat data training dan testing
        Dim trainData As Double()() = Nothing
        Dim testData As Double()() = Nothing
        MakeTrainTest(allpixel, trainData, testData, testFold)

        'Deklarasi seluruh weights dan bias dengan random antara -1 sampai +1        
        nn.InitializeWeights()
        'Memulai training
        nn.Train(trainData, maxepochs, learnrate, momentum, mse)
        Dim weights As Double() = nn.GetWeights()
        Dim trainAcc As Double = nn.Accuracy(trainData)
        Dim testAcc As Double = nn.Accuracy(testData)
        TrainingForm.MyInstance.akurasiTraining += trainAcc
        TrainingForm.MyInstance.akurasiTest += testAcc
        Return weights
    End Function

    Sub MakeTrainTest(ByVal allData As Double()()(), ByRef trainData As Double()(), ByRef testData As Double()(), ByVal testFold As Integer)
        testData = allData(testFold)
        trainData = New Double(83)() {}
        Dim counter As Integer = 0
        For i = 0 To 4
            If i <> testFold Then
                For j = 0 To allData(testFold).Length - 1
                    trainData(counter) = allData(i)(j)
                    counter += 1
                Next
            End If
        Next
    End Sub

    Sub writeBobot(ByVal allPixel() As Double, ByVal FName As String, bobotpath As String)
        'Untuk menyimpan bobot training
        'Menyimpan data pada file txt
        Try
            Dim fileExists As Boolean = File.Exists(bobotpath)
            If Not fileExists Then
                File.Create(bobotpath).Dispose()
            Else
                File.Delete(bobotpath)
            End If

            Using writer As StreamWriter = New StreamWriter(bobotpath)
                For r As Integer = 0 To allPixel.Length - 1
                    writer.WriteLine(allPixel(r))
                Next r
            End Using
        Catch ex As Exception
            MsgBox("Tidak bisa mengambil file bobot")
        End Try
    End Sub

    Function getAvgBobot(tempWeight()() As Double)
        Dim avg As Double() = New Double(30966) {}
        For i = 0 To 30966
            avg(i) = (tempWeight(0)(i) + tempWeight(1)(i) + tempWeight(2)(i) + tempWeight(3)(i) + tempWeight(4)(i)) / 5
        Next
        Return avg
    End Function

    Function getDataTraining(ByVal FName As String)
        'Mengambil training data dari txt file        
        Dim filePath As String = FName
        Dim idxPixel As Integer = 0
        Dim sr As StreamReader = New StreamReader(filePath)
        Dim line As String = ""
        Dim coun As Integer = 0
        Do
            line = sr.ReadLine
            If Not line Is Nothing Then
                Dim pixel(0 To 1030) As Double 'Total 1031 = (32*32) + 7 dimana 32*32 adalah jumlah pixelnya, dan 7 adalah outputnya
                Dim temp() As String = line.Split(",")
                For i = 0 To pixel.Length - 1
                    pixel(i) = CDbl(temp(i))
                Next
                AllPixel(idxPixel) = pixel
                idxPixel = idxPixel + 1
            End If
        Loop Until line Is Nothing
        Return AllPixel
    End Function

    'Function train(allpixel()() As Double, maxepochs As Double, learnrate As Double, momentum As Double, mse As Double) As Double()
    '    'Deklarasi jumlah Input, Hidden, dan Output           
    '    Const numInput As Integer = 1024
    '    Const numHidden As Integer = 30
    '    Const numOutput As Integer = 7
    '    nn = New NeuralNetwork(numInput, numHidden, numOutput)
    '    progress = ""
    '    counter += 1
    '    result &= "=============== Bagian " & counter & "===============" & vbNewLine
    '    'Membagi data set menjadi 2 bagian yaitu data training dan data test       
    '    progress &= vbNewLine & "Membagi data set menjadi 80% training data dan 20% testing data " & vbNewLine & vbNewLine
    '    TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.ProgressBox, progress, TrainingForm.ResultBox, result))

    '    Dim trainData As Double()() = Nothing
    '    Dim testData As Double()() = Nothing
    '    MakeTrainTest(allpixel, trainData, testData)

    '    'Deklarasi seluruh weights dan bias dengan random antara -1 sampai +1        
    '    nn.InitializeWeights()
    '    TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.ProgressBox, progress, TrainingForm.ResultBox, result))

    '    'Memulai training
    '    progress &= "------------------------------------------------" & vbNewLine
    '    progress &= vbNewLine
    '    progress &= "Memulai training dengan back propagation " & vbNewLine
    '    progress &= vbNewLine
    '    TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.ProgressBox, progress, TrainingForm.ResultBox, result))
    '    nn.Train(trainData, maxepochs, learnrate, momentum, mse)
    '    Dim weights As Double() = nn.GetWeights()
    '    progress &= vbNewLine & "------------------------------------------------" & vbNewLine
    '    progress &= vbNewLine
    '    TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.ProgressBox, progress, TrainingForm.ResultBox, result))

    '    Dim trainAcc As Double = nn.Accuracy(trainData)
    '    Dim testAcc As Double = nn.Accuracy(testData)

    '    result &= "Akurasi pada training data = " + trainAcc.ToString("F4") & vbNewLine
    '    result &= "Akurasi pada test data = " + testAcc.ToString("F4") & vbNewLine

    '    TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.ProgressBox, progress, TrainingForm.ResultBox, result))
    '    Return weights
    'End Function

    'Sub MakeTrainTest(ByVal allData As Double()(), ByRef trainData As Double()(), ByRef testData As Double()())
    '    'Membagi data set menjadi 67% data training dan 33% data test.
    '    Dim i, j As Integer
    '    Dim rnd As New Random(0)
    '    Dim totRows As Integer = allData.Length
    '    Dim numCols As Integer = allData(0).Length
    '    Dim trainRows As Integer = CInt(totRows * 0.67)
    '    Dim testRows As Integer = totRows - trainRows
    '    trainData = New Double(trainRows - 1)() {}
    '    testData = New Double(testRows - 1)() {}

    '    'Membuat random sequence
    '    Dim sequence As Integer() = New Integer(totRows - 1) {}
    '    For i = 0 To sequence.Length - 1
    '        sequence(i) = i
    '    Next

    '    For i = 0 To sequence.Length - 1
    '        Dim r As Integer = rnd.[Next](i, sequence.Length)
    '        Dim tmp As Integer = sequence(r)
    '        sequence(r) = sequence(i)
    '        sequence(i) = tmp
    '    Next

    '    i = 0 'Index untuk sequence
    '    j = 0 'Index untuk data training
    '    While i < trainRows
    '        trainData(j) = New Double(numCols - 1) {}
    '        Dim idx As Integer = sequence(i)
    '        Array.Copy(allData(idx), trainData(j), numCols)
    '        j += 1
    '        i += 1
    '    End While

    '    j = 0 'Reset Index untuk data test
    '    While i < totRows
    '        'remainder to test data
    '        testData(j) = New Double(numCols - 1) {}
    '        Dim idx As Integer = sequence(i)
    '        Array.Copy(allData(idx), testData(j), numCols)
    '        j += 1
    '        i += 1
    '    End While
    'End Sub
End Module
