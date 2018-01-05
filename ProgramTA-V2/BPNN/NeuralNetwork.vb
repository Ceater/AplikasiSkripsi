Public Class NeuralNetwork : Implements IDisposable
    Private Shared rnd As Random
    Private disposed As Boolean = False
    Private numInput As Integer
    Private numHidden As Integer
    Private numOutput As Integer

    Private inputs As Double()
    Private ihWeights As Double()()
    Private hBiases As Double()
    Private hOutputs As Double()
    Private hoWeights As Double()()
    Private oBiases As Double()
    Private outputs As Double()
    Private oGrads As Double()
    Private hGrads As Double()
    Private ihPrevWeightsDelta As Double()()
    Private hPrevBiasesDelta As Double()
    Private hoPrevWeightsDelta As Double()()
    Private oPrevBiasesDelta As Double()

    Public Sub New(ByVal numInput As Integer, ByVal numHidden As Integer, ByVal numOutput As Integer)
        'Deklarasi jumlah input, hidden, dan output
        rnd = New Random(0)
        Me.numInput = numInput
        Me.numHidden = numHidden
        Me.numOutput = numOutput

        Me.inputs = New Double(numInput - 1) {}
        Me.ihWeights = MakeMatrix(numInput, numHidden)
        Me.hBiases = New Double(numHidden - 1) {}
        Me.hOutputs = New Double(numHidden - 1) {}
        Me.hoWeights = MakeMatrix(numHidden, numOutput)
        Me.oBiases = New Double(numOutput - 1) {}
        Me.outputs = New Double(numOutput - 1) {}
        Me.hGrads = New Double(numHidden - 1) {}
        Me.oGrads = New Double(numOutput - 1) {}
        Me.ihPrevWeightsDelta = MakeMatrix(numInput, numHidden)
        Me.hPrevBiasesDelta = New Double(numHidden - 1) {}
        Me.hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput)
        Me.oPrevBiasesDelta = New Double(numOutput - 1) {}
    End Sub

    Private Shared Function MakeMatrix(ByVal rows As Integer, ByVal cols As Integer) As Double()()
        Dim result As Double()() = New Double(rows - 1)() {}
        For r As Integer = 0 To result.Length - 1
            result(r) = New Double(cols - 1) {}
        Next
        Return result
    End Function

    Public Sub SetWeights(ByVal weights As Double())
        'Mencopy weight dan bias pada input-hidden weight, input-hidden bias, hidden-output weight, hidden-output bias
        Dim numWeights As Integer = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput
        If weights.Length <> numWeights Then
            Throw New Exception("Panjang array weight tidak sesuai dengan jumlah weight")
        End If

        Dim k As Integer = 0
        For i As Integer = 0 To numInput - 1
            For j As Integer = 0 To numHidden - 1
                ihWeights(i)(j) = weights(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1))
            Next
        Next
        For i As Integer = 0 To numHidden - 1
            hBiases(i) = weights(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1))
        Next
        For i As Integer = 0 To numHidden - 1
            For j As Integer = 0 To numOutput - 1
                hoWeights(i)(j) = weights(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1))
            Next
        Next
        For i As Integer = 0 To numOutput - 1
            oBiases(i) = weights(numWeights - 1)
        Next
    End Sub

    Public Sub InitializeWeights()
        'Deklarasi weight dan bias dengan nilai random antara 0.0001 sampai 0.001
        Dim numWeights As Integer = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput
        Dim initialWeights As Double() = New Double(numWeights - 1) {}
        For i As Integer = 0 To initialWeights.Length - 1
            initialWeights(i) = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001
        Next
        Me.SetWeights(initialWeights)
    End Sub

    Public Function GetWeights() As Double()
        'Mengembalikan weight dan bias sementara
        Dim numWeights As Integer = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput
        Dim result As Double() = New Double(numWeights - 1) {}
        Dim k As Integer = 0
        For i As Integer = 0 To ihWeights.Length - 1
            For j As Integer = 0 To ihWeights(0).Length - 1
                result(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1)) = ihWeights(i)(j)
            Next
        Next
        For i As Integer = 0 To hBiases.Length - 1
            result(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1)) = hBiases(i)
        Next
        For i As Integer = 0 To hoWeights.Length - 1
            For j As Integer = 0 To hoWeights(0).Length - 1
                result(System.Math.Max(System.Threading.Interlocked.Increment(k), k - 1)) = hoWeights(i)(j)
            Next
        Next
        For i As Integer = 0 To oBiases.Length - 1
            result(numWeights - 1) = oBiases(i)
        Next
        Return result
    End Function

    Public Function ComputeOutputs(ByVal xValues As Double()) As Double()
        'Mendapatkan output yang dihasilkan dari perhitungan menggunakan weight dan bias
        If xValues.Length <> numInput Then
            Throw New Exception("Panjang array input tidak sesuai dengan jumlah input")
        End If
        Dim hSums As Double() = New Double(numHidden - 1) {}
        Dim oSums As Double() = New Double(numOutput - 1) {}
        For i As Integer = 0 To xValues.Length - 1
            Me.inputs(i) = xValues(i)
        Next

        For j As Integer = 0 To numHidden - 1
            For i As Integer = 0 To numInput - 1
                hSums(j) += Me.inputs(i) * Me.ihWeights(i)(j)
            Next
        Next

        For i As Integer = 0 To numHidden - 1
            hSums(i) += Me.hBiases(i)
        Next

        For i As Integer = 0 To numHidden - 1
            Me.hOutputs(i) = HyperTanFunction(hSums(i))
        Next

        For j As Integer = 0 To numOutput - 1
            For i As Integer = 0 To numHidden - 1
                oSums(j) += hOutputs(i) * hoWeights(i)(j)
            Next
        Next

        For i As Integer = 0 To numOutput - 1
            oSums(i) += oBiases(i)
        Next

        Dim softOut As Double() = Softmax(oSums)
        Array.Copy(softOut, outputs, softOut.Length)

        Dim retResult As Double() = New Double(numOutput - 1) {}
        Array.Copy(Me.outputs, retResult, retResult.Length)
        Return retResult
    End Function

    Private Shared Function HyperTanFunction(ByVal x As Double) As Double
        If x < -20.0 Then
            Return -1.0
        ElseIf x > 20.0 Then
            Return 1.0
        Else
            Return Math.Tanh(x)
        End If
    End Function

    Private Shared Function Softmax(ByVal oSums As Double()) As Double()
        Dim max As Double = oSums(0)
        For i As Integer = 0 To oSums.Length - 1
            If oSums(i) > max Then
                max = oSums(i)
            End If
        Next

        Dim scale As Double = 0.0
        For i As Integer = 0 To oSums.Length - 1
            scale += Math.Exp(oSums(i) - max)
        Next

        Dim result As Double() = New Double(oSums.Length - 1) {}
        For i As Integer = 0 To oSums.Length - 1
            result(i) = Math.Exp(oSums(i) - max) / scale
        Next

        Return result
    End Function

    Private Sub UpdateWeights(ByVal tValues As Double(), ByVal learnRate As Double, ByVal momentum As Double)
        'Update weight and bias menggunakan back-propagation dengan target values, learning rate, dan momentum        
        If tValues.Length <> numOutput Then
            Throw New Exception("Panjang array output tidak sesuai dengan jumlah ouput")
        End If

        '1. Menghitung output gradients
        For i As Integer = 0 To oGrads.Length - 1
            Dim derivative As Double = (1 - outputs(i)) * outputs(i)
            oGrads(i) = derivative * (tValues(i) - outputs(i))
        Next

        '2. Menghitung hidden gradients
        For i As Integer = 0 To hGrads.Length - 1
            Dim derivative As Double = (1 - hOutputs(i)) * (1 + hOutputs(i))
            Dim sum As Double = 0.0
            For j As Integer = 0 To numOutput - 1
                Dim x As Double = oGrads(j) * hoWeights(i)(j)
                sum += x
            Next
            hGrads(i) = derivative * sum
        Next

        '3a. Update hidden weight
        For i As Integer = 0 To ihWeights.Length - 1
            For j As Integer = 0 To ihWeights(0).Length - 1
                Dim delta As Double = learnRate * hGrads(j) * inputs(i)
                ihWeights(i)(j) += delta
                ihWeights(i)(j) += momentum * ihPrevWeightsDelta(i)(j)
                ihPrevWeightsDelta(i)(j) = delta
            Next
        Next

        '3b. Update hidden bias
        For i As Integer = 0 To hBiases.Length - 1
            Dim delta As Double = learnRate * hGrads(i) * 1.0
            hBiases(i) += delta
            hBiases(i) += momentum * hPrevBiasesDelta(i)
            hPrevBiasesDelta(i) = delta
        Next

        '4a. Update hidden-output weight
        For i As Integer = 0 To hoWeights.Length - 1
            For j As Integer = 0 To hoWeights(0).Length - 1
                Dim delta As Double = learnRate * oGrads(j) * hOutputs(i)
                hoWeights(i)(j) += delta
                hoWeights(i)(j) += momentum * hoPrevWeightsDelta(i)(j)
                hoPrevWeightsDelta(i)(j) = delta
            Next
        Next

        '4b. Update output bias
        For i As Integer = 0 To oBiases.Length - 1
            Dim delta As Double = learnRate * oGrads(i) * 1.0
            oBiases(i) += delta
            oBiases(i) += momentum * oPrevBiasesDelta(i)
            oPrevBiasesDelta(i) = delta
        Next
    End Sub

    Public Sub Train(ByVal trainData As Double()(), ByVal maxEpochs As Integer, ByVal learnRate As Double, ByVal momentum As Double, ByVal mse As Double)
        'Melakukan Back Propagation Neural Network dengan learning rate dan momentum
        Dim epoch As Integer = 0
        Dim xValues As Double() = New Double(numInput - 1) {} 'Input        
        Dim yValues As Double() 'Output
        Dim tValues As Double() = New Double(numOutput - 1) {} 'Target        
        Dim sequence As Integer() = New Integer(trainData.Length - 1) {}
        For i As Integer = 0 To sequence.Length - 1
            sequence(i) = i
        Next

        While epoch < maxEpochs
            Dim err As Double = MeanSquaredError(trainData)
            If err < mse Then
                'TrainingForm.MyInstance.progress &= vbNewLine
                'TrainingForm.MyInstance.progress &= "Training dihentikan pada epoch " & epoch & vbNewLine
                'TrainingForm.MyInstance.progress &= "Nilai weight dan bias yang memenuhi kriteria MSE telah ditemukan pada epoch " & epoch & vbNewLine
                'TrainingForm.BwTrain.ReportProgress(0, TrainingForm.MyInstance.progress)

                TrainingForm.MyInstance.result &= "Ketemu di epoch " & epoch & vbNewLine
                'TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.MyInstance.ProgressBox, TrainingForm.MyInstance.progress, TrainingForm.MyInstance.ResultBox, TrainingForm.MyInstance.result))
                Exit While
            End If

            Shuffle(sequence)
            For i As Integer = 0 To trainData.Length - 1
                Dim idx As Integer = sequence(i)
                Array.Copy(trainData(idx), xValues, numInput)
                Array.Copy(trainData(idx), numInput, tValues, 0, numOutput)
                yValues = ComputeOutputs(xValues)
                UpdateWeights(tValues, learnRate, momentum)
            Next

            If epoch Mod 1 = 0 Then
                Dim finalError As Double = MeanSquaredError(trainData)
                TrainingForm.MyInstance.progress &= "EPOCH " & epoch & " : "
                TrainingForm.MyInstance.progress &= "MSE saat ini => " & finalError.ToString("F8") & vbNewLine
                'TrainingForm.BwTrain.ReportProgress(0, TrainingForm.MyInstance.progress)
                TrainingForm.BwTrain.ReportProgress(0, New TrainingForm.ControlWithText(TrainingForm.MyInstance.ProgressBox, TrainingForm.MyInstance.progress, TrainingForm.MyInstance.ResultBox, TrainingForm.MyInstance.result))
            End If
            epoch += 1
        End While
    End Sub

    Private Shared Sub Shuffle(ByVal sequence As Integer())
        'Mengacak urutan training data
        For i As Integer = 0 To sequence.Length - 1
            Dim r As Integer = rnd.[Next](i, sequence.Length)
            Dim tmp As Integer = sequence(r)
            sequence(r) = sequence(i)
            sequence(i) = tmp
        Next
    End Sub

    Private Function MeanSquaredError(ByVal trainData As Double()()) As Double
        'Menghitung MSE yang digunakan sebagai acuan pemberhentian training
        Dim sumSquaredError As Double = 0.0
        Dim xValues As Double() = New Double(numInput - 1) {} 'Input
        Dim tValues As Double() = New Double(numOutput - 1) {} 'Target 
        For i As Integer = 0 To trainData.Length - 1
            Array.Copy(trainData(i), xValues, numInput)
            Array.Copy(trainData(i), numInput, tValues, 0, numOutput)
            Dim yValues As Double() = Me.ComputeOutputs(xValues) 'Menghitung output berdasarkan weight sementara            
            For j As Integer = 0 To numOutput - 1
                Dim err As Double = tValues(j) - yValues(j)
                sumSquaredError += err * err
            Next
        Next

        Return sumSquaredError / trainData.Length
    End Function

    Public Function Accuracy(ByVal testData As Double()()) As Double
        'Menghitung persentase akurasi
        Dim numCorrect As Integer = 0
        Dim numWrong As Integer = 0
        Dim xValues As Double() = New Double(numInput - 1) {} 'Input        
        Dim tValues As Double() = New Double(numOutput - 1) {} 'Target        
        Dim yValues As Double() 'Output        
        For i As Integer = 0 To testData.Length - 1
            Array.Copy(testData(i), xValues, numInput)
            Array.Copy(testData(i), numInput, tValues, 0, numOutput)
            yValues = Me.ComputeOutputs(xValues)
            Dim maxIndex__1 As Integer = MaxIndex(yValues)
            If tValues(maxIndex__1) = 1.0 Then
                numCorrect += 1
            Else
                numWrong += 1
            End If
        Next
        Return (numCorrect * 1.0) / (numCorrect + numWrong)
    End Function

    Private Shared Function MaxIndex(ByVal vector As Double()) As Integer
        Dim bigIndex As Integer = 0
        Dim biggestVal As Double = vector(0)
        For i As Integer = 0 To vector.Length - 1
            If vector(i) > biggestVal Then
                biggestVal = vector(i)
                bigIndex = i
            End If
        Next
        Return bigIndex
    End Function

    Public Function ShowVector(ByVal vector As Double(), ByVal decimals As Integer, ByVal valsPerLine As Integer) As String
        Dim s As String = ""
        For i As Integer = 0 To vector.Length - 11
            If i > 0 AndAlso i Mod valsPerLine = 0 Then
                s &= vbNewLine
            End If
            If vector(i) >= 0.0 Then
                s &= " "
            End If
            s &= vector(i).ToString("F" & decimals) & " "
        Next
        s &= vbNewLine & vbNewLine

        Return s
    End Function

    Public Sub Dispose() _
               Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If disposed Then Return

        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub
End Class